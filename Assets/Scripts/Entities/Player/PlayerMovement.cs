using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    private Animator animator;

    private bool playingFootsteps = false;
    public float footstepSpeed = 0.5f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (PauseController.IsGamePaused)
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("isWalking", false);
            StopFootsteps();
            return;
        }

        rb.linearVelocity = moveInput * moveSpeed;
        bool isMoving = rb.linearVelocity.magnitude > 0;
        animator.SetBool("isWalking", isMoving);

        if (isMoving && !playingFootsteps)
        {
            StartFootsteps();
        }
        else if (!isMoving && playingFootsteps)
        {
            StopFootsteps();
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 newInput = context.ReadValue<Vector2>();

        if (moveInput != Vector2.zero)
        {
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
        }

        moveInput = newInput;
        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);

        if (context.canceled)
        {
            animator.SetBool("isWalking", false);
        }
    }

    void StartFootsteps()
    {
        playingFootsteps = true;
        InvokeRepeating(nameof(PlayFootstep), footstepSpeed, footstepSpeed);
    }

    void StopFootsteps()
    {
        playingFootsteps = false;
        CancelInvoke(nameof(PlayFootstep));
    }

    void PlayFootstep()
    {
        SoundEffectManager.Play("FootstepDirt", true);
    }
}