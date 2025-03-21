using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class basicChest : MonoBehaviour, IInteractable
{
    private GameObject HUD;
    private HUD HUDScript;
    public TextMeshProUGUI interactText;
    public GameObject father;

    public void Start()
    {
        HUD = GameObject.Find("HUD");
        if (HUD == null)
        {
            Debug.LogError("HUD niet gevonden in de scene!");
        }
        HUDScript = HUD.GetComponent<HUD>();
    }
    public void Interact()
    {
        float loot = Mathf.Round(Random.Range(9001, 9999));
        HUDScript.AddGold(loot);
        Destroy(father);
        interactText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactText.gameObject.SetActive(false);
        }
    }
}