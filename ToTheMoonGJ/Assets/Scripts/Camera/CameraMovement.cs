using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject player;

    [Header("Settings")]
    private Vector3 playerOffset;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Ragdoll");

            if (player == null)
            {
                Debug.LogError("No player found in scene");
            }
        }
        playerOffset = new Vector3(0, 1, -5);
    }

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 targetPosition = new Vector3(player.transform.position.x + playerOffset.x, player.transform.position.y + playerOffset.y, player.transform.position.z + playerOffset.z);

            transform.position = targetPosition;

        }
    }
}
