using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
        public ThirdPersonMovement playerMovement; // Sleep je Player object hier naartoe in de inspector
        public Animator animator;
        public float health = 100f;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerMovement != null)
            {
                health -= playerMovement.damage;
            }
            else
            {
                Debug.LogError("PlayerMovement niet gevonden op de enemy!");
            }
        }
    }

}
