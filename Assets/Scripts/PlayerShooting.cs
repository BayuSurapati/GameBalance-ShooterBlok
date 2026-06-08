using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [Header("Referensi")]
    public Camera fpsCamera;
    public InputActionReference fireAction;
    public Transform shootPoint;    // Tempat peluru keluar
    public GameObject bulletPrefab; // Prefab peluru yang akan ditembakkan

    [Header("Pengaturan Senjata")]
    public float bulletSpeed = 20f; // Kecepatan peluru

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
        // 1. Memunculkan peluru di posisi dan rotasi ShootPoint
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

        // 2. Mengambil komponen Rigidbody dari peluru tersebut
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // 3. Memberikan kecepatan pada peluru agar melesat lurus 
            // mengikuti arah pandangan kamera (tengah layar)
            rb.velocity = fpsCamera.transform.forward * bulletSpeed;
        }
    }
}