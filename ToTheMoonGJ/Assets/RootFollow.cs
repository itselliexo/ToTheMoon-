using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootFollow : MonoBehaviour
{
    [SerializeField] Transform root;
    [SerializeField] Vector3 playerOffset = new Vector3(0, 0, -0.5f);

    private void Start()
    {
        if (root == null)
        {
            root = transform.parent?.parent;

            if (root == null)
            {
                Debug.Log("Not enough parent levels in hierarchy!");
            }
        }

    }

    private void Update()
    {
        transform.localPosition = playerOffset;
        //transform.rotation = root.rotation;
    }
}
