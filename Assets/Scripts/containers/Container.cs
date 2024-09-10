using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Container : MonoBehaviour , IInteractable
{
    public void Interact()
    {
        Debug.Log(Random.Range(0, 100));
        Debug.Log("De kist gaat open");
    }
}

