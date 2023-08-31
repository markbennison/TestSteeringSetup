using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLocomotion : MonoBehaviour
{

    private TestSteeringSetup inputActions;
    private InputAction move;

    private void Awake()
    {
        inputActions = new TestSteeringSetup();
    }

    void Start()

    {
        
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Debug.Log("Move: " + move.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        move = inputActions.Player.Move;
        move.Enable();

        inputActions.Player.Fire.performed += Fire;
        inputActions.Player.Fire.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        inputActions.Player.Fire.Disable();
    }

    private void Fire(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

}
