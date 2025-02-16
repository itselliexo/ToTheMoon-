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
    [SerializeField] public float maxSpeed;
    [SerializeField] public float jetpackFuel;
    [SerializeField] public float lateralSpeed;
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
    }

    public void UpgradeJetpackPower(float powerIncrease)
    {
        playerMovement.verticalForce += powerIncrease;
        jetpackPower = playerMovement.verticalForce;
    }

    public void UpgradeJetpackFuel(float fuelIncrease)
    {
        jetpackFuel += fuelIncrease;
        playerMovement.maxFuel = jetpackFuel;
        playerMovement.fuel = playerMovement.maxFuel;
    }

    public void UpgradeLateralSpeed(float lateralSpeedIncrease)
    {
        lateralSpeed += lateralSpeedIncrease;
        playerMovement.horizontalMovement = lateralSpeed;
    }
    public void UpgradeMaxSpeed(float maxSpeedIncrease)
    {
        maxSpeed += maxSpeedIncrease;
        playerMovement.maxSpeed = maxSpeed;
    }
}
