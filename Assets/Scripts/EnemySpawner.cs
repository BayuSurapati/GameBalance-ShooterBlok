using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Titik Kemunculan")]
    public Transform[] spawnPoints;

    [Header("Testing (Sementara)")]
    public EnemyData[] testEnemyData; // Array untuk menyimpan berbagai jenis musuh saat playtest

    // Fungsi utama ini sekarang menerima parameter EnemyData.
    // Nantinya, WaveManager (dari pembacaan CSV) akan memanggil fungsi ini!
    public void SpawnEnemy(EnemyData enemyToSpawn)
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("Spawn Points belum diisi di Inspector!");
            return;
        }

        if (enemyToSpawn == null || enemyToSpawn.enemyPrefab == null)
        {
            Debug.LogWarning("Data musuh kosong atau tidak memiliki prefab!");
            return;
        }

        // 1. Memilih indeks acak dari array titik spawn
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform selectedPoint = spawnPoints[randomIndex];

        // 2. Instansiasi musuh menggunakan Prefab yang ada di DALAM Scriptable Object
        GameObject spawnedEnemy = Instantiate(enemyToSpawn.enemyPrefab, selectedPoint.position, selectedPoint.rotation);

        // 3. (Opsional tapi disarankan) Menyuntikkan data ke EnemyAI yang baru muncul
        EnemyAI aiComponent = spawnedEnemy.GetComponent<EnemyAI>();
        if (aiComponent != null)
        {
            aiComponent.myData = enemyToSpawn;
        }
    }

    void Update()
    {
        // Tekan tombol T untuk mengetes spawn musuh acak dari array testEnemyData
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (testEnemyData.Length > 0)
            {
                // Pilih jenis musuh secara acak dari array
                int randomEnemyIndex = Random.Range(0, testEnemyData.Length);
                SpawnEnemy(testEnemyData[randomEnemyIndex]);
            }
        }
    }
}