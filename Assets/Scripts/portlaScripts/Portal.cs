using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{

    public Transform player;
    public Transform reciever;

    private bool playerIsOverlapping = false;

    // Update is called once per frame
    void Update()
    {
        if (playerIsOverlapping)
        {
            Vector3 portalToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            Debug.Log("portalToPlayer: " + portalToPlayer);
            Debug.Log("dotProduct: " + dotProduct);

            // If this is true: The player has moved across the portal
            if (dotProduct < 0f)
            {
                Debug.Log("Player has crossed the portal. Starting teleportation...");

                // Teleport the player
                float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);
                rotationDiff += 180;
                Debug.Log("Rotation difference: " + rotationDiff);

                // Rotate the player
                player.Rotate(Vector3.up, rotationDiff);
                Debug.Log("Player rotated.");

                // Calculate the new position based on the receiver's position
                Vector3 newPositionOffset = reciever.TransformPoint(Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer);
                Debug.Log("New position offset: " + newPositionOffset);

                // Apply the new position to the player
                player.position = newPositionOffset;
                Debug.Log("Player new position: " + player.position);

                playerIsOverlapping = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsOverlapping = true;
            Debug.Log("Player entered the portal.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsOverlapping = false;
            Debug.Log("Player exited the portal.");
        }
    }
}
