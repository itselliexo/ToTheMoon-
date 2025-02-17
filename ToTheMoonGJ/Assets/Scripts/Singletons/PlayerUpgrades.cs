using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    public static PlayerUpgrades Instance;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private UpdateStatUI updateStatUI;

    [Header("Jetpack Upgrades")]
    [SerializeField] public float jetpackPower;
    [SerializeField] public float jetpackFuel;
    [SerializeField] public float lateralSpeed;
    [SerializeField] public float maxSpeed;

    [SerializeField] public bool canSellPower;
    [SerializeField] public bool canSellFuel;
    [SerializeField] public bool canSellLatSpeed;
    [SerializeField] public bool canSellMaxSpeed;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (playerMovement == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            playerMovement = player.GetComponent<PlayerMovement>();
        }

        if (updateStatUI == null)
        {
            GameObject manager = GameObject.FindGameObjectWithTag("UpdateStatUI");
            updateStatUI = manager.GetComponent<UpdateStatUI>();
        }

        jetpackPower = playerMovement.verticalForce;
        jetpackFuel = playerMovement.maxFuel;
        lateralSpeed = playerMovement.horizontalMovement;
        maxSpeed = playerMovement.maxSpeed;
        updateStatUI.UpdateUI();
        UpdateSellStatus();
    }

    public void UpgradeJetpackPower(float powerIncrease)
    {
        playerMovement.verticalForce += (powerIncrease / 10);
        jetpackPower = playerMovement.verticalForce;
        UpdateSellStatus();
    }
    public void DowngradeJetpackPower(float powerDecrease)
    {
        if (canSellPower)
        {
            playerMovement.verticalForce -= (powerDecrease / 10);
            playerMovement.verticalForce = Mathf.Clamp(playerMovement.verticalForce, 1, Mathf.Infinity);
            jetpackPower = playerMovement.verticalForce;
            UpdateSellStatus();
        }
    }

    public void UpgradeJetpackFuel(float fuelIncrease)
    {
        jetpackFuel += fuelIncrease;
        playerMovement.maxFuel = jetpackFuel;
        playerMovement.fuel = playerMovement.maxFuel;
        UpdateSellStatus();
    }
    public void DowngradeJetpackFuel(float fuelDecrease)
    {
        if (canSellFuel)
        {
            jetpackFuel = Mathf.Max(jetpackFuel - fuelDecrease, 1);
            playerMovement.maxFuel = jetpackFuel;
            playerMovement.fuel = playerMovement.maxFuel;
        }
        UpdateSellStatus();
    }

    public void UpgradeLateralSpeed(float lateralSpeedIncrease)
    {
        lateralSpeed += lateralSpeedIncrease;
        playerMovement.horizontalMovement = lateralSpeed;
        UpdateSellStatus();
    }
    public void DowngradeLateralSpeed(float lateralSpeedDecrease)
    {
        if (canSellLatSpeed)
        {
            lateralSpeed = Mathf.Max(lateralSpeed - lateralSpeedDecrease, 1);
            playerMovement.horizontalMovement = lateralSpeed;
        }
        UpdateSellStatus();
    }
    public void UpgradeMaxSpeed(float maxSpeedIncrease)
    {
        maxSpeed += (maxSpeedIncrease / 10);
        playerMovement.maxSpeed = maxSpeed;
        UpdateSellStatus();
    }
    public void DowngradeMaxSpeed(float maxSpeedDecrease)
    {
        if (canSellMaxSpeed)
        {
            maxSpeed = Mathf.Max(maxSpeed - maxSpeedDecrease / 10, 1);
            playerMovement.maxSpeed = maxSpeed;
        }
        UpdateSellStatus();
    }

    private void UpdateSellStatus()
    {
        if (playerMovement.verticalForce > 1)
        {
            canSellPower = true;
        }
        else
        {
            canSellPower = false;
        }

        if (playerMovement.maxFuel > 1)
        {
            canSellFuel = true;
        }
        else
        {
            canSellFuel = false;
        }

        if (playerMovement.horizontalMovement > 1)
        {
            canSellLatSpeed = true;
        }
        else
        {
            canSellLatSpeed = false;
        }

        if (playerMovement.maxSpeed > 1)
        {
            canSellMaxSpeed = true;
        }
        else
        {
            canSellMaxSpeed = false;
        }
    }
}
