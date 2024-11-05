using UnityEngine;
using UnityEngine.UI;

public class TutorialSystem : MonoBehaviour
{
    public GameObject[] tutorialTabs; // Sleep hier de tutorial tabs in uit de inspector
    private int currentTabIndex = 0;

    void Start()
    {
        // Zet de eerste tab aan en de rest uit
        UpdateTabs();
    }

    void Update()
    {
        // Volgende tab via Enter
        if (Input.GetKeyDown(KeyCode.N))
        {
            NextTab();
        }
        // Vorige tab via Z
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            PreviousTab();
        }
        // Start tutorial opnieuw via T
        else if (Input.GetKeyDown(KeyCode.T))
        {
            ResetTutorial();
        }
    }

    void NextTab()
    {
        if (currentTabIndex < tutorialTabs.Length - 1)
        {
            currentTabIndex++;
            UpdateTabs();
        }
    }

    void PreviousTab()
    {
        if (currentTabIndex > 0)
        {
            currentTabIndex--;
            UpdateTabs();
        }
    }

    void ResetTutorial()
    {
        currentTabIndex = 0;
        UpdateTabs();
    }

    void UpdateTabs()
    {
        // Zet alle tabs uit
        for (int i = 0; i < tutorialTabs.Length; i++)
        {
            tutorialTabs[i].SetActive(i == currentTabIndex);
        }
    }
}
