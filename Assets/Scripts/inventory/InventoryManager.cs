using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    public Toggle enableRemove;

    public InventoryItemController[] inventoryItems;

    public GameObject Inventory;

    private void Awake()
    {
        Instance = this; 
    }

    private void Update()
    {
        // Controleer of de I-toets wordt ingedrukt om de inventory te toggelen
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(GameManager.instance.isMenuOpen == false || GameManager.instance.isMenuOpen == true && Inventory.activeSelf)
            {
                ActivateInventory();
            }
            else
            {
                Debug.Log("Je kan inventaris niet openen");
            }
        }


    }

    public void ActivateInventory()
    {
        GameManager.instance.ToggleBool();
        bool isActive = !Inventory.activeSelf; // Bepaal of de inventory nu actief moet worden
        ListItems();
        Inventory.SetActive(isActive); // Wissel de actieve status van de inventory

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

        if (!Inventory.activeSelf)
        {
            Cursor.lockState = CursorLockMode.Locked; // Lock de muis naar het midden
            Cursor.visible = false; // Maak de muis onzichtbaar
        }
    }

    public void Add(Item item)
    {
        items.Add(item);
    }
    public void Remove(Item item)
    {
        items.Remove(item);
    }

    public void ListItems()
    {
        // Verwijder bestaande UI-items
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        // Doorloop alle items in de lijst en toon ze in de UI
        foreach (var item in items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var controller = obj.GetComponent<InventoryItemController>(); // Verkrijg de InventoryItemController 

            // Stel het item in
            controller.AddItem(item);

            var itemNameTransform = obj.transform.Find("ItemName");
            var itemIconTransform = obj.transform.Find("ItemIcon");
            var removeButton = obj.transform.Find("Remove").GetComponent<Button>();

            var itemName = itemNameTransform.GetComponent<TextMeshProUGUI>();
            var itemIcon = itemIconTransform.GetComponent<Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;

            if (enableRemove.isOn)
                removeButton.gameObject.SetActive(true);
            else
                removeButton.gameObject.SetActive(false);
        }

        SetInventoryItems(); // Update de lijst van inventory items in de UI
    }


    public void EnableItemsRemove()
        {
            if (enableRemove.isOn)
            {
                foreach(Transform item in ItemContent)
                {
                    item.Find("Remove").gameObject.SetActive(true);
                }
            }
            else
            {
                foreach (Transform item in ItemContent)
                {
                    item.Find("Remove").gameObject.SetActive(false);
                }
            }
        }

    public void SetInventoryItems()
    {
        inventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();
    
        for(int i = 0; i < items.Count; i++)
        {
            inventoryItems[i].AddItem(items[i]);
        }
    }


    }

