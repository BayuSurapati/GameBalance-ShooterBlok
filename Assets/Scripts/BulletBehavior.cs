using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [Header("Pengaturan Peluru")]
    public float damage = 25f; // Jumlah damage yang diberikan peluru
    public float lifeTime = 3f;
    public float hitForce = 150f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // 1. Cek apakah objek yang ditabrak memiliki komponen EnemyAI
        EnemyAI hitEnemy = collision.gameObject.GetComponent<EnemyAI>();

        // 2. Jika iya (berarti peluru mengenai musuh), berikan damage
        if (hitEnemy != null)
        {
            hitEnemy.TakeDamage(damage);
        }

        // 3. Efek dorongan fisik (opsional, jika musuh punya Rigidbody)
        Rigidbody targetRb = collision.gameObject.GetComponent<Rigidbody>();
        if (targetRb != null)
        {
            Vector3 pushDirection = collision.contacts[0].normal;
            targetRb.AddForce(-pushDirection * hitForce);
        }

        // 4. Hancurkan peluru setelah menabrak
        Destroy(gameObject);
    }
}