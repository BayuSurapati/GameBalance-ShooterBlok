using UnityEngine;

public class BillboardEffect : MonoBehaviour
{
    private Transform mainCameraTransform;

    void Start()
    {
        // Mencari transform dari kamera utama saat game dimulai
        if (Camera.main != null)
        {
            mainCameraTransform = Camera.main.transform;
        }
    }

    void LateUpdate()
    {
        if (mainCameraTransform != null)
        {
            // Memaksa objek Canvas ini untuk selalu menghadap ke arah kamera
            transform.LookAt(transform.position + mainCameraTransform.forward);
        }
    }
}