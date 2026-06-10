using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Data Sumber")]
    public EnemyData myData;

    private NavMeshAgent agent;
    private Transform currentTarget;
    private float currentHealth;

    [Header("Pengaturan Serangan")]
    public float attackRate = 1f;
    private float attackTimer = 0f;
    // 1. Tambahkan variabel Jangkauan Serangan baru
    public float attackRange = 2.5f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (myData != null)
        {
            agent.speed = myData.moveSpeed;
            currentHealth = myData.maxHealth;
            gameObject.name = myData.enemyName;
        }

        ChooseObjective();
    }

    void Update()
    {
        if (currentTarget != null && agent.isOnNavMesh)
        {
            agent.SetDestination(currentTarget.position);

            float distanceToTarget = Vector3.Distance(transform.position, currentTarget.position);

            // 2. Ganti logika pengecekan jarak menjadi menggunakan attackRange
            if (distanceToTarget <= attackRange)
            {
                AttackTarget();
            }
        }
    }

    private void AttackTarget()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackRate)
        {
            ObjectiveHealth objectiveHealth = currentTarget.GetComponent<ObjectiveHealth>();

            if (objectiveHealth != null)
            {
                float damageToDeal = (myData != null) ? myData.attackDamage : 10f;
                objectiveHealth.TakeDamage(damageToDeal);
            }

            attackTimer = 0f;
        }
    }

    private void ChooseObjective()
    {
        GameObject[] objectives = GameObject.FindGameObjectsWithTag("Objective");

        if (objectives.Length > 0)
        {
            int randomIndex = Random.Range(0, objectives.Length);
            currentTarget = objectives[randomIndex].transform;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Tambahkan skor kill ke UI
        if (UIManager.instance != null)
        {
            UIManager.instance.AddKill();
        }

        CalculateDropItem();
        Destroy(gameObject);
    }

    private void CalculateDropItem()
    {
        // Pastikan EnemyData ada dan array dropItems tidak kosong
        if (myData != null && myData.dropItems.Length > 0)
        {
            // Melakukan looping untuk mengecek SETIAP item di dalam daftar
            foreach (DropItem drop in myData.dropItems)
            {
                // Pastikan prefab item tidak kosong untuk menghindari error
                if (drop.itemPrefab != null)
                {
                    // Lempar dadu acak untuk item ini (0 sampai 100)
                    float randomRoll = Random.Range(0f, 100f);

                    // Jika angka dadu masuk ke dalam peluang drop item ini
                    if (randomRoll <= drop.dropChance)
                    {
                        Debug.Log("Great " + gameObject.name + " menjatuhkan " + drop.itemName + "!");

                        // Memunculkan item ini. 
                        Instantiate(drop.itemPrefab, transform.position + Vector3.up * 0.2f, Quaternion.identity);
                    }
                }
            }
        }
    }
}