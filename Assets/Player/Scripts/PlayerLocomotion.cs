using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Android.Gradle;
using Unity.Android.Gradle.Manifest;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerLocomotion : MonoBehaviour
{

    //TestSteeringSetup inputActions;
    //InputAction move;
    //InputAction look;

    InputAction turn;
    InputAction turnx;
    InputAction breakPedal;
    InputAction accelerate;

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
        //inputActions = new TestSteeringSetup();
    }

    void Start()
    {
        Cursor.visible = false;
        characterController = GetComponent<CharacterController>();
        GameManager.InputManager.inputActions.Drive.Turn.Enable();
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //if(move.ReadValue<Vector2>() != Vector2.zero)
        //{
        //    Debug.Log("Move: " + move.ReadValue<Vector2>());
        //}
        //if (look.ReadValue<Vector2>() != Vector2.zero)
        //{
        //    Debug.Log("Look: " + look.ReadValue<Vector2>());
        //}

        Debug.Log("Turn: " + turn.ReadValue<Vector2>().x);
        Debug.Log("Accelerate: " + accelerate.ReadValue<float>());
        //Debug.Log("Move: " + move.ReadValue<Vector2>().x);


        //if (breakPedal.ReadValue<float>() != 0f)
        //{
        //    Debug.Log("Break Pedal: " + breakPedal.ReadValue<float>());
        //}
        Debug.Log("Break Pedal: " + breakPedal.ReadValue<float>());

        //Locomotion();
        //RotateAndLook();
    }

    private void OnEnable()
    {
        //move = GameManager.InputManager.inputActions.Player.Move;
        //move.Enable();

        //look = GameManager.InputManager.inputActions.Player.Look;
        //look.Enable();

        turn = GameManager.InputManager.inputActions.Drive.Turn;
        turn.Enable();

        accelerate = GameManager.InputManager.inputActions.Drive.Accelerate;
        accelerate.Enable();

        breakPedal = GameManager.InputManager.inputActions.Drive.BreakPedal;
        breakPedal.Enable();

        GameManager.InputManager.inputActions.Player.Fire.performed += Fire;
        GameManager.InputManager.inputActions.Player.Fire.Enable();

    }

    private void OnDisable()
    {
        //move.Disable();
        //look.Disable();
        turn.Disable();
        accelerate.Disable();
        breakPedal.Disable();

        GameManager.InputManager.inputActions.Player.Fire.Disable();
    }

    private void Fire(InputAction.CallbackContext context)
    {

        Debug.Log("Fire Button Pressed");
    }

    //void Locomotion()
    //{
    //    if (characterController.isGrounded) // When grounded, set y-axis to zero (to ignore it)
    //    {
    //        Vector2 moveInput = move.ReadValue<Vector2>();
    //        moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);
    //        moveDirection = transform.TransformDirection(moveDirection);
    //        moveDirection *= speed;
    //    }

    //    moveDirection.y -= gravity * Time.deltaTime;
    //    characterController.Move(moveDirection * Time.deltaTime);
    //}

    //void RotateAndLook()
    //{
    //    Vector2 lookInput = look.ReadValue<Vector2>();
    //    rotateX = lookInput.x * mouseSensitivity;
    //    rotateY -= lookInput.y * mouseSensitivity;

    //    rotateX += cameraContainer.transform.localRotation.eulerAngles.y;
    //    //rotateY += cameraContainer.transform.localRotation.eulerAngles.x;

    //    //rotateX = Mathf.Clamp(rotateX, -30f, +30f);
    //    rotateY = Mathf.Clamp(rotateY, lookUpClamp, lookDownClamp);

    //    //Debug.Log("XY: " + rotateX + "," + rotateY);

    //    //cameraContainer.transform.Rotate(0f, rotateX, 0f);
    //    //cameraContainer.transform.localRotation = Quaternion.Euler(rotateY, transform.localRotation.y, transform.localRotation.z);

    //    cameraContainer.transform.localRotation = Quaternion.Euler(rotateY, rotateX, 0f);
    //}
}
