using System.Runtime.Serialization;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cameraTransform;
    public float speed = 6f;
    public float sprintSpeed = 10f; // Variabele voor sprintsnelheid
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float damage = 10f;

    private Vector3 velocity;
    private bool isGrounded;
    private Animator animator;

    private PlayerStats playerStats; // Referentie naar het PlayerStats-script

    public float sprintStaminaCost = 10f; // Hoeveel stamina het kost om te sprinten per seconde

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerStats = PlayerStats.Instance; // Haal de instantie van PlayerStats op
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        Move(); // Verplaatsing
        ApplyGravityAndJump(); // Pas zwaartekracht en springen toe
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            animator.SetBool("IsMoving", true);

            // Controleer of de speler kan sprinten
            bool isSprinting = Input.GetKey(KeyCode.LeftShift) && isGrounded && playerStats.currentStamina > 0;
            float currentSpeed = isSprinting ? sprintSpeed : speed;

            // Als de speler aan het sprinten is, verlaag de stamina
            if (isSprinting)
            {
                playerStats.UseStamina(sprintStaminaCost * Time.deltaTime); // Verbruik stamina per seconde
            }

            // Camera-gebaseerde beweging
            Vector3 cameraForward = cameraTransform.forward;
            Vector3 cameraRight = cameraTransform.right;

            // Alleen rotatie op de Y-as
            cameraForward.y = 0f;
            cameraRight.y = 0f;

            // Normaliseer de richtingen
            cameraForward.Normalize();
            cameraRight.Normalize();

            // Bereken de bewegingsrichting op basis van de camera
            Vector3 moveDirection = cameraForward * vertical + cameraRight * horizontal;

            // Verplaats de speler
            controller.Move(moveDirection * currentSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }

    private void ApplyGravityAndJump()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Kleine negatieve waarde om op de grond te blijven
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.Play("JumpAll");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}
