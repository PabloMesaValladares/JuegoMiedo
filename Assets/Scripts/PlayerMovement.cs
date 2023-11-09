using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;

    public float groundDrag;

    [SerializeField] private float stamina;
    [SerializeField] private float maxStamina;
    [SerializeField] private int multiplierStamina;
    [SerializeField] private float staminaCooldown;
    [SerializeField] private float staminaMaxCooldown;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    [Header("Keybinds")]
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;
    
    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    public Vector3 moveDirection;

    Rigidbody _rb;

    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        crouching
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        stamina = maxStamina;
        staminaCooldown = 0;

        startYScale = transform.localScale.y;
    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();
        StateHandler();

        // handle drag
        if (grounded)
            _rb.drag = groundDrag;
        else
            _rb.drag = 0;

        //stamina
        if(state == MovementState.sprinting)
        {
            DecreaseStamina();
        }
        else if(state != MovementState.sprinting)
        {
            IncreaseStamina();
        }

    }

    private void DecreaseStamina()
    {
        if(stamina != 0)
        {
            stamina -= multiplierStamina * Time.deltaTime;
        }
        if(stamina <= 5)
        {
            staminaCooldown = staminaMaxCooldown;
        }
    }

    private void IncreaseStamina()
    {
        if (staminaCooldown > 0)
        {
            staminaCooldown -= 0.5f * Time.deltaTime;
        }
        if (stamina < maxStamina)
        {
            stamina += (multiplierStamina / 2) * Time.deltaTime;
        }
        else if(stamina > maxStamina)
        {
            stamina = maxStamina;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // start crouch
        if (Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            _rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        // stop crouch
        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    private void StateHandler()
    {
        // Mode - Crouching && Crouching/Fatigue
        if (Input.GetKey(crouchKey) && staminaCooldown < 0)
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }

        if (Input.GetKey(crouchKey) && staminaCooldown > 0)
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed / 2;
        }

        // Mode - Sprinting
        else if(grounded && Input.GetKey(sprintKey) && state != MovementState.crouching && moveDirection != new Vector3(0,0,0) && staminaCooldown <= 0)
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }

        // Mode - Walking && Walking/Fatigue
        else if (grounded && staminaCooldown < 0)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }

        else if(grounded && staminaCooldown > 0)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed / 2;
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on slope
        if (OnSlope() && !exitingSlope)
        {
            _rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);

            if (_rb.velocity.y > 0)
                _rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        // on ground
        else if(grounded)
            _rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // turn gravity off while on slope
        _rb.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {
        // limiting speed on slope
        if (OnSlope() && !exitingSlope)
        {
            if (_rb.velocity.magnitude > moveSpeed)
                _rb.velocity = _rb.velocity.normalized * moveSpeed;
        }

        // limiting speed on ground or in air
        else
        {
            Vector3 flatVel = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

            // limit velocity if needed
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                _rb.velocity = new Vector3(limitedVel.x, _rb.velocity.y, limitedVel.z);
            }
        }
    }

    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
}