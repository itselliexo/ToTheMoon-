using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private UpdateStatUI updateStatsUI;
    [SerializeField] private Button fuelUpgradeButton;
    [SerializeField] private Button powerUpgradeButton;
    [SerializeField] private Button speedUpgradeButton;

    [Header("Upgrade Prices")]
    [SerializeField] private int fuelUpgradeCost;
    [SerializeField] private int powerUpgradeCost;
    [SerializeField] private int speedUpgradeCost;

    void Start()
    {
        fuelUpgradeButton.onClick.AddListener(() => BuyFuelUpgrade(fuelUpgradeCost));
        powerUpgradeButton.onClick.AddListener(() => BuyPowerUpgrade(powerUpgradeCost));
        speedUpgradeButton.onClick.AddListener(() => BuySpeedUpgrade(speedUpgradeCost));
    }

    public void BuyFuelUpgrade(int cost)
    {
        if (CurrencyManager.Instance.money >= cost)
        {
            CurrencyManager.Instance.SpendMoney(cost);

            PlayerUpgrades.Instance.UpgradeJetpackFuel(10);
            Debug.Log("Fuel Upgrade Purchased!");
            updateStatsUI.UpdateUI();
        }
    }

    public void BuyPowerUpgrade(int cost)
    {
        if (CurrencyManager.Instance.money >= cost)
        {
            CurrencyManager.Instance.SpendMoney(cost);

            PlayerUpgrades.Instance.UpgradeJetpackPower(3);
            Debug.Log("Fuel Upgrade Purchased!");
            updateStatsUI.UpdateUI();
        }
    }

    public void BuySpeedUpgrade(int cost)
    {
        if (CurrencyManager.Instance.money >= cost)
        {
            CurrencyManager.Instance.SpendMoney(cost);

            PlayerUpgrades.Instance.UpgradeLateralSpeed(3);
            Debug.Log("Fuel Upgrade Purchased!");
            updateStatsUI.UpdateUI();
        }
    }
}
