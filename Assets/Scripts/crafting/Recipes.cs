using UnityEngine;

public class Recipes : MonoBehaviour
{
    private InventoryManager inventoryManager;
    public PlayerStats playerStats;

    // Voeg je ScriptableObjects hier toe via de Unity-editor
    public Item item;

    // Referentie naar het CraftingLog script om berichten toe te voegen aan de ScrollView
    public CraftingLog craftingLog;

    private HUD hud;

    private void Start()
    {
        inventoryManager = InventoryManager.Instance;
        hud = GameObject.Find("HUD").GetComponent<HUD>();
    }

    // Methode om een Health Potion te craften
    public void CraftHealthPotion(int cost)
    {
        if (hud.gold >= cost)
        {
            int items = 2;

            if (HasRequiredMaterials("Mushroom", items))
            {
                if (item == null)
                {
                    return;
                }
                RemoveMaterials("Mushroom", items);
                AddCraftedItem();
                hud.gold -= cost;
                hud.UpdateGold();

                // Voeg een bericht toe aan de crafting log
                craftingLog.AddCraftingMessage("Health Potion has been crafted!", "#00FF00");
            }
            else
            {
                // Voeg een foutmelding toe aan de crafting log
                craftingLog.AddCraftingMessage("Niet genoeg mushrooms om een Health Potion te maken!", "#FF0000");
            }
        }
        else
        {
            craftingLog.AddCraftingMessage("Niet genoeg geld om een Health Potion te maken!", "#FF0000");
        }
    }

    // Methode om een Mana Potion te craften
    public void CraftManaPotion(int cost)
    {
        if (hud.gold >= cost)
        {
            int items = 2;

            if (HasRequiredMaterials("Flower", items))
            {
                RemoveMaterials("Flower", items);
                AddCraftedItem();
                hud.gold -= cost;
                hud.UpdateGold();

                // Voeg een bericht toe aan de crafting log
                craftingLog.AddCraftingMessage("Mana Potion has been crafted", "#00FF00");
            }
            else
            {
                // Voeg een foutmelding toe aan de crafting log
                craftingLog.AddCraftingMessage("Niet genoeg flowers om een Mana Potion te maken!", "#FF0000");
            }
        }
        else
        {
            craftingLog.AddCraftingMessage("Niet genoeg geld om een Mana Potion te maken!", "#FF0000");
        }

    }

    // Methode om de health van de speler te upgraden
    public void UpgradeHealth(int cost)
    {
        if (hud.gold >= cost)
        {
            int items = 2;

            if (HasRequiredMaterials("RedPotion", items))
            {
                RemoveMaterials("RedPotion", items);
                AddCraftedItem();
                hud.gold -= cost;
                hud.UpdateGold();
                // Voeg een bericht toe aan de crafting log
                craftingLog.AddCraftingMessage("HealthBooster is succesvol gecraft!", "#00FF00");
            }
            else
            {
                // Voeg een foutmelding toe aan de crafting log
                craftingLog.AddCraftingMessage("Niet genoeg materialen om de health Booster te!", "#FF0000");
            }
        }
        else
        {
            craftingLog.AddCraftingMessage("Niet genoeg geld om een Health Booster te maken!", "#FF0000");
        }

    }

    // Methode om de mana van de speler te upgraden
    public void UpgradeMana(int cost)
    {
        if (hud.gold >= cost)
        {
            int items = 2;

            if (HasRequiredMaterials("BluePotion", items))
            {
                RemoveMaterials("BluePotion", items);
                AddCraftedItem();
                hud.gold -= cost;
                hud.UpdateGold();
                // Voeg een bericht toe aan de crafting log
                craftingLog.AddCraftingMessage("ManaBooster is succesvol gecraft!", "#00FF00");
            }
            else
            {
                // Voeg een foutmelding toe aan de crafting log
                craftingLog.AddCraftingMessage("Niet genoeg materialen om de Mana Booster te maken!", "#FF0000");
            }
        }
        else
        {
            craftingLog.AddCraftingMessage("Niet genoeg geld om een Mana Potion te maken!", "#FF0000");
        }

    }
    public void UpgradeStamina(int cost)
    {
        if (hud.gold >= cost)
        {
            int items = 2;

            if (HasRequiredMaterials("Ruby", items))
            {
                RemoveMaterials("Ruby", items);
                AddCraftedItem();
                hud.gold -= cost;
                hud.UpdateGold();
                // Voeg een bericht toe aan de crafting log
                craftingLog.AddCraftingMessage("ManaBooster is succesvol gecraft!", "#00FF00");
            }
            else
            {
                // Voeg een foutmelding toe aan de crafting log
                craftingLog.AddCraftingMessage("Niet genoeg materialen om de booster te maken!", "#FF0000");
            }
        }
        else
        {
            craftingLog.AddCraftingMessage("Niet genoeg geld om de booster te maken!", "#FF0000");
        }

    }

    public void UpgradeDamage(int cost)
    {
        if (hud.gold >= cost)
        {
            int items = 2;

            if (HasRequiredMaterials("Sapphire", items))
            {
                RemoveMaterials("Sapphire", items);
                AddCraftedItem();
                hud.gold -= cost;
                hud.UpdateGold();
                // Voeg een bericht toe aan de crafting log
                craftingLog.AddCraftingMessage("DamageBooster is succesvol gecraft!", "#00FF00");
            }
            else
            {
                // Voeg een foutmelding toe aan de crafting log
                craftingLog.AddCraftingMessage("Niet genoeg materialen om de Booster te maken!", "#FF0000");
            }
        }
        else
        {
            craftingLog.AddCraftingMessage("Niet genoeg geld om de Booster te maken!", "#FF0000");
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
