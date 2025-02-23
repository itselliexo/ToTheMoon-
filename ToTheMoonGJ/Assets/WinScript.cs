using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour
{
    [SerializeField] GameObject winScreen;
    [SerializeField] PlayerMovement playerMovement;

    private void Start()
    {
        if (winScreen == null)
        {
            winScreen = GameObject.FindGameObjectWithTag("WinScreen");

            if (winScreen == null)
            {
                Debug.Log("no winScreen Initialised");
            }
        }

        if (playerMovement == null)
        {
            playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        }

        winScreen.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HandleWin();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void HandleWin()
    {
        winScreen.SetActive(true);
        playerMovement.isRagdoll = true;
        Time.timeScale = 0;
        Debug.Log("You win");
    }
}
