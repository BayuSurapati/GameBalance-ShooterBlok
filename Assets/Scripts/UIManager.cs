using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Referensi Teks UI")]
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI grenadeText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI killText;

    [Header("Pengaturan Waktu (Detik)")]
    public float waktuMundurAwal = 120f;
    public float waktuSaatIni;
    private bool waktuHabis = false;

    [Header("Variabel Data")]
    private int totalKills = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        waktuSaatIni = waktuMundurAwal;

        // PENTING: Memastikan waktu game kembali berjalan normal (1) saat game dimulai/diulang
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (!waktuHabis)
        {
            waktuSaatIni -= Time.deltaTime;

            if (waktuSaatIni <= 0)
            {
                waktuSaatIni = 0;
                waktuHabis = true;

                // Memanggil fungsi untuk menghentikan game
                SelesaiWaktuHabis();
            }

            int minutes = Mathf.FloorToInt(waktuSaatIni / 60F);
            int seconds = Mathf.FloorToInt(waktuSaatIni - minutes * 60);
            timerText.text = string.Format("Waktu: {0:00}:{1:00}", minutes, seconds);
        }
    }

    private void SelesaiWaktuHabis()
    {
        // Membekukan seluruh jalannya waktu di dalam Unity
        Time.timeScale = 0f;

        Debug.Log("⏱️ WAKTU HABIS! Game telah dihentikan (Freeze).");

        // TIPS KE DEPAN: Di sini adalah tempat terbaik untuk memunculkan 
        // UI Panel "Victory" atau "Game Over" Anda (misal: panelGameOver.SetActive(true);)
    }

    public void UpdateAmmo(int amount)
    {
        ammoText.text = "Peluru: " + amount;
    }

    public void UpdateGrenade(int amount)
    {
        grenadeText.text = "Granat: " + amount;
    }

    public void AddKill()
    {
        totalKills++;
        killText.text = "Kills: " + totalKills;
    }
}