using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMover : MonoBehaviour
{
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject newLocation;
    [SerializeField] private string newSpawnTag;

    private void Start()
    {
        if (spawnPoint == null)
        {
            spawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawn");

            if (spawnPoint == null)
            {
                Debug.Log("No spawn point found.");
            }
        }

        if (newLocation == null)
        {
            newLocation = GameObject.FindGameObjectWithTag(newSpawnTag);

            if (newLocation == null)
            {
                Debug.Log("Assignment of new location failed");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 newPosition = newLocation.transform.position;
            newPosition.z = 0f;

            spawnPoint.transform.position = newPosition;
            spawnPoint.transform.rotation = newLocation.transform.rotation;

        }
    }
}
