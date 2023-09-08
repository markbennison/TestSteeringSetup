using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public TestSteeringSetup inputActions = new TestSteeringSetup();
    public event Action<InputActionMap> actionMapChange;

    void Start()
    {
        ToggleActionMap(inputActions.Player);
    }

    void Update()
    {
        
    }

    public void ToggleActionMap(InputActionMap actionMap)
    {
        if (actionMap.enabled)
        {
            return;
        }

        inputActions.Disable();
        actionMapChange?.Invoke(actionMap);
        actionMap.Enable();
    }
}
