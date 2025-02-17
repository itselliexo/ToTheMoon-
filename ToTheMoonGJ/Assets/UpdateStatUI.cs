using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateStatUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI powerText;
    [SerializeField] private TextMeshProUGUI fuelText;
    [SerializeField] private TextMeshProUGUI lateralSpeedText;
    [SerializeField] private TextMeshProUGUI maxSpeedText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] public int dayTracker;

    [Header("Costs")]
    [SerializeField] private TextMeshProUGUI powerCostText;
    [SerializeField] private TextMeshProUGUI fuelCostText;
    [SerializeField] private TextMeshProUGUI lateralSpeedCostText;
    [SerializeField] private TextMeshProUGUI maxSpeedCostText;

    [Header("Sell Value")]
    [SerializeField] private TextMeshProUGUI powerSellText;
    [SerializeField] private TextMeshProUGUI fuelSellText;
    [SerializeField] private TextMeshProUGUI lateralSpeedSellText;
    [SerializeField] private TextMeshProUGUI maxSpeedSellText;

    private void OnEnable()
    {
        UpdateUI();
    }
    public void UpdateUI()
    {
        dayText.text = dayTracker.ToString();

        if (PlayerUpgrades.Instance != null)
        {
            powerText.text = PlayerUpgrades.Instance.jetpackPower.ToString();
            fuelText.text = PlayerUpgrades.Instance.jetpackFuel.ToString();
            lateralSpeedText.text = PlayerUpgrades.Instance.lateralSpeed.ToString();
            maxSpeedText.text = PlayerUpgrades.Instance.maxSpeed.ToString();
        }

        if (CurrencyManager.Instance != null && ShopManager.Instance != null)
        {
            moneyText.text = CurrencyManager.Instance.money.ToString();
            powerCostText.text = "-" + ShopManager.Instance.powerUpgradeCost.ToString();
            fuelCostText.text = "-" + ShopManager.Instance.fuelUpgradeCost.ToString();
            lateralSpeedCostText.text = "-" + ShopManager.Instance.lateralSpeedUpgradeCost.ToString();
            maxSpeedCostText.text = "-" + ShopManager.Instance.maxSpeedUpgradeCost.ToString();

            powerSellText.text = "+" + ShopManager.Instance.powerUpgradeCost.ToString();
            fuelSellText.text = "+" + ShopManager.Instance.fuelUpgradeCost.ToString();
            lateralSpeedSellText.text = "+" + ShopManager.Instance.lateralSpeedUpgradeCost.ToString();
            maxSpeedSellText.text = "+" + ShopManager.Instance.maxSpeedUpgradeCost.ToString();
        }
        else
        {
            Debug.LogError("ShopManager.Instance or CurrencyManager.Instance is null!");
        }
    }
}
