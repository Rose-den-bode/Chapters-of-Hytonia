using System.Runtime.Serialization;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cameraTransform;
    public float speed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float damage = 10f;

    private Vector3 velocity;
    private bool isGrounded;
    private Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        isGrounded = controller.isGrounded;
        Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        Move();
            // Verkrijg de input van de speler
            
        //Hieronder staat alles om de speler te laten bewegen ten opzichte van de camera-angle



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
                animator.Play("JumpAll");
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
    }

    private void Move()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Input.GetAxis("Horizontal") != 0|| Input.GetAxis("Vertical") != 0)
        {
            animator.SetBool("IsMoving", true);
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
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

    }
}
