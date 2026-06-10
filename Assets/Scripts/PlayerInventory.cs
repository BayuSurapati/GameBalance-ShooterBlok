using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Isi Tas Pemain")]
    public int currentAmmo = 30;
    public int currentGrenades = 0;

    void Start()
    {
        // Memperbarui UI pertama kali saat game dimulai
        UIManager.instance.UpdateAmmo(currentAmmo);
        UIManager.instance.UpdateGrenade(currentGrenades);
    }

    public void AddAmmo(int amount)
    {
        currentAmmo += amount;
        UIManager.instance.UpdateAmmo(currentAmmo); // Update UI
    }

    public void AddGrenade(int amount)
    {
        currentGrenades += amount;
        UIManager.instance.UpdateGrenade(currentGrenades); // Update UI
    }

    // Fungsi baru khusus untuk mengurangi peluru saat menembak
    public void UseAmmo()
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;
            UIManager.instance.UpdateAmmo(currentAmmo); // Update UI
        }
    }
}