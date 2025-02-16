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

    private void OnEnable()
    {
        UpdateUI();
    }
    public void UpdateUI()
    {
        if (PlayerUpgrades.Instance != null)
        {
            powerText.text = PlayerUpgrades.Instance.jetpackPower.ToString();
            fuelText.text = PlayerUpgrades.Instance.jetpackFuel.ToString();
            lateralSpeedText.text = PlayerUpgrades.Instance.lateralSpeed.ToString();
            maxSpeedText.text = PlayerUpgrades.Instance.maxSpeed.ToString();
        }

        if (CurrencyManager.Instance != null)
        {
            moneyText.text = CurrencyManager.Instance.money.ToString();
        }
    }
}
