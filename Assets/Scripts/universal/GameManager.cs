using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Statische variabele voor de singleton instantie
    public static GameManager instance;

    public bool isMenuOpen = false;


    // Wordt aangeroepen wanneer het object wordt gecreëerd
    void Awake()
    {
        // Als er al een instantie bestaat en dat is niet deze, vernietig dan het nieuwe object
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Zet deze instantie als de enige
        instance = this;

        // Zorg ervoor dat het GameManager object niet wordt vernietigd bij een nieuwe scene
        DontDestroyOnLoad(gameObject);
    }


    public void ToggleBool()
    {
        isMenuOpen = !isMenuOpen;
    }
}
