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

    private void Awake()
    {
        Instance = this; 
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

