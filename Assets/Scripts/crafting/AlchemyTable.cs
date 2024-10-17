using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemyTable : MonoBehaviour
{
    public GameObject craftingMenu;
    private Boolean isInRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E) )
        {
            if(GameManager.instance.isMenuOpen ==  false || GameManager.instance.isMenuOpen == true && craftingMenu.activeSelf)
            {
                ActivateCrafting();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Zorg ervoor dat je de juiste tag gebruikt
        {
            isInRange = true; // Speler is in het bereik
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false; // Speler is niet meer in het bereik
        }
    }
    

    public void ActivateCrafting()
    {
        bool isActive = !craftingMenu.activeSelf; // Bepaal of de inventory nu actief moet worden
        craftingMenu.SetActive(isActive); // Wissel de actieve status van de inventory

        // Pauze de game of hervat deze
        if (isActive)
        {
            Time.timeScale = 0; // Pauze de game
            Cursor.lockState = CursorLockMode.None; // Ontgrendel de muis
            Cursor.visible = true; // Maak de muis zichtbaar
        }
        else
        {
            Time.timeScale = 1; // Hervat de game
            Cursor.lockState = CursorLockMode.Locked; // Lock de muis naar het midden
            Cursor.visible = false; // Maak de muis onzichtbaar
        }

        if (!craftingMenu.activeSelf)
        {
            Cursor.lockState = CursorLockMode.Locked; // Lock de muis naar het midden
            Cursor.visible = false; // Maak de muis onzichtbaar
        }
    }

    // Update is called once per frame
    
}
