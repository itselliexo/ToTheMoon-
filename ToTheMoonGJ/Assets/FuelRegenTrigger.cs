using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class FuelRegenTrigger : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;

    private void Start()
    {
        if (playerMovement == null)
        {
            playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

            if (playerMovement == null)
            {
                Debug.Log("Initialisation failed");
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerMovement.fuel += Time.deltaTime * 3;

            playerMovement.fuel = Mathf.Clamp(playerMovement.fuel, 0f, playerMovement.maxFuel);

            if (playerMovement.isRagdoll)
            {
                if (Input.GetKey(KeyCode.R))
                {
                    playerMovement.FinalizeRun();

                    playerMovement.restartIndicator.SetActive(false);
                    playerMovement.updateStatUI.UpdateUI();
                    transform.position = playerMovement.playerSpawnLocation.transform.position;
                    transform.rotation = playerMovement.playerSpawnLocation.transform.rotation;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && playerMovement.isRagdoll)
        {
            playerMovement.restartIndicator.SetActive(true);
        }
    }
}
