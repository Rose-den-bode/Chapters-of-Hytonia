using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    private PlayerStats playerStats;  // Referentie naar het PlayerStats script

    void Start()
    {
        // Koppel het PlayerStats script aan dit object
        playerStats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        // Simuleer schade als de spatiebalk wordt ingedrukt (verlaag health)
        if (Input.GetKeyDown(KeyCode.B))
        {
            playerStats.TakeDamage(10f);  // Verlaag health met 10
            Debug.Log("Health: " + playerStats.currentHealth);
        }

        // Simuleer stamina gebruik bij rennen met linker shift
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerStats.UseStamina(10f * Time.deltaTime);  // Verlaag stamina met 10 per seconde
            Debug.Log("Stamina: " + playerStats.currentStamina);
        }

        // Simuleer mana gebruik bij het casten van een spreuk met toets 'F'
        if (Input.GetKeyDown(KeyCode.N))
        {
            playerStats.UseMana(15f);  // Verlaag mana met 15
            Debug.Log("Mana: " + playerStats.currentMana);
        }

        // Voeg een knop toe om de player te genezen, bijvoorbeeld toets 'H'
        if (Input.GetKeyDown(KeyCode.H))
        {
            playerStats.currentHealth = playerStats.maxHealth;  // Herstel volledige gezondheid
            Debug.Log("Health fully restored: " + playerStats.currentHealth);
        }
    }
}
