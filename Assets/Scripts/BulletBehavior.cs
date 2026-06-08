using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [Header("Pengaturan Peluru")]
    public float lifeTime = 3f; // Peluru hancur otomatis dalam 3 detik
    public float hitForce = 150f;

    void Start()
    {
        // Menghancurkan peluru ini otomatis setelah waktu lifeTime habis
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Jika menabrak objek yang memiliki Rigidbody, berikan gaya dorong
        Rigidbody targetRb = collision.gameObject.GetComponent<Rigidbody>();
        if (targetRb != null)
        {
            // Menghitung arah pantulan berdasarkan titik tabrakan
            Vector3 pushDirection = collision.contacts[0].normal;
            targetRb.AddForce(-pushDirection * hitForce);
        }

        // Hancurkan peluru segera setelah menabrak sesuatu
        Destroy(gameObject);
    }
}