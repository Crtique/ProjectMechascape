using System.Collections;
using UnityEngine;

public class PlayerParryingController : MonoBehaviour
{
    [Header("Parrying Controls")]
    [SerializeField] private Collider2D KickCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        KickCollider.enabled = false; 
    }

   

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            KickCollider.enabled = true;
            Debug.Log("Kick!");
        }
    }
}
