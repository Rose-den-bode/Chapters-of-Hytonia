using Controls;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMagicSystem : MonoBehaviour
{
    [SerializeField] private float maxMana = 100f;
    [SerializeField] private float currentMana;
    [SerializeField] private float manaRegen = 2f;

    [SerializeField] private Transform castPoint;

    [SerializeField] private GameObject FireballPrefab;
    [SerializeField] private GameObject IceiclePrefab;

    private bool canCastMagic = true;

    private PlayerControls playerContols;

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
                Instantiate(FireballPrefab, transform.position, transform.rotation);
                canCastMagic = false;
                StartCoroutine(SpellCooldown());
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Quaternion playerRotation = transform.rotation;
                Quaternion icicleRotation = playerRotation * Quaternion.Euler(0, 90, 90);
                Instantiate(IceiclePrefab, transform.position, icicleRotation);
                canCastMagic = false;
                StartCoroutine(SpellCooldown());
            }
        }
        
    }

    IEnumerator SpellCooldown()
    {
        yield return new WaitForSeconds(0.7f);
        canCastMagic = true;
    }
}
