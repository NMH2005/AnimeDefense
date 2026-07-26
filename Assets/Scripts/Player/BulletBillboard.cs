using UnityEngine;

public class BulletBillboard : MonoBehaviour
{
    private Transform cam;

    private void Start()
    {
        cam = Camera.main.transform;
    }

    private void LateUpdate()
    {
        transform.rotation = cam.rotation * Quaternion.Euler(0,180,0) ;
    }
}
