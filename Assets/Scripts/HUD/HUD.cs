using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI money;
    private float gold = 0f;
    // Start is called before the first frame update
    void Start()
    {
        GameObject Money = GameObject.Find("Money");
        money = Money.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddGold(float amount)
    {
        gold += amount;
        UpdateGold();
    }

    private void UpdateGold()
    {
        money.text = $"Money: {gold}";
    }
}
