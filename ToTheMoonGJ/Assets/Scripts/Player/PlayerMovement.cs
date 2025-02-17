using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private UpdateStatUI updateStatUI;
    [SerializeField] private GameObject shopPanel;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float airTimeRequiredToEndRun;
    [SerializeField] private float airTime;
    [SerializeField] private bool runCanEnd;
    [SerializeField] private bool shopIsOpen;

    [Header("Movement Settings")]
    [SerializeField] public float horizontalMovement;
    [SerializeField] public float verticalForce;
    [SerializeField] public float maxSpeed;
    [SerializeField] public float maxFuel;
    [SerializeField] public float fuel;

    [Header("Player stat stracker")]
    [SerializeField] private float currentHeight;
    [SerializeField] public float maxHeightReached;
        
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fuel = maxFuel;

        if (updateStatUI == null)
        {
            updateStatUI = FindObjectOfType<UpdateStatUI>();

            if(updateStatUI == null)
            {
                Debug.Log("No UI updater in scene");
            }
        }

        if (shopPanel == null)
        {
            shopPanel = GameObject.FindGameObjectWithTag("Shop");

            if (shopPanel == null)
            {
                Debug.Log("No shop found in scene");
            }
        }
    }

    void Update()
    {
        if (!shopIsOpen)
        {
            HandleMovement();
            HandleJetpack();
        }

        TrackMaxHeight();

        if (shopPanel.activeSelf)
        {
            shopIsOpen = true;
        }
        else
        {
            shopIsOpen = false;
        }

        if (!shopIsOpen)
        {
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

            if (runCanEnd)
            {
                rb.constraints = RigidbodyConstraints.FreezePositionZ;
            }
            else
            {
                rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ; ;
            }
        }
        if (shopIsOpen)
        {
            airTime = 0f;
        }
    }

    private void HandleJetpack()
    {
        if (Input.GetKey(KeyCode.W) && fuel > 0)
        {
            rb.AddForce(Vector3.up * verticalForce, ForceMode.Force);
            fuel -= Time.deltaTime;
            fuel = Mathf.Clamp(fuel, 0, maxFuel);
        }
    }
    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.A) && fuel > 0)
        {
            rb.AddForce(new Vector3(-horizontalMovement * 3, 0, 0), ForceMode.Force);
            fuel -= Time.deltaTime;
            fuel = Mathf.Clamp(fuel, 0, maxFuel);
        }
        else if (Input.GetKey(KeyCode.D) && fuel > 0)
        {
            rb.AddForce(new Vector3(horizontalMovement * 3, 0, 0), ForceMode.Force);
            fuel -= Time.deltaTime;
            fuel = Mathf.Clamp(fuel, 0, maxFuel);
        }

        Vector3 velocity = rb.velocity;

        if (rb.velocity.y > maxSpeed)
        {
            velocity.y = Mathf.Clamp(velocity.y, 0, (maxSpeed * 10));
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
            updateStatUI.UpdateUI();
        }

        if (runCanEnd && collision.gameObject.CompareTag("Floor"))
        {
            FinalizeRun();
            runCanEnd = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
            updateStatUI.UpdateUI();
        }

        if (runCanEnd && collision.gameObject.CompareTag("Floor"))
        {
            FinalizeRun();
            runCanEnd = false;
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
        CurrencyManager.Instance.UpdateMoneyBasedOnAirTime(airTime);

        if (runCanEnd)
        {
            if (!shopIsOpen)
            {
                shopPanel.SetActive(true);
                shopIsOpen = true;
            }
        }
    }
}
