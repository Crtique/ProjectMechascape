using System.Collections;
using UnityEngine;

public class PlayerParryingController : MonoBehaviour
{
    [Header("Parrying Controls")]
    [SerializeField] private Collider2D parryCollider;
    [SerializeField] private float parryWindowTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       parryCollider.enabled = false; 
    }

    // Update is called once per frame
    void Update()
    {
        ParryInput();
    }

    // Input system for the parrying system
    void ParryInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(ParryWindow());
            Debug.Log("parry!");
        }
    }

    // Coroutine for the parrying window
    private IEnumerator ParryWindow()
    {
        // make the colider visible when start
        parryCollider.enabled = true;

        // Wait just a few seconds till we can parry again
        yield return new WaitForSeconds(parryWindowTime);
        
        // Reset the parrying window by enabling the collider object that will be hit
        parryCollider.enabled = false;
    }
}
