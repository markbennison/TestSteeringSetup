using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerLocomotion : MonoBehaviour
{

    TestSteeringSetup inputActions;
    InputAction move;
    InputAction look;

    CharacterController characterController;
    public Transform cameraContainer;
    Transform playerContainer;

    public float speed = 6.0f;
    //public float jumpSpeed = 10f;
    public float mouseSensitivity = 2f;
    public float gravity = 20.0f;
    public float lookUpClamp = -30f;
    public float lookDownClamp = 60f;

    Vector3 moveDirection = Vector3.zero;
    float rotateX, rotateY;

    private void Awake()
    {
        inputActions = new TestSteeringSetup();
    }

    void Start()
    {
        Cursor.visible = false;
        characterController = GetComponent<CharacterController>();

    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //Debug.Log("Move: " + move.ReadValue<Vector2>());

        Locomotion();
        RotateAndLook();
    }

    private void OnEnable()
    {
        move = inputActions.Player.Move;
        move.Enable();

        look = inputActions.Player.Look;
        look.Enable();

        inputActions.Player.Fire.performed += Fire;
        inputActions.Player.Fire.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        look.Disable();
        inputActions.Player.Fire.Disable();
    }

    private void Fire(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    void Locomotion()
    {
        if (characterController.isGrounded) // When grounded, set y-axis to zero (to ignore it)
        {
            Vector2 moveInput = move.ReadValue<Vector2>();
            moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
        }

        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    void RotateAndLook()
    {
        Vector2 lookInput = look.ReadValue<Vector2>();
        rotateX = lookInput.x * mouseSensitivity;
        rotateY -= lookInput.y * mouseSensitivity;

        rotateY = Mathf.Clamp(rotateY, lookUpClamp, lookDownClamp);

        cameraContainer.transform.Rotate(0f, rotateX, 0f);

        cameraContainer.transform.localRotation = Quaternion.Euler(rotateY, 0f, 0f);
    }
}
