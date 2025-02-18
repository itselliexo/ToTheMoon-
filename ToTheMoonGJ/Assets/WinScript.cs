using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HandleWin();
        }
    }

    private void HandleWin()
    {
        Debug.Log("You Win!!!!");
    }
}
