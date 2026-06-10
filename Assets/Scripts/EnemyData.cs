using UnityEngine;

// Membuat cetakan data baru khusus untuk menampung info drop item
[System.Serializable]
public struct DropItem
{
    public string itemName; // Opsional: Hanya agar rapi dan mudah dibaca di Inspector
    public GameObject itemPrefab;
    [Range(0f, 100f)]
    public float dropChance;
}

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Game Data/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("Identitas")]
    public string enemyID;
    public string enemyName;
    public GameObject enemyPrefab;

    [Header("Statistik Pertarungan")]
    public float maxHealth = 100f;
    public float moveSpeed = 3.5f;
    public float attackDamage = 10f;

    [Header("Sistem Drop Item")]
    // Mengganti variabel tunggal menjadi Array dari struct DropItem
    public DropItem[] dropItems;
}