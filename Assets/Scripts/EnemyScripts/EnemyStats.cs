using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy/CreateEnemy")]
public class EnemyStats : ScriptableObject
{
    public int maxHealth;
    public float damage;
    public float speed;

}
