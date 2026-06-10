using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    // Membuat pilihan tipe item (Dropdown di Inspector)
    public enum ItemType { Ammo, Grenade }

    [Header("Pengaturan Item")]
    public ItemType tipeItem;
    public int jumlahItem = 5; // Berapa isi peluru/granat di dalam box ini

    // Fungsi ini terpanggil otomatis jika ada objek yang menyentuh area Trigger item ini
    private void OnTriggerEnter(Collider other)
    {
        // Mengecek apakah yang menyentuh item ini adalah pemain
        if (other.CompareTag("Player"))
        {
            // Mengambil komponen PlayerInventory dari pemain yang menyentuh
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();

            if (inventory != null)
            {
                // Menentukan fungsi apa yang dipanggil berdasarkan tipe item
                switch (tipeItem)
                {
                    case ItemType.Ammo:
                        inventory.AddAmmo(jumlahItem);
                        break;

                    case ItemType.Grenade:
                        inventory.AddGrenade(jumlahItem);
                        break;
                }

                // Hancurkan item dari scene setelah diambil
                Destroy(gameObject);
            }
        }
    }
}