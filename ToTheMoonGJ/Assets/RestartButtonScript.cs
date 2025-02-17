using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RestartButtonScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Rigidbody playerRb;
    [SerializeField] GameObject playerSpawnLocation;
    [SerializeField] UpdateStatUI updateStatUI;
    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");

            if (playerRb == null)
            {
                playerRb = player.GetComponent<Rigidbody>();
            }
        }
        else
        {
            Debug.Log("No player found in scene");
        }

        if (playerSpawnLocation == null)
        {
            playerSpawnLocation = GameObject.FindGameObjectWithTag("PlayerSpawn");
        }

        if (playerMovement == null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();

            if (playerMovement == null)
            {
                Debug.Log("?");
            }
        }

        if (updateStatUI == null)
        {
            GameObject uiManager = GameObject.FindGameObjectWithTag("UpdateStatUI");
            updateStatUI = uiManager.GetComponent<UpdateStatUI>();

            if(updateStatUI == null)
            {
                Debug.Log("no updateStatUI assigned");
            }
        }
    }

    public void RestartLevel()
    {
        updateStatUI.dayTracker++;

        playerRb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;

        ShopManager.Instance.shopPanel.SetActive(false);
        playerMovement.fuel = playerMovement.maxFuel;
    }
}
