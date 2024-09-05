using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cameraTransform;
    public float speed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    private Vector3 velocity;
    private bool isGrounded;

    void Update()
    {
            isGrounded = controller.isGrounded;
        //Hieronder staat alles om de speler te laten bewegen ten opzichte van de camera-angle

            // Verkrijg de input van de speler
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // Verkrijg de richting van de camera
            Vector3 cameraForward = cameraTransform.forward;
            Vector3 cameraRight = cameraTransform.right;

            // Zorg ervoor dat de camera alleen op de Y-as rotert
            cameraForward.y = 0f;
            cameraRight.y = 0f;

            // Normaliseer de richtingen
            cameraForward.Normalize();
            cameraRight.Normalize();

            // Bereken de gewenste bewegingsrichting op basis van camera
            Vector3 moveDirection = cameraForward * vertical + cameraRight * horizontal;

            // Beweeg de speler
            controller.Move(moveDirection * speed * Time.deltaTime);

        //Hieronder staat hoe het karakter springt

            

            if (isGrounded && velocity.y < 0)
                {
                    velocity.y = -2f; // Kleine negatieve waarde om te zorgen dat je op de grond blijft
                }

            // Pas zwaartekracht toe
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
    }
}
