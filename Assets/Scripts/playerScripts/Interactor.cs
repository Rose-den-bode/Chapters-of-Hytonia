using System.Collections;
using System.Collections.Generic;
using UnityEngine;


interface IInteractable
{
    public void Interact();
}
public class Interactor : MonoBehaviour
{

    public Transform InteractorSource;
    public float InteractRange = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) 
        {
            Ray r = new Ray(InteractorSource.position + new Vector3(0, 0.5f,0), InteractorSource.forward);

            Debug.DrawRay(r.origin, r.direction * InteractRange, Color.red, 1.0f);

            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                if(hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                    {
                        interactObj.Interact();
                    }
            }
        }   
    }
}
