using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Vector3 startPosition;
    public GameObject prefab; // De prefab van de bloem die je wilt spawnen
    private GameObject spawnedPrefab;
    public float spawnInterval = 3f; // Het aantal seconden tussen spawns
    private float timer = 0f;

    public Tracker tracker;
    public QuestGiver questGiver;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        // Verhoog de timer met de tijd sinds de laatste frame
        if (spawnedPrefab == null || !spawnedPrefab.activeSelf)
        {
            timer += Time.deltaTime;
        }

        // Controleer of het tijd is om een nieuwe vijand te spawnen
        if (timer >= spawnInterval )
        {
            SpawnEnemy(); // Spawn een nieuwe vijand
            timer = 0f; // Reset de timer
        }
    }

    private void SpawnEnemy()
    {
        // Instantiate de vijand op de spawnPoint locatie
        spawnedPrefab = Instantiate(prefab, startPosition - new Vector3(0, 0.25f, 0), transform.rotation);

        // Krijg de EnemyAI-component van de nieuw gespawnde vijand
        EnemyAI enemyAI = spawnedPrefab.GetComponent<EnemyAI>();
        if (enemyAI != null)
        {
            enemyAI.player = GameObject.FindGameObjectWithTag("Player").transform; // Geef de speler door aan het EnemyAI-script
        }

        // Voeg de Tracker-component toe als de quest actief is
        if (questGiver != null && questGiver.questNumber == 1 && !questGiver.IsCompleted()) // Controleer of de juiste quest actief is
        {
            if (spawnedPrefab.GetComponent<Tracker>() == null) // Controleer of de Tracker-component al bestaat
            {
                Tracker tracker = spawnedPrefab.AddComponent<Tracker>(); // Voeg de Tracker-component toe
                tracker.questGiver = questGiver; // Koppel de QuestGiver aan de Tracker
                tracker.number = questGiver.questNumber; // Geef de questNumber door
            }
        }
    }
}
