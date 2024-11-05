using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // Voeg deze regel toe voor de Slider componenten

public class HUD : MonoBehaviour
{
    // Referenties voor Text en sliders
    public TextMeshProUGUI money;
    public Slider healthSlider;
    public Slider staminaSlider;
    public Slider manaSlider;

    public TextMeshProUGUI healthText; // Zorg dat je dit koppelt in de Inspector
    public TextMeshProUGUI manaText; // Zorg dat je dit koppelt in de Inspector
    public TextMeshProUGUI staminaText; // Zorg dat je dit koppelt in de Inspector


    public float gold = 0f;

    // Referentie naar het PlayerStats script
    private PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        // Geld tekst koppelen
        GameObject Money = GameObject.Find("Money");
        money = Money.GetComponent<TextMeshProUGUI>();

        // Speler statistieken koppelen
        GameObject player = GameObject.FindWithTag("Player"); // Zorg ervoor dat de speler het tag 'Player' heeft
        playerStats = player.GetComponent<PlayerStats>();

        // Sliders koppelen (zorg dat ze aan de juiste GameObjects zijn toegewezen in de inspector)
        GameObject healthSliderObj = GameObject.Find("HealthSlider");
        healthSlider = healthSliderObj.GetComponent<Slider>();

        GameObject staminaSliderObj = GameObject.Find("StaminaSlider");
        staminaSlider = staminaSliderObj.GetComponent<Slider>();

        GameObject manaSliderObj = GameObject.Find("ManaSlider");
        manaSlider = manaSliderObj.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSliders();
    }

    // Functie om goud toe te voegen
    public void AddGold(float amount)
    {
        gold += amount;
        UpdateGold();
    }

    // Update geldtekst
    public void UpdateGold()
    {
        money.text = $"Money: {gold}";
    }

    // Update sliders op basis van de PlayerStats waarden
    private void UpdateSliders()
    {
        // Zorg ervoor dat je het correct berekent door de huidige waarde te delen door de maximale waarde
        healthSlider.value = playerStats.currentHealth / playerStats.maxHealth;
        staminaSlider.value = playerStats.currentStamina / playerStats.maxStamina;
        manaSlider.value = playerStats.currentMana / playerStats.maxMana;

        healthText.text = $"{Mathf.RoundToInt(playerStats.currentHealth)}/{Mathf.RoundToInt(playerStats.maxHealth)}"; // Of een andere weergave die je mooi vindt
        manaText.text = $"{Mathf.RoundToInt(playerStats.currentMana)}/{Mathf.RoundToInt(playerStats.maxMana)}"; // Of een andere weergave die je mooi vindt
        staminaText.text = $"{Mathf.RoundToInt(playerStats.currentStamina)}/{Mathf.RoundToInt(playerStats.maxStamina)}"; // Of een andere weergave die je mooi vindt

    }
}
