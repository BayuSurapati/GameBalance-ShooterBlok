using UnityEngine;

// Baris ini membuat menu baru agar kita bisa membuat file data ini dari klik kanan di Unity
[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Game Data/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("Identitas")]
    public string enemyID; // Berguna nanti untuk pencocokan data di CSV
    public string enemyName;
    public GameObject enemyPrefab; // Visual/Model dari musuhnya

    [Header("Statistik Pertarungan")]
    public float maxHealth = 100f;
    public float moveSpeed = 3.5f;
    public float attackDamage = 10f;

    // Anda bisa menambahkan data lain di sini nanti, seperti warna material atau jenis suara
}