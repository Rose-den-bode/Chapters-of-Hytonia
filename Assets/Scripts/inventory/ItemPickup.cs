using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemPickup : MonoBehaviour 
{
    public Item item;
    private Boolean isInRange;
    public TextMeshProUGUI pickupText;

    void Start()
    {

        // Zorg ervoor dat de pickup tekst in het begin verborgen is
        pickupText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E)) 
        {
            Pickup();
        }
    }

    void Pickup()
    {
        InventoryManager.Instance.Add(item);
        Destroy(gameObject);
        InventoryManager.Instance.ListItems();
        pickupText.gameObject.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Zorg ervoor dat je de juiste tag gebruikt
        {
            isInRange = true; // Speler is in het bereik
            pickupText.gameObject.SetActive(true); // Toon de pickup tekst
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false; // Speler is niet meer in het bereik
            pickupText.gameObject.SetActive(false); // Verberg de pickup tekst
        }
    }
}
