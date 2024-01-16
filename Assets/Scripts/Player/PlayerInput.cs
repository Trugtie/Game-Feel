using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public FrameInput FrameInput { get; private set; }

    private PlayerInputActions _playerInputActions;

    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _jetpackAction;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();

        _moveAction = _playerInputActions.Player.Move;
        _jumpAction = _playerInputActions.Player.Jump;
        _jetpackAction = _playerInputActions.Player.Jetpack;
    }

    private void OnEnable()
    {
        _playerInputActions.Enable();
    }

    private void OnDisable()
    {
        _playerInputActions.Disable();
    }

    private void Update()
    {
        FrameInput = GatherInput();
    }

    private FrameInput GatherInput()
    {
        return new FrameInput
        {
            Move = _moveAction.ReadValue<Vector2>(),
            Jump = _jumpAction.WasPressedThisFrame(),
            Jetpack = _jetpackAction.WasPressedThisFrame(),
        };
    }
}

public struct FrameInput
{
    public Vector2 Move;
    public bool Jump;
    public bool Jetpack;
}
