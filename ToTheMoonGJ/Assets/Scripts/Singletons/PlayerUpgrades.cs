using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    public static PlayerUpgrades Instance;

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

    public void UpgradeJetpackPower(float powerIncrease)
    {
        jetpackPower += powerIncrease;
    }

    public void UpgradeJetpackFuel(float fuelIncrease)
    {
        jetpackFuel += fuelIncrease;
    }

    public void UpgradeLateralSpeed(float speedIncrease)
    {
        lateralSpeed = +speedIncrease;
    }
}
