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
        //Debug.Log($"{jetpackFuel}, {jetpackFuel}, {lateralSpeed}");
        updateStatUI.UpdateUI();
    }

    public void UpgradeJetpackPower(float powerIncrease)
    {
        jetpackPower += powerIncrease;
        playerMovement.verticalForce = jetpackPower;
    }

    public void UpgradeJetpackFuel(float fuelIncrease)
    {
        jetpackFuel += fuelIncrease;
        playerMovement.maxFuel = jetpackFuel;
        playerMovement.fuel = playerMovement.maxFuel;
    }

    public void UpgradeLateralSpeed(float speedIncrease)
    {
        lateralSpeed += speedIncrease;
        playerMovement.horizontalMovement = lateralSpeed;
    }
}
