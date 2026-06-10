using System.Collections.Generic;
using UnityEngine;

// Struktur data khusus untuk menentukan bobot kemunculan setiap jenis musuh
[System.Serializable]
public class EnemySpawnWeight
{
    public string namaMusuh; // Hanya penanda agar rapi di Inspector
    public EnemyData enemyData; // File Scriptable Object musuh

    [Tooltip("Semakin besar angkanya, semakin sering musuh ini muncul relatif terhadap total bobot.")]
    public int weight = 10;
}

// Struktur data untuk mengatur setiap fase gelombang
[System.Serializable]
public class WavePhase
{
    public string phaseName;
    [Tooltip("Fase ini aktif saat sisa waktu di UI menyentuh angka ini (Tulis dari angka terbesar ke terkecil)")]
    public float startTime;
    public float spawnRate;

    // Menggunakan struktur data bobot yang baru
    public EnemySpawnWeight[] allowedEnemies;
}

public class WaveManager : MonoBehaviour
{
    [Header("Referensi Spawner")]
    public EnemySpawner spawner;

    [Header("Konfigurasi Gelombang (Sistem Bobot)")]
    public List<WavePhase> wavePhases = new List<WavePhase>();

    private WavePhase currentPhase;
    private float spawnTimer;

    void Update()
    {
        // 1. Membaca sisa waktu dari UIManager
        float sisaWaktu = UIManager.instance.waktuSaatIni;

        // 2. Mengecek perubahan fase (Logika yang sudah diperbaiki agar tidak reset setiap frame)
        CheckPhase(sisaWaktu);

        // 3. Logika memunculkan musuh berkala berdasarkan spawn rate fase aktif
        if (currentPhase != null && sisaWaktu > 0)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= currentPhase.spawnRate)
            {
                SpawnRandomEnemyFromPhase();
                spawnTimer = 0f;
            }
        }
    }

    private void CheckPhase(float currentTime)
    {
        WavePhase kandidatFase = null;

        // Mencari fase yang paling sesuai dengan sisa waktu saat ini
        foreach (WavePhase phase in wavePhases)
        {
            if (currentTime <= phase.startTime)
            {
                kandidatFase = phase;
            }
        }

        // Eksekusi pergantian fase (Hanya berjalan SEKALI saat terjadi perpindahan sesungguhnya)
        if (kandidatFase != null && currentPhase != kandidatFase)
        {
            currentPhase = kandidatFase;
            spawnTimer = 0f;
            Debug.Log("⚠️ Memasuki Fase Baru: " + currentPhase.phaseName);
        }
    }

    private void SpawnRandomEnemyFromPhase()
    {
        if (currentPhase.allowedEnemies.Length == 0) return;

        // --- ALGORITMA WEIGHTED RANDOM ---

        // A. Hitung total bobot dari semua musuh yang terdaftar di fase ini
        int totalWeight = 0;
        foreach (EnemySpawnWeight enemy in currentPhase.allowedEnemies)
        {
            totalWeight += enemy.weight;
        }

        if (totalWeight <= 0)
        {
            Debug.LogWarning("Total bobot di fase " + currentPhase.phaseName + " bernilai 0 atau minus!");
            return;
        }

        // B. Lempar dadu virtual (angka acak antara 0 hingga total bobot)
        int randomRoll = Random.Range(0, totalWeight);

        // C. Cari musuh mana yang berhak muncul berdasarkan angka dadu tersebut
        EnemyData selectedEnemyData = null;
        int weightSum = 0;

        foreach (EnemySpawnWeight enemy in currentPhase.allowedEnemies)
        {
            weightSum += enemy.weight;

            if (randomRoll < weightSum)
            {
                selectedEnemyData = enemy.enemyData;
                break; // Keluar dari perulangan jika target sudah ditemukan
            }
        }

        // D. Instruksikan Spawner untuk melahirkan musuh yang terpilih
        if (selectedEnemyData != null && spawner != null)
        {
            spawner.SpawnEnemy(selectedEnemyData);
        }
    }
}