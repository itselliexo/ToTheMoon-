using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float airTimeRequiredToEndRun;
    [SerializeField] private float airTime;
    [SerializeField] private bool runCanEnd;

    [Header("Movement Settings")]
    [SerializeField] private float horizontalMovement;
    [SerializeField] private float verticalForce;
    [SerializeField] public float maxSpeed;
    [SerializeField] public float maxFuel;
    [SerializeField] private float fuel;

    [Header("Player stat stracker")]
    [SerializeField] private float currentHeight;
    [SerializeField] public float maxHeightReached;
        
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleJetpack();
        HandleMovement();
        TrackMaxHeight();

        if (!isGrounded)
        {
            airTime += Time.deltaTime;
        }

        if (airTime >= airTimeRequiredToEndRun)
        {
            runCanEnd = true;
        }
        else
        {
            runCanEnd = false;
        }
    }

    private void HandleJetpack()
    {
        if (Input.GetKey(KeyCode.Space) && fuel > 0)
        {
            rb.AddForce(Vector3.up * verticalForce, ForceMode.Acceleration);
            fuel -= Time.deltaTime;
            fuel = Mathf.Clamp(fuel, 0, maxFuel);
        }
    }
    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.A) && fuel > 0)
        {
            rb.AddForce(new Vector3(-horizontalMovement, 0, 0), ForceMode.Impulse);
            fuel -= Time.deltaTime;
            fuel = Mathf.Clamp(fuel, 0, maxFuel);
        }

        if (Input.GetKey(KeyCode.D) && fuel > 0)
        {
            rb.AddForce(new Vector3(horizontalMovement, 0, 0), ForceMode.Impulse);
            fuel -= Time.deltaTime;
            fuel = Mathf.Clamp(fuel, 0, maxFuel);
        }

        Vector3 velocity = rb.velocity;

        if (rb.velocity.y > maxSpeed)
        {
            velocity.y = Mathf.Clamp(velocity.y, 0, maxSpeed);
        }

        if (Mathf.Abs(velocity.x) > maxSpeed)
        {
            velocity.x = Mathf.Sign(velocity.x) * maxSpeed;
        }
        rb.velocity = velocity;
    }

    private void TrackMaxHeight()
    {
        currentHeight = transform.position.y;

        if (currentHeight > maxHeightReached)
        {
            maxHeightReached = currentHeight;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }

        if (runCanEnd && collision.gameObject.CompareTag("Floor"))
        {
            FinalizeRun();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }

        if (runCanEnd && collision.gameObject.CompareTag("Floor"))
        {
            FinalizeRun();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }
    }

    public void FinalizeRun()
    {
        CurrencyManager.Instance.UpdateMoneyBasedOnHeight(maxHeightReached);
    }
}
