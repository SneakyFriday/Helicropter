using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "Helicropter/InputReader", order = 1)]
public class InputReader : ScriptableObject, PlayerInput.IPlayerActions
{
    private PlayerInput m_playerInput; 
    public PlayerInput PlayerInput => m_playerInput;
    public event UnityAction Jump;

    private void OnEnable()
    {
        m_playerInput ??= new PlayerInput();
        m_playerInput.Player.SetCallbacks(this);
        m_playerInput.Player.Enable();
    }

    private void OnDisable()
    {
        m_playerInput.Player.Disable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        if(context.started)
            Jump?.Invoke();
    }
}
