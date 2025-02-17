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
    [SerializeField] private GameObject playerSpawnLocation;

    [Header("Movement Settings")]
    [SerializeField] public float horizontalMovement;
    [SerializeField] public float verticalForce;
    [SerializeField] public float maxSpeed;
    [SerializeField] public float maxFuel;
    [SerializeField] public float fuel;
    [SerializeField] private float moveDirection = 0f;
    [SerializeField] private bool isRagdoll = false;
    [SerializeField] private float timeOnObstical;
    [SerializeField] private float resetTime;
    [SerializeField] public bool isDownThrustersUnlocked = false;

    [Header("Player stats tracker")]
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

        if (playerSpawnLocation == null)
        {
            playerSpawnLocation = GameObject.FindGameObjectWithTag("PlayerSpawn");
        }
    }

    void Update()
    {
        if (!shopIsOpen && !isRagdoll)
        {
            HandleMovement();
            HandleRotation();
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
                rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
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
        else if (Input.GetKey(KeyCode.S) && fuel > 0)
        {
            if (isDownThrustersUnlocked == true)
            {
                rb.AddForce(-Vector3.up * verticalForce / 2, ForceMode.Force);
                fuel -= Time.deltaTime;
                fuel = Mathf.Clamp(fuel, 0, maxFuel);
            }
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

        /*if (Input.GetKey(KeyCode.A))
        {
            moveDirection = -1f;
        }
        else
        {
            moveDirection = 0f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection = 1f;
        }
        else
        {
            moveDirection = 0f;
        }*/

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

    private void HandleRotation()
    {
        if (!isRagdoll)
        {
            moveDirection = Input.GetKey(KeyCode.A) ? -1 : Input.GetKey(KeyCode.D) ? 1f : 0f;
            Quaternion targetRotation;
            if (moveDirection != 0)
            {
                targetRotation = Quaternion.Euler(0, moveDirection == 1 ? -40 : 40, 0);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10);
            }
            if (moveDirection == 0)
            {
                targetRotation = Quaternion.Euler(0, 0, 0);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10);
            }
        }
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

        if (collision.gameObject.CompareTag("Obstical"))
        {
            if (runCanEnd)
            {
                isRagdoll = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
            updateStatUI.UpdateUI();

            if(runCanEnd)
            {
                FinalizeRun();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Obstical"))
        {
            isGrounded = true;
            timeOnObstical += Time.deltaTime;
            if (timeOnObstical >= resetTime)
            {
                runCanEnd = true;
                FinalizeRun();
                updateStatUI.UpdateUI();
                transform.position = playerSpawnLocation.transform.position;
                transform.rotation = playerSpawnLocation.transform.rotation;
            }
            if (runCanEnd)
            {
                isRagdoll = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }

        if (collision.gameObject.CompareTag("Obstical"))
        {
            timeOnObstical = 0f;
        }
    }

    public void FinalizeRun()
    {
        if (!runCanEnd) return;

        CurrencyManager.Instance.UpdateMoneyBasedOnHeight(maxHeightReached);
        CurrencyManager.Instance.UpdateMoneyBasedOnAirTime(airTime);

        runCanEnd = false;

        isRagdoll = false;

        if (!shopIsOpen)
        {
            shopPanel.SetActive(true);
            shopIsOpen = true;
        }
    }
}
