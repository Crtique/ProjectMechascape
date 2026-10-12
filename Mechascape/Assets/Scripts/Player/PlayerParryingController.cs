using System.Collections;
using UnityEngine;

public class PlayerParryingController : MonoBehaviour
{
    [Header("Parrying Controls")]
    [SerializeField] private Collider2D parryCollider;
    [SerializeField] private float parryWindowTime;
    [SerializeField] private float parryingWindowCooldown = 1f;

    private bool canParry = true;
    public bool isParrying;

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
        if (Input.GetKeyDown(KeyCode.F) && canParry && !isParrying)
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

        canParry = false;
        isParrying = true;

        yield return new WaitForSeconds(parryWindowTime);
        isParrying = false;

        // Reset the parrying window by enabling the collider object that will be hit
        parryCollider.enabled = false;

        yield return new WaitForSeconds(parryingWindowCooldown);
        canParry = true;
    }
}
