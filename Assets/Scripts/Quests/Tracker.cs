using UnityEditor;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    public QuestGiver questGiver;
    public int number;

    private void OnDestroy()
    {
        questGiver.IncreaseAmount();
    }

    private void Update()
    {
        if (questGiver.IsCompleted() || number != questGiver.questNumber)
        {
            Destroy(this); // Verwijder de Tracker-component
        }
    }
}
