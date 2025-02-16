using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;
    [SerializeField] PlayerMovement playerMovement;
    public int money;
    [SerializeField] float previousMaxHeight;

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

        if (playerMovement == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            playerMovement = player.GetComponent<PlayerMovement>();
        }
    }

    public void AddMoney(int amount)
    {
        money += amount;
    }

    public bool SpendMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            return true;
        }
        return false;
    }

    public void UpdateMoneyBasedOnHeight(float currentHeight)
    {
        if (currentHeight > previousMaxHeight)
        {
            int earnedMoney = Mathf.FloorToInt((currentHeight - previousMaxHeight) * 2);
            AddMoney(earnedMoney);
            previousMaxHeight = currentHeight;
        }
        else
        {
            int earnedMoney = Mathf.FloorToInt((currentHeight - previousMaxHeight) * 1);
            AddMoney(earnedMoney);
        }
    }
}
