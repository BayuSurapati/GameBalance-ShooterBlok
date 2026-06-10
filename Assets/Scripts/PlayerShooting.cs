using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [Header("Referensi Sistem")]
    public Camera fpsCamera;
    public InputActionReference fireAction;
    public Transform shootPoint;
    public GameObject bulletPrefab;

    // Variabel baru untuk mengakses isi tas pemain
    private PlayerInventory inventory;

    [Header("Pengaturan Senjata")]
    public float bulletSpeed = 50f;

    void Start()
    {
        // Mengambil komponen PlayerInventory yang menempel pada objek Player yang sama
        inventory = GetComponent<PlayerInventory>();

        if (inventory == null)
        {
            Debug.LogWarning("PlayerInventory tidak ditemukan pada objek Player!");
        }
    }

    void OnEnable()
    {
        fireAction.action.Enable();
        fireAction.action.performed += Shoot;
    }

    void OnDisable()
    {
        fireAction.action.Disable();
        fireAction.action.performed -= Shoot;
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        if (inventory != null && inventory.currentAmmo > 0)
        {
            // Panggil fungsi UseAmmo untuk mengurangi peluru & update UI
            inventory.UseAmmo();

            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.velocity = fpsCamera.transform.forward * bulletSpeed;
            }
        }
        else
        {
            Debug.Log("💥 Peluru Habis! Cepat cari Ammo Box!");
        }
    }
}
