using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Data Sumber")]
    public EnemyData myData;

    private NavMeshAgent agent;
    private Transform player;
    private float currentHealth;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (myData != null)
        {
            agent.speed = myData.moveSpeed;
            currentHealth = myData.maxHealth;
            gameObject.name = myData.enemyName;
        }

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player != null && agent.isOnNavMesh)
        {
            agent.SetDestination(player.position);
        }
    }

    // Fungsi baru untuk menerima serangan
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log(gameObject.name + " terkena damage! HP tersisa: " + currentHealth);

        // Cek apakah HP habis
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Fungsi untuk menangani kematian musuh
    private void Die()
    {
        Debug.Log(gameObject.name + " mati!");
        // Menghancurkan objek musuh dari scene
        Destroy(gameObject);
    }
}