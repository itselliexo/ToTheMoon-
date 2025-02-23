using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RestartButtonScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Ragdoll ragdoll;
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

        if (ragdoll == null)
        {
            ragdoll = player.GetComponentInChildren<Ragdoll>();

            if (ragdoll == null)
            {
                Debug.Log("initialisation failed");
            }
        }
    }

    public void RestartLevel()
    {
        updateStatUI.dayTracker++;

        player.transform.position = playerSpawnLocation.transform.position;
        player.transform.rotation = playerSpawnLocation.transform.rotation;

        ragdoll.ResetRagdoll();

        playerMovement.emergencyReset = 0;

        playerRb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;
        playerMovement.restartIndicator.SetActive(false);
        ShopManager.Instance.shopPanel.SetActive(false);
        playerMovement.fuel = playerMovement.maxFuel;
        playerMovement.hud.SetActive(true);
    }
}
