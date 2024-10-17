using UnityEngine;
using static UnityEditor.Progress;

public class Recipes : MonoBehaviour
{
    private InventoryManager inventoryManager;
    public PlayerStats playerStats;

    // Voeg je ScriptableObjects hier toe via de Unity-editor
    public Item item;

    private void Start()
    {
        inventoryManager = InventoryManager.Instance;
    }

    // Methode om een Health Potion te craften
    public void CraftHealthPotion()
    {
        int requiredMushroom = 2;

        if (HasRequiredMaterials("Mushroom", requiredMushroom))
        {
            if (item == null)
            {
                return;
            }
            RemoveMaterials("Mushroom", requiredMushroom);
            AddCraftedItem();
            Debug.Log("Health Potion has been crafted!");
        }
        else
        {
            Debug.LogError("Niet genoeg mushrooms om een Health Potion te maken!");
        }
    }

    // Methode om een Mana Potion te craften
    public void CraftManaPotion()
    {
        int requiredFlowers = 2;

        if (HasRequiredMaterials("Flower", requiredFlowers))
        {
            RemoveMaterials("Flower", requiredFlowers);
            AddCraftedItem();
            Debug.Log("Mana Potion has been crafted");
        }
        else
        {
            Debug.LogError("Niet genoeg flowers om een Mana Potion te maken!");
        }
    }

    // Methode om de health van de speler te upgraden
    public void UpgradeHealth()
    {
        int requiredHealthPotions = 2;

        if (HasRequiredMaterials("RedPotion", requiredHealthPotions))
        {
            RemoveMaterials("RedPotion", requiredHealthPotions);
            AddCraftedItem() ;
            Debug.Log("HealthBooster is succesvol gecraft!");
        }
        else
        {
            Debug.LogError("Niet genoeg materialen om de health te upgraden!");
        }
    }

    public void UpgradeMana()
    {
        int requiredManaPotions = 2;

        if (HasRequiredMaterials("GreenPotion", requiredManaPotions))
        {
            RemoveMaterials("GreenPotion", requiredManaPotions);
            AddCraftedItem();
            Debug.Log("ManaBooster is succesvol gecraft!");
        }
        else
        {
            Debug.LogError("Niet genoeg materialen om de health te upgraden!");
        }
    }

    // Hulpmethode om te controleren of de speler genoeg van een materiaal heeft
    private bool HasRequiredMaterials(string itemName, int requiredAmount)
    {
        int materialCount = 0;

        foreach (var item in inventoryManager.items)
        {
            if (item.itemName == itemName)
            {
                materialCount++;
            }
        }

        return materialCount >= requiredAmount;
    }

    // Hulpmethode om materialen uit de inventory te verwijderen
    private void RemoveMaterials(string itemName, int amountToRemove)
    {
        int removedCount = 0;

        for (int i = inventoryManager.items.Count - 1; i >= 0 && removedCount < amountToRemove; i--)
        {
            if (inventoryManager.items[i].itemName == itemName)
            {
                inventoryManager.Remove(inventoryManager.items[i]);
                removedCount++;
            }
        }
    }

    // Hulpmethode om een gecraft item toe te voegen aan de inventory
    private void AddCraftedItem()
    {
        

        InventoryManager.Instance.Add(item);
        InventoryManager.Instance.ListItems();
    }
}
