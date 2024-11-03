using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Item/CreateWeapon")]

public class Weapons : ScriptableObject
{
    public string Name;
    public Types WeaponType;
    public float Damage;
    public damageType DamageType;

    public float Reach;
    public float Radius;
    public GameObject WeaponMesh;
    // Start is called before the first frame update

    public enum Types
    {
        Sword,
        Axe,
        Lance
    }
    public enum damageType
    {
        Slashing,
        Piercing,
        Strike
    }
}
