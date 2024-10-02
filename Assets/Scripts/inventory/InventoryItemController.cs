using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    private Item item;

    public Button Remove;
    private void Start()
    {
        Remove.onClick.AddListener(RemoveItem);
    }

    public void RemoveItem()
    {
        Debug.Log("Verwijder item: " + item.itemName);
        InventoryManager.Instance.Remove(item); // Verwijder het item uit de lijst
        Destroy(gameObject); // Verwijder het UI-object
        InventoryManager.Instance.ListItems(); // Herlaad de inventaris UI
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
    }
}
