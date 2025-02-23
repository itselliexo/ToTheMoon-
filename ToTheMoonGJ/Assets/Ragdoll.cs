using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    Rigidbody[] ragdollRigidbodies;
    Vector3[] initialPositions;
    Quaternion[] initialRotations;

    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] RootFollow rootFollow;

    void Awake()
    {
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();


        if (playerMovement == null)
        {
            playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

            if (playerMovement == null)
            {
                Debug.Log("playerMovement script not assigned correctly");
            }
        }

        if (rootFollow == null)
        {
            rootFollow = GetComponentInChildren<RootFollow>();

            if (rootFollow == null)
            {
                Debug.Log("RootFollow script not assigned correctly");
            }
        }

        StoreInitialTransforms();

        AttachRagdoll();
    }

    void StoreInitialTransforms()
    {
        initialPositions = new Vector3[ragdollRigidbodies.Length];
        initialRotations = new Quaternion[ragdollRigidbodies.Length];

        for (int i = 0; i < ragdollRigidbodies.Length; i++)
        {
            initialPositions[i] = ragdollRigidbodies[i].transform.position;
            initialRotations[i] = ragdollRigidbodies[i].transform.rotation;
        }
    }

    void AttachRagdoll()
    {
        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = true;
        }
    }

    void Detachragdoll()
    {
        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = false;
        }
    }

    public void ResetRagdoll()
    {
        for (int i = 0; i < ragdollRigidbodies.Length; i++)
        {
            ragdollRigidbodies[i].isKinematic = true;

            ragdollRigidbodies[i].transform.position = initialPositions[i];
            ragdollRigidbodies[i].transform.rotation = initialRotations[i];


            //ragdollRigidbodies[i].velocity = Vector3.zero;
            //ragdollRigidbodies[i].angularVelocity = Vector3.zero;

            ragdollRigidbodies[i].isKinematic = false;

        }
    }

    private void Update()
    {
        if (playerMovement.isRagdoll == true)
        {
            rootFollow.enabled = false;

            Detachragdoll();
        }
        else
        {
            rootFollow.enabled = true;

            AttachRagdoll();
        }
    }
}
