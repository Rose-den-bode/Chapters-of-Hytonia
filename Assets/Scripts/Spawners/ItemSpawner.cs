using TMPro;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    private Vector3 startPosition;
    public GameObject prefab; // De prefab van de bloem die je wilt spawnen
    public float spawnInterval = 3f; // Het aantal seconden tussen spawns
    public TextMeshProUGUI pickupText; // UI-element voor pickup text
    private float timer = 0f;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        // Update de timer
        timer += Time.deltaTime;

        // Check of de timer gelijk of groter is dan het spawn-interval en of er geen bloem in de trigger is
        if (timer >= spawnInterval)
        {
            SpawnItem();
            timer = 0f; // Reset de timer na het spawnen
        }
    }

    private void SpawnItem()
    {
        // Instantiate de bloem op de spawnPoint locatie
        if (!IsPositionOccupied())
        {
            GameObject spawned = Instantiate(prefab, startPosition - new Vector3(0, 0.25f, 0), transform.rotation);
            ItemPickup itemPickupScript = spawned.GetComponent<ItemPickup>();

            if (itemPickupScript != null)
            {
                itemPickupScript.pickupText = pickupText; // Geef de pickupText door aan het ItemPickup-script
            }
            // Voer hier andere acties uit met het spawned object
        }
    }

    // Controleert of er al een bloem in de buurt is met Physics.OverlapSphere
    private bool IsPositionOccupied()
    {
        // Controleert of er iets is in de opgegeven straal
        return Physics.CheckSphere(startPosition, 0.1f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Kleur van de gizmo
        Gizmos.DrawWireSphere(startPosition, 0.1f); // Tekent een wireframe sphere
    }
}
