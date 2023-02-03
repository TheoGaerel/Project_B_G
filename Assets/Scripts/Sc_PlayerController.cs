using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sc_PlayerController : MonoBehaviour
{
    [SerializeField] private float f_playerSpeed = 5f;
    private CharacterController controller;
    [SerializeField]
    private Vector2 v2_movement;
    private Vector3 v3_aim;

    private Vector3 v3_playerVelocity;

    private PlayerControls playerControls;
    private PlayerInput playerInput;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerControls = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        HandleInputs();
        HandleMovements();
        HandleRotation();
    }

    private void HandleInputs()
    {
        v2_movement = playerControls.Controls.Movement.ReadValue<Vector2>();
        v3_aim = playerControls.Controls.Aim.ReadValue<Vector2>();
    }
    private void HandleMovements()
    {
        Vector3 move = new Vector3(v2_movement.x, 0, v2_movement.y);
        controller.Move(move * Time.deltaTime * f_playerSpeed);
    }
    private void HandleRotation()
    {

    }
}
