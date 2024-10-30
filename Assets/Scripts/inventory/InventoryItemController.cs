using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    private Item item;  

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

    public void UseItem()
    {
        Debug.Log("Use");
        switch (item.itemType) 
        {
            case Item.ItemType.Health:
                PlayerStats.Instance.HealHealth(item.value);
                break;
            case Item.ItemType.Healthup:
                Debug.Log("Ik werk");
                PlayerStats.Instance.UpgradeStat("health", item.value);
                Debug.Log(PlayerStats.Instance.maxHealth);
                break;
            case Item.ItemType.Mana:
                PlayerStats.Instance.HealMana(item.value);
                break;
            case Item.ItemType.ManaBooster:
                PlayerStats.Instance.UpgradeStat("mana", item.value);
                break;
            default: 
                Debug.Log("Nothing happened");
                break;
        }
        RemoveItem();
    }
}
