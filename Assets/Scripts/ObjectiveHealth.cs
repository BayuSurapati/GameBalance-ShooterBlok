using UnityEngine;
using UnityEngine.UI; // WAJIB ditambahkan untuk memanipulasi komponen UI Slider

public class ObjectiveHealth : MonoBehaviour
{
    [Header("Status Objek")]
    public float maxHealth = 500f;
    private float currentHealth;

    [Header("Referensi UI")]
    public Slider healthSlider; // Tempat menaruh komponen Slider dari Canvas

    void Start()
    {
        currentHealth = maxHealth;

        // Inisialisasi nilai awal Slider sesuai dengan HP Maksimal
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        // Perbarui nilai visual Slider setiap kali objek menerima damage
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        Debug.Log("⚠️ " + gameObject.name + " diserang! Sisa HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Hancur();
        }
    }

    private void Hancur()
    {
        Debug.Log("💥 " + gameObject.name + " HANCUR!");

        // Sembunyikan atau matikan Slider jika objeknya sudah hancur dari game
        if (healthSlider != null)
        {
            healthSlider.gameObject.SetActive(false);
        }

        Destroy(gameObject);
    }
}