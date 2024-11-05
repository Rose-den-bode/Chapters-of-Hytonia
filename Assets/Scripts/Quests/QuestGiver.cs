using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    //UI element
    public TextMeshProUGUI questBox;
    public TextMeshProUGUI interactText;
    private bool isInRange;

    //quest status
    private string progress;
    private bool completed;

    //quest requirements
    private int currentValue;
    private int goalValue;
    private int rewardAmount;
    private string questType;
    private string objective;

    //quest generation variables
    public int questNumber;
    private List<GameObject> objectList; // Houdt alle Goblins bij
    private HUD HUDScript;


    // Start is called before the first frame update
    void Start()
    {
        HUDScript = GameObject.Find("HUD").GetComponent<HUD>();
        ResetQuest();
        objectList = new List<GameObject>();
        completed = true;
    }

    private void ResetQuest()
    {
        questBox.text = "Current objective: None\r\n";
        currentValue = 0;
    }

    private void UpdateQuest()
    {
        progress = $"Current Progress: {currentValue}/ {goalValue}\n";
        questBox.text = $"Current objective: {questType} {goalValue} {objective}\n" +
                $"{progress}" +
                $"Reward = {rewardAmount} Gold";
    }
    public void IncreaseAmount()
    {
        currentValue++;
        UpdateQuest();
    }


    public void QuestGenerator(int number)
    {
        ResetQuest();
        goalValue = Random.Range(4, 9);
        completed = false;

        if (number == 1)
        {
            //sets reward
            rewardAmount = goalValue * 40;

            //sets interactable variabel for tracker
            progress = $"Current Progress: {currentValue}/ {goalValue}\n";
            questType = "Kill";
            objective = "Goblins";
                
            questBox.text = $"Current objective: {questType} {goalValue} {objective}\n" +
                $"{progress}" +
                $"Reward = {rewardAmount} Gold";
            GameObject[] foundEnemies = GameObject.FindGameObjectsWithTag("Enemy"); // Zoek naar alle objecten met de tag "Enemy"
            List<GameObject> foundGoblins = new List<GameObject>();

            foreach (GameObject enemy in foundEnemies)
            {
                if (enemy.name == "Goblin" || enemy.name == "Goblin(Clone)") // Controleer of de naam "Goblin" is
                {
                    foundGoblins.Add(enemy);
                }
            }

            objectList.Clear();
            objectList.AddRange(foundGoblins);

            // Voeg de Tracker-component toe aan elke gevonden Goblin
            foreach (GameObject goblin in objectList)
            {
                if (goblin.GetComponent<Tracker>() == null) // Controleer of de Tracker-component al bestaat
                {
                    goblin.AddComponent<Tracker>().questGiver = this; // Voeg de Tracker-component toe
                    Tracker tracker = goblin.GetComponent<Tracker>();
                    tracker.number = questNumber;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E)) 
        {
            QuestGenerator(questNumber);
        }
        
        Complete();
    }
    public bool IsCompleted()
    {
        return completed;
    }
    void Complete()
    {
        if (currentValue >= goalValue)
        {
            completed = true;
            HUDScript.AddGold(rewardAmount);
            ResetQuest();
        }
    }

        private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Zorg ervoor dat je de juiste tag gebruikt
        {
            isInRange = true; // Speler is in het bereik
            interactText.gameObject.SetActive(true); // Toon de interact tekst
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false; // Speler is niet meer in het bereik
            interactText.gameObject.SetActive(false); // Verberg de interact tekst
        }
    }
}
