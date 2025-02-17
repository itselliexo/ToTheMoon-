using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] public static ShopManager Instance;

    [SerializeField] public PlayerMovement playerMovement;

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

    [SerializeField] private Button unlockDownThrusters;
    [SerializeField] private Button sellDownThrusters;

    [Header("Upgrade Prices")]
    [SerializeField] public int fuelUpgradeCost;
    [SerializeField] public int powerUpgradeCost;
    [SerializeField] public int lateralSpeedUpgradeCost;
    [SerializeField] public int maxSpeedUpgradeCost;

    [SerializeField] public int downThrusterCost;

    [Header("Upgrade Quantity")]
    [SerializeField] public int fuelUpgradeAmount;
    [SerializeField] public int powerUpgradeAmount;
    [SerializeField] public int lateralSpeedUpgradeAmount;
    [SerializeField] public int maxSpeedUpgradeAmount;

    [SerializeField] public bool dTUnlocked = false;

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

        if (playerMovement == null)
        {
            playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

            if(playerMovement == null)
            {
                Debug.Log("Initialisation failed (playerMovement)");
            }
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

        unlockDownThrusters.onClick.AddListener(() => UnlockDownThrusters(downThrusterCost));
        sellDownThrusters.onClick.AddListener(() => SellDownThrusters(downThrusterCost));
    }

    public void BuyFuelUpgrade(int cost)
    {
        if (CurrencyManager.Instance.money >= cost)
        {
            CurrencyManager.Instance.SpendMoney(cost);

            PlayerUpgrades.Instance.UpgradeJetpackFuel(fuelUpgradeAmount);
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
            PlayerUpgrades.Instance.DowngradeJetpackFuel(fuelUpgradeAmount);
            updateStatsUI.UpdateUI();
        }
    }

    public void BuyPowerUpgrade(int cost)
    {
        if (CurrencyManager.Instance.money >= cost)
        {
            CurrencyManager.Instance.SpendMoney(cost);

            PlayerUpgrades.Instance.UpgradeJetpackPower(powerUpgradeAmount);
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
            PlayerUpgrades.Instance.DowngradeJetpackPower(powerUpgradeAmount);
            updateStatsUI.UpdateUI();
        }
    }

    public void BuySpeedUpgrade(int cost)
    {
        if (CurrencyManager.Instance.money >= cost)
        {
            CurrencyManager.Instance.SpendMoney(cost);

            PlayerUpgrades.Instance.UpgradeLateralSpeed(lateralSpeedUpgradeAmount);
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
            PlayerUpgrades.Instance.DowngradeLateralSpeed(lateralSpeedUpgradeAmount);
            updateStatsUI.UpdateUI();
        }
    }

    public void BuyMaxSpeedUpgrade(int cost)
    {
        if (CurrencyManager.Instance.money >= cost)
        {
            CurrencyManager.Instance.SpendMoney(cost);

            PlayerUpgrades.Instance.UpgradeMaxSpeed(maxSpeedUpgradeAmount);
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
            PlayerUpgrades.Instance.DowngradeMaxSpeed(maxSpeedUpgradeAmount);
            updateStatsUI.UpdateUI();
        }
    }

    public void UnlockDownThrusters(int cost)
    {
        if (dTUnlocked == true)
        {
            Debug.Log("You already own this upgrade");
            return;
        }

        if (CurrencyManager.Instance.money >= cost)
        {
            CurrencyManager.Instance.SpendMoney(cost);

            if (dTUnlocked == false)
            {
                playerMovement.isDownThrustersUnlocked = true;
                dTUnlocked = true;
            }

            Debug.Log("Max Speed Upgrade Purchased!");
            updateStatsUI.UpdateUI();
        }
        else
        {
            Debug.Log("Youre too broke");
        }
    }

    public void SellDownThrusters(int cost)
    {
        if (dTUnlocked == true)
        {
            CurrencyManager.Instance.AddMoney(cost);
            Debug.Log($"Upgrade sold for {cost}");
            playerMovement.isDownThrustersUnlocked = false;
            dTUnlocked = false;
            updateStatsUI.UpdateUI();
        }
    }
}
