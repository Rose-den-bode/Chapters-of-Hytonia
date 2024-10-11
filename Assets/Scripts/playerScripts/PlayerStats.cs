using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Statistieken van de speler
    public float maxHealth = 100f;
    public float currentHealth;

    public float maxStamina = 100f;
    public float currentStamina;

    public float maxMana = 100f;
    public float currentMana;

    // Regeneratiesnelheden
    public float healthRegenRate = 1f;   // HP per seconde
    public float staminaRegenRate = 5f;  // Stamina per seconde
    public float manaRegenRate = 2f;     // Mana per seconde

    // Tijd voordat de regeneratie begint
    public float staminaRegenDelay = 2f;
    public float manaRegenDelay = 3f;

    private float staminaRegenTimer;
    private float manaRegenTimer;

    void Start()
    {
        // Initialiseren van de huidige waarden
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        currentMana = maxMana;
    }

    void Update()
    {
        // Roep de regeneratiefuncties aan
        RegenerateHealth();
        RegenerateStamina();
        RegenerateMana();
    }

    // Gezondheid regeneratie
    void RegenerateHealth()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += healthRegenRate * Time.deltaTime;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Zorgt dat de waarde niet hoger wordt dan maxHealth
        }
    }

    // Stamina regeneratie
    void RegenerateStamina()
    {
        if (currentStamina < maxStamina)
        {
            staminaRegenTimer += Time.deltaTime;
            if (staminaRegenTimer >= staminaRegenDelay)
            {
                currentStamina += staminaRegenRate * Time.deltaTime;
                currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina); // Zorgt dat de waarde niet hoger wordt dan maxStamina
            }
        }
        else
        {
            staminaRegenTimer = 0f; // Reset de timer als stamina vol is
        }
    }

    // Mana regeneratie
    void RegenerateMana()
    {
        if (currentMana < maxMana)
        {
            manaRegenTimer += Time.deltaTime;
            if (manaRegenTimer >= manaRegenDelay)
            {
                currentMana += manaRegenRate * Time.deltaTime;
                currentMana = Mathf.Clamp(currentMana, 0, maxMana); // Zorgt dat de waarde niet hoger wordt dan maxMana
            }
        }
        else
        {
            manaRegenTimer = 0f; // Reset de timer als mana vol is
        }
    }

    // Functie om schade toe te brengen aan de speler
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Functie voor het verlagen van stamina (bijv. bij rennen)
    public void UseStamina(float amount)
    {
        currentStamina -= amount;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        staminaRegenTimer = 0f; // Reset de timer zodat stamina niet onmiddellijk regenereert
    }

    // Functie voor het verlagen van mana (bijv. bij het casten van spreuken)
    public void UseMana(float amount)
    {
        currentMana -= amount;
        currentMana = Mathf.Clamp(currentMana, 0, maxMana);
        manaRegenTimer = 0f; // Reset de timer zodat mana niet onmiddellijk regenereert
    }

    public void UpgradeStat(string statType, float value)
    {
        switch (statType.ToLower())
        {
            case "health":
                currentHealth = Mathf.Clamp(currentHealth + value, 0, maxHealth);
                break;
            case "stamina":
                currentStamina = Mathf.Clamp(currentStamina + value, 0, maxStamina);
                break;
            case "mana":
                currentMana = Mathf.Clamp(currentMana + value, 0, maxMana);
                break;
            default:
                Debug.LogError($"Onbekend statType: {statType}");
                break;
        }
    }

    public void DowngradeStat(string statType, float value)
    {
        switch (statType.ToLower())
        {
            case "health":
                currentHealth = Mathf.Clamp(currentHealth - value, 0, maxHealth);
                break;
            case "stamina":
                currentStamina = Mathf.Clamp(currentStamina - value, 0, maxStamina);
                break;
            case "mana":
                currentMana = Mathf.Clamp(currentMana - value, 0, maxMana);
                break;
            default:
                Debug.LogError($"Onbekend statType: {statType}");
                break;
        }
    }

    // Dood de speler (placeholder functie)
    void Die()
    {
        Debug.Log("Player has died.");
        // Voeg hier de doodslogica toe, zoals respawning of het einde van het spel.
    }
}
