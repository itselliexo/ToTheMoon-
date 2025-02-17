using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButtonScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Rigidbody playerRb;
    [SerializeField] GameObject playerSpawnLocation;

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
    }

    public void RestartLevel()
    {
        player.transform.position = playerSpawnLocation.transform.position;
        player.transform.rotation = playerSpawnLocation.transform.rotation;

        playerRb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;

        ShopManager.Instance.shopPanel.SetActive(false);
        playerMovement.fuel = playerMovement.maxFuel;
    }
}
