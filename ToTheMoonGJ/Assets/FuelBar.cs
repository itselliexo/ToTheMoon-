using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBar : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] private Slider fuelSlider;

    void Start()
    {
      if (playerMovement == null)
        {
            playerMovement = FindObjectOfType<PlayerMovement>();
        }
    }

    void Update()
    {
        fuelSlider.value = playerMovement.fuel / playerMovement.maxFuel;
    }
}
