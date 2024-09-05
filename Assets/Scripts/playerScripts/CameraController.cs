using UnityEngine;

public class RobloxCameraController : MonoBehaviour
{
    public Transform player; // De speler waar de camera omheen draait
    public float mouseSensitivity = 100f; // Gevoeligheid van de muis
    public float distanceFromPlayer = 5f; // Afstand van de camera tot de speler
    public float verticalAngleLimit = 80f; // Limiet voor verticale rotatie
    public Vector2 cameraRotationSpeed = new Vector2(10f, 10f); // Snelheid van rotatie

    private float yRotation = 0f; // Horizontale rotatie van de camera
    private float xRotation = 0f; // Verticale rotatie van de camera

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Haal de muisinvoer op
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Verwerk horizontale rotatie (rond de Y-as)
        yRotation += mouseX;
        yRotation %= 360f; // Zorg ervoor dat de rotatie binnen 360 graden blijft

        // Verwerk verticale rotatie (rond de X-as)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -verticalAngleLimit, verticalAngleLimit); // Beperk verticale rotatie

        // Pas de camera-positie en -rotatie aan op basis van de rotatie
        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        Vector3 position = player.position - rotation * Vector3.forward;

        transform.position = position;
        transform.LookAt(player);
    }
}
