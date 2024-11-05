using Controls;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMagicSystem : MonoBehaviour
{
    [SerializeField] private Transform castPoint;

    [SerializeField] private GameObject FireballPrefab;
    [SerializeField] private GameObject IceiclePrefab;

    private bool canCastMagic = true;

    private PlayerControls playerContols;
    public PlayerStats playerStats;

    private void Awake()
    {
        playerContols = new PlayerControls();
    }



    private void OnEnable()
    {
        playerContols.Enable();
    }

    private void OnDisable()
    {
        playerContols.Disable();
    }


    private void Update()
    {
                if (canCastMagic == true)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (playerStats.currentMana >= 30)
                { 
                    playerStats.UseMana(30);
                    Instantiate(FireballPrefab, castPoint.position, transform.rotation);
                    canCastMagic = false;
                    StartCoroutine(SpellCooldown());
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (playerStats.currentMana >= 25)
                {
                    playerStats.UseMana(25);
                    Quaternion playerRotation = transform.rotation;
                    Quaternion icicleRotation = playerRotation * Quaternion.Euler(0, 90, 90);
                    Instantiate(IceiclePrefab, castPoint.position, icicleRotation);
                    canCastMagic = false;
                    StartCoroutine(SpellCooldown());
                }
            }
        }
        
    }

    IEnumerator SpellCooldown()
    {
        yield return new WaitForSeconds(0.7f);
        canCastMagic = true;
    }
}
