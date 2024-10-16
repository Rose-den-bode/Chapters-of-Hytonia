using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interact : MonoBehaviour
{

    public TextMeshProUGUI interactText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Zorg ervoor dat je de juiste tag gebruikt
        {
           interactText.gameObject.SetActive(true); // Toon de pickup tekst
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactText.gameObject.SetActive(false); // Verberg de pickup tekst
        }
    }
}
