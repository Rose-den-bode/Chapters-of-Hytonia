using UnityEngine;

public class RotateWithCamera : MonoBehaviour
{
    public Transform cameraTransform; // De camera die de richting bepaalt
    public float rotationSpeed = 10f; // Snelheid van rotatie

    void Update()
    {
        // Verkrijg de input van de speler
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Bereken de bewegingsrichting in lokale ruimte van de camera
        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical);

        if (moveDirection != Vector3.zero)
        {
            // Verkrijg de richting van de camera op de Y-as
            Vector3 cameraForward = cameraTransform.forward;
            Vector3 cameraRight = cameraTransform.right;

            // Zorg ervoor dat de camera alleen op de Y-as rotert
            cameraForward.y = 0f;
            cameraRight.y = 0f;

            // Normaliseer de richtingen
            cameraForward.Normalize();
            cameraRight.Normalize();

            // Bereken de gewenste bewegingsrichting in wereldruimte
            Vector3 worldDirection = cameraForward * moveDirection.z + cameraRight * moveDirection.x;

            // Bereken de gewenste rotatie op basis van de bewegingsrichting
            Quaternion targetRotation = Quaternion.LookRotation(worldDirection);

            // Draai de kubus langzaam naar de gewenste rotatie
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
