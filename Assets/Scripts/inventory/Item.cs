using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public int value;
    public Sprite icon;
    public ItemType itemType;

    public enum ItemType
    {
        Health,
        Healthup,
        Mana,
        ManaBooster,
        Stamina,
        StaminaBooster,
        DamageBooster,
        Gold,
        Spell1,
        Spell2,
        Spell3,
        Ingredient
    }
}
