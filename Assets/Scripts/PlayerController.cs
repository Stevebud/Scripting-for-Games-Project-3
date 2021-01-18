using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FPSInput))]
[RequireComponent(typeof(FPSMotor))]
public class PlayerController : MonoBehaviour
{
    FPSInput _input = null;
    FPSMotor _motor = null;

    [SerializeField] float _moveSpeed = .1f;
    [SerializeField] float _turnSpeed = 6f;
    [SerializeField] float _jumpStrength = 10f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Awake()
    {
        _input = GetComponent<FPSInput>();
        _motor = GetComponent<FPSMotor>();
    }

    private void OnEnable()
    {
        _input.MoveInput += OnMove;
        _input.RotateInput += OnRotate;
        _input.JumpInput += OnJump;
        _input.SkiInput += OnSki;
    }

    private void OnDisable()
    {
        _input.MoveInput -= OnMove;
        _input.RotateInput -= OnRotate;
        _input.JumpInput -= OnJump;
        _input.SkiInput -= OnSki;
    }

    private void OnMove(Vector3 movement)
    {
        _motor.Move(movement * _moveSpeed);
    }

    private void OnRotate(Vector3 rotation)
    {
        _motor.Look(rotation.x * _turnSpeed);
        _motor.Turn(rotation.y * _turnSpeed);
    }

    private void OnJump()
    {
        _motor.Jump(_jumpStrength);
    }

    private void OnSki()
    {
        _motor.Ski();
    }
}
