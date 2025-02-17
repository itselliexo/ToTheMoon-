using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] public static ShopManager Instance;

    [Header("UI References")]
    [SerializeField] public GameObject shopPanel;
    [SerializeField] private UpdateStatUI updateStatsUI;
    [SerializeField] private Button fuelUpgradeButton;
    [SerializeField] private Button powerUpgradeButton;
    [SerializeField] private Button speedUpgradeButton;
    [SerializeField] private Button maxSpeedUpgradeButton;

    [SerializeField] private Button fuelSellButton;
    [SerializeField] private Button powerSellButton;
    [SerializeField] private Button speedSellButton;
    [SerializeField] private Button maxSpeedSellButton;

    [Header("Upgrade Prices")]
    [SerializeField] public int fuelUpgradeCost;
    [SerializeField] public int powerUpgradeCost;
    [SerializeField] public int lateralSpeedUpgradeCost;
    [SerializeField] public int maxSpeedUpgradeCost;

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

    void Start()
    {
        if (shopPanel == null)
        {
            shopPanel = GameObject.FindGameObjectWithTag("Shop");
        }
        shopPanel.SetActive(false);
        fuelUpgradeButton.onClick.AddListener(() => BuyFuelUpgrade(fuelUpgradeCost));
        powerUpgradeButton.onClick.AddListener(() => BuyPowerUpgrade(powerUpgradeCost));
        speedUpgradeButton.onClick.AddListener(() => BuySpeedUpgrade(lateralSpeedUpgradeCost));
        maxSpeedUpgradeButton.onClick.AddListener(() => BuyMaxSpeedUpgrade(maxSpeedUpgradeCost));

        fuelSellButton.onClick.AddListener(() => SellFuelUpgrade(fuelUpgradeCost));
        powerSellButton.onClick.AddListener(() => SellPowerUpgrade(powerUpgradeCost));
        speedSellButton.onClick.AddListener(() => SellSpeedUpgrade(lateralSpeedUpgradeCost));
        maxSpeedSellButton.onClick.AddListener(() => SellMaxSpeedUpgrade(maxSpeedUpgradeCost));
    }

    public void BuyFuelUpgrade(int cost)
    {
        if (CurrencyManager.Instance.money >= cost)
        {
            CurrencyManager.Instance.SpendMoney(cost);

            PlayerUpgrades.Instance.UpgradeJetpackFuel(1);
            Debug.Log("Fuel Upgrade Purchased!");
            updateStatsUI.UpdateUI();
        }
        else
        {
            Debug.Log("Youre too broke");
        }
    }

    public void SellFuelUpgrade(int cost)
    {
        if (PlayerUpgrades.Instance.canSellFuel)
        {
            CurrencyManager.Instance.AddMoney(cost);
            Debug.Log($"Upgrade sold for {cost}");
            PlayerUpgrades.Instance.DowngradeJetpackFuel(1);
            updateStatsUI.UpdateUI();
        }
    }

    public void BuyPowerUpgrade(int cost)
    {
        if (CurrencyManager.Instance.money >= cost)
        {
            CurrencyManager.Instance.SpendMoney(cost);

            PlayerUpgrades.Instance.UpgradeJetpackPower(1);
            Debug.Log("Fuel Upgrade Purchased!");
            updateStatsUI.UpdateUI();
        }
        else
        {
            Debug.Log("Youre too broke");
        }
    }
    public void SellPowerUpgrade(int cost)
    {
        if (PlayerUpgrades.Instance.canSellPower)
        {
            CurrencyManager.Instance.AddMoney(cost);
            Debug.Log($"Upgrade sold for {cost}");
            PlayerUpgrades.Instance.DowngradeJetpackPower(1);
            updateStatsUI.UpdateUI();
        }
    }

    public void BuySpeedUpgrade(int cost)
    {
        if (CurrencyManager.Instance.money >= cost)
        {
            CurrencyManager.Instance.SpendMoney(cost);

            PlayerUpgrades.Instance.UpgradeLateralSpeed(1);
            Debug.Log("Fuel Upgrade Purchased!");
            updateStatsUI.UpdateUI();
        }
        else
        {
            Debug.Log("Youre too broke");
        }
    }
    public void SellSpeedUpgrade(int cost)
    {
        if (PlayerUpgrades.Instance.canSellLatSpeed)
        {
            CurrencyManager.Instance.AddMoney(cost);
            Debug.Log($"Upgrade sold for {cost}");
            PlayerUpgrades.Instance.DowngradeLateralSpeed(1);
            updateStatsUI.UpdateUI();
        }
    }

    public void BuyMaxSpeedUpgrade(int cost)
    {
        if (CurrencyManager.Instance.money >= cost)
        {
            CurrencyManager.Instance.SpendMoney(cost);

            PlayerUpgrades.Instance.UpgradeMaxSpeed(1);
            Debug.Log("Max Speed Upgrade Purchased!");
            updateStatsUI.UpdateUI();
        }
        else
        {
            Debug.Log("Youre too broke");
        }
    }
    public void SellMaxSpeedUpgrade(int cost)
    {
        if (PlayerUpgrades.Instance.canSellMaxSpeed)
        {
            CurrencyManager.Instance.AddMoney(cost);
            Debug.Log($"Upgrade sold for {cost}");
            PlayerUpgrades.Instance.DowngradeMaxSpeed(1);
            updateStatsUI.UpdateUI();
        }
    }
}
