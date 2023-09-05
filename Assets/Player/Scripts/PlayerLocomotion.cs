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

    TestSteeringSetup inputActions;
    InputAction move;
    InputAction look;

    InputAction turn;
    InputAction turnx;
    InputAction breakPedal;

    InputAction uiNavigate, uiSubmit, uiCancel, uiPoint, uiClick, uiScrollWheel, uiMiddleClick, uiRightClick, uiTrackedDevicePosition, uiTrackedDeviceOrientation;

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
        //if(move.ReadValue<Vector2>() != Vector2.zero)
        //{
        //    Debug.Log("Move: " + move.ReadValue<Vector2>());
        //}
        //if (look.ReadValue<Vector2>() != Vector2.zero)
        //{
        //    Debug.Log("Look: " + look.ReadValue<Vector2>());
        //}


        Debug.Log("Turn: " + turn.ReadValue<Vector2>().x);
        Debug.Log("Move: " + move.ReadValue<Vector2>().x);


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
        move = inputActions.Player.Move;
        move.Enable();

        look = inputActions.Player.Look;
        look.Enable();

        turn = inputActions.Player.Turn;
        turn.Enable();

        breakPedal = inputActions.Player.BreakPedal;
        breakPedal.Enable();

        inputActions.Player.Fire.performed += Fire;
        inputActions.Player.Fire.Enable();


        //uiNavigate = inputActions.UI.Navigate;
        //uiSubmit = inputActions.UI.Submit;
        //uiCancel = inputActions.UI.Cancel;
        //uiPoint = inputActions.UI.Point;
        //uiClick = inputActions.UI.Click;
        //uiScrollWheel = inputActions.UI.ScrollWheel;
        //uiMiddleClick = inputActions.UI.MiddleClick;
        //uiRightClick = inputActions.UI.Point;
        //uiTrackedDevicePosition = inputActions.UI.TrackedDevicePosition;
        //uiTrackedDeviceOrientation = inputActions.UI.TrackedDeviceOrientation;

        //inputActions.UI.Navigate.performed += UI_Navigate;
        //inputActions.UI.Submit.performed += UI_Submit;
        //inputActions.UI.Cancel.performed += UI_Cancel;
        //inputActions.UI.Point.performed += UI_Point;
        //inputActions.UI.Click.performed += UI_Click;
        //inputActions.UI.ScrollWheel.performed += UI_ScrollWheel;
        //inputActions.UI.MiddleClick.performed += UI_MiddleClick;
        //inputActions.UI.Point.performed += UI_Point;
        //inputActions.UI.TrackedDevicePosition.performed += UI_TrackedDevicePosition;
        //inputActions.UI.TrackedDeviceOrientation.performed += UI_TrackedDeviceOrientation;

        //inputActions.UI.Navigate.Enable();
        //inputActions.UI.Submit.Enable();
        //inputActions.UI.Cancel.Enable();
        //inputActions.UI.Point.Enable();
        //inputActions.UI.Click.Enable();
        //inputActions.UI.ScrollWheel.Enable();
        //inputActions.UI.MiddleClick.Enable();
        //inputActions.UI.Point.Enable();
        //inputActions.UI.TrackedDevicePosition.Enable();
        //inputActions.UI.TrackedDeviceOrientation.Enable();
    }

    private void UI_TrackedDeviceOrientation(InputAction.CallbackContext context)
    {
        Debug.Log("UI_TrackedDeviceOrientation");
    }

    private void UI_TrackedDevicePosition(InputAction.CallbackContext context)
    {
        Debug.Log("UI_TrackedDevicePosition");
    }

    private void UI_MiddleClick(InputAction.CallbackContext context)
    {
        Debug.Log("UI_MiddleClick");
    }

    private void UI_ScrollWheel(InputAction.CallbackContext context)
    {
        Debug.Log("UI_ScrollWheel");
    }

    private void UI_Click(InputAction.CallbackContext context)
    {
        Debug.Log("UI_Click");
    }

    private void UI_Point(InputAction.CallbackContext context)
    {
        Debug.Log("UI_Point");
    }

    private void UI_Cancel(InputAction.CallbackContext context)
    {
        Debug.Log("UI_Cancel");
    }

    private void UI_Submit(InputAction.CallbackContext context)
    {
        Debug.Log("UI_Submit");
    }

    private void UI_Navigate(InputAction.CallbackContext context)
    {
        Debug.Log("UI_Navigate");
    }

    private void OnDisable()
    {
        move.Disable();
        look.Disable();
        inputActions.Player.Fire.Disable();


        //uiNavigate.Disable();
        //uiSubmit.Disable();
        //uiCancel.Disable();
        //uiPoint.Disable();
        //uiClick.Disable();
        //uiScrollWheel.Disable();
        //uiMiddleClick.Disable();
        //uiRightClick.Disable();
        //uiTrackedDevicePosition.Disable();
        //uiTrackedDeviceOrientation.Disable();
    }

    private void Fire(InputAction.CallbackContext context)
    {
        //throw new NotImplementedException();
        Debug.Log("Fire Button Pressed");
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

        rotateX += cameraContainer.transform.localRotation.eulerAngles.y;
        //rotateY += cameraContainer.transform.localRotation.eulerAngles.x;

        //rotateX = Mathf.Clamp(rotateX, -30f, +30f);
        rotateY = Mathf.Clamp(rotateY, lookUpClamp, lookDownClamp);

        //Debug.Log("XY: " + rotateX + "," + rotateY);

        //cameraContainer.transform.Rotate(0f, rotateX, 0f);
        //cameraContainer.transform.localRotation = Quaternion.Euler(rotateY, transform.localRotation.y, transform.localRotation.z);

        cameraContainer.transform.localRotation = Quaternion.Euler(rotateY, rotateX, 0f);
    }
}
