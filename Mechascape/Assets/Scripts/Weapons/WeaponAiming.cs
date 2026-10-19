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

        Vector2 rotation = (mousePos - transform.position).normalized;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        // return a rotation around the Z axis
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
        FixWeaponFlip(rotZ);


    }

    void FixWeaponFlip(float angle)
    {
        // If aiming to the left, flip the gun vertically
        if (angle > 90 || angle < -90)
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
        // If aiming to the right, keep it normal
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
