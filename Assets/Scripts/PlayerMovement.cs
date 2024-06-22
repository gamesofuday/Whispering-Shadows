using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintSpeed = 10f;
    public float sprintDuration = 2f; // Duration for how long the sprint lasts
    public float sprintCooldown = 5f; // Cooldown time before sprint can be used again
    public GameObject flashlight;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private bool isFlashlightOn = false;
    private bool isSprinting = false;
    private float sprintTimer = 0f;
    private float cooldownTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveVelocity = moveInput.normalized * (isSprinting ? sprintSpeed : moveSpeed); // Adjust speed based on sprinting state

        HandleFlashlight();
        HandleSprint();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    void HandleFlashlight()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFlashlight();
        }
    }

    void ToggleFlashlight()
    {
        isFlashlightOn = !isFlashlightOn;
        flashlight.SetActive(isFlashlightOn);
    }

    void HandleSprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isSprinting && cooldownTimer <= 0)
        {
            StartCoroutine(Sprint());
        }

        // Update cooldown timer if sprinting is on cooldown
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    IEnumerator Sprint()
    {
        isSprinting = true;
        moveVelocity = moveInput.normalized * sprintSpeed;
        sprintTimer = sprintDuration;

        while (sprintTimer > 0)
        {
            sprintTimer -= Time.deltaTime;
            yield return null;
        }

        isSprinting = false;
        moveVelocity = moveInput.normalized * moveSpeed;
        cooldownTimer = sprintCooldown;
    }
}
