using UnityEngine;
using System.Collections;

public class FPS_Movement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterController controller;

    [Header("Movement")]
    [SerializeField] private float speed = 10f;

    [Header("Jump & Gravity")]
    [SerializeField] private float jumpHeight = 3.5f;
    [SerializeField] private float gravity = -30f;
    

    private Vector2 horizontalInput;
    private Vector3 velocity;
    private bool jumpRequested;
    private bool isGrounded;

    private void Update()
    {
        // Check if the player is touching the ground
        isGrounded = controller.isGrounded;

        // Keep player grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Horizontal movement
        Vector3 move = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;

        // Jump
        if (jumpRequested && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpRequested = false;
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;

        // Combine horizontal and vertical movement
        Vector3 finalMove = move + velocity;

        // Move character
        controller.Move(finalMove * Time.deltaTime);
    }

    public void ReceiveInput(Vector2 input)
    {
        horizontalInput = input;
    }

    public void OnJumpPressed()
    {
        jumpRequested = true;
    }

    public void Respawn(Vector3 position)
    {
    StartCoroutine(RespawnRoutine(position));
    }

    private IEnumerator RespawnRoutine(Vector3 position)
    {
    controller.enabled = false;
    transform.position = position;
    velocity = Vector3.zero;
    controller.enabled = true;
    yield return null;
    }

}