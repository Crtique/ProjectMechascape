using UnityEngine;

public class WeaponAiming : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;

    private void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg + 180f;

        // return a rotation around the Z axis
        transform.rotation = Quaternion.Euler(0, 0, rotZ);


    }
}
