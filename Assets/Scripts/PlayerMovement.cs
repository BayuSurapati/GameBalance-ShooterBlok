using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Referensi Komponen")]
    public CharacterController controller;
    public Transform cameraTransform;

    [Header("Input Actions")]
    public InputActionReference moveAction;
    public InputActionReference lookAction;

    [Header("Pengaturan Pergerakan")]
    public float speed = 5f;
    public float gravity = -9.81f;
    private Vector3 velocity;

    [Header("Pengaturan Kamera")]
    public float mouseSensitivity = 15f;
    private float xRotation = 0f;

    void Start()
    {
        // Menyembunyikan dan mengunci kursor mouse di tengah layar
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (controller == null)
            controller = GetComponent<CharacterController>();
    }

    void OnEnable()
    {
        // Wajib mengaktifkan action jika menggunakan InputActionReference
        moveAction.action.Enable();
        lookAction.action.Enable();
    }

    void OnDisable()
    {
        moveAction.action.Disable();
        lookAction.action.Disable();
    }

    void Update()
    {
        Look();
        Move();
    }

    private void Look()
    {
        // Membaca nilai dari input mouse (Vector2)
        Vector2 lookInput = lookAction.action.ReadValue<Vector2>();

        // Mengalikan dengan sensitivity dan Time.deltaTime agar mulus
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        // Menghitung rotasi vertikal (kamera melihat ke atas/bawah)
        xRotation -= mouseY;
        // Membatasi rotasi agar kepala pemain tidak terbalik (clamping)
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Memutar badan pemain secara horizontal (kiri/kanan)
        transform.Rotate(Vector3.up * mouseX);
    }

    private void Move()
    {
        // Membaca nilai dari WASD (Vector2)
        Vector2 moveInput = moveAction.action.ReadValue<Vector2>();

        // Menggerakkan pemain relatif terhadap arah hadapnya saat ini
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * speed * Time.deltaTime);

        // Menerapkan gravitasi sederhana agar pemain menapak di tanah
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Nilai kecil untuk memastikan tetap menempel di tanah
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}