using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FPSMotor : MonoBehaviour
{
    [SerializeField] Camera _camera = null;

    Rigidbody _rigidbody = null;

    [SerializeField] float _cameraAngleLimit = 70f;

    private float _currentCameraRotationX = 0;

    [SerializeField] float walkForceMultiplier = 100f;

    Vector3 _movementThisFrame = Vector3.zero;
    float _turnAmountThisFrame = 0;
    float _lookAmountThisFrame = 0;

    bool isSkiing = false;
    Collider _collider = null;
    [SerializeField] PhysicMaterial frictionlessMaterial = null;
    [SerializeField] PhysicMaterial walkingMaterial = null;

    [SerializeField] GroundDetector _groundDetector = null;
    bool _isGrounded = false;
    public void Move(Vector3 requestedMovement)
    {
        _movementThisFrame = requestedMovement;
    }

    public void Turn(float turnAmount)
    {
        _turnAmountThisFrame = turnAmount;
    }

    public void Look(float lookAmount)
    {
        _lookAmountThisFrame = lookAmount;
    }

    public void Jump(float jumpForce)
    {
        _rigidbody.AddForce(Vector3.up * jumpForce);
    }

    public void Ski()
    {
        if(isSkiing)
        {
            isSkiing = false;
            _collider.material = walkingMaterial;
        } else
        {
            isSkiing = true;
            _collider.material = frictionlessMaterial;
        }
    }

    private void OnEnable()
    {
        _groundDetector.GroundDetected += OnGroundDetected;
        _groundDetector.GroundVanished += OnGroundVanished;
    }

    private void OnDisable()
    {
        _groundDetector.GroundDetected -= OnGroundDetected;
        _groundDetector.GroundVanished -= OnGroundVanished;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _collider.material = walkingMaterial;
    }

    private void FixedUpdate()
    {
        ApplyMovement(_movementThisFrame);
        ApplyTurn(_turnAmountThisFrame);
        ApplyLook(_lookAmountThisFrame);
    }

    void ApplyMovement(Vector3 moveVector)
    {
        if (moveVector == Vector3.zero)
            return;
        //_rigidbody.MovePosition(_rigidbody.position + moveVector);
        if(isSkiing == true || _isGrounded == false)
        {
            _rigidbody.AddForce(moveVector * walkForceMultiplier);
        } else
        {
            _rigidbody.MovePosition(_rigidbody.position + moveVector);
        }
        _movementThisFrame = Vector3.zero;
    }

    void ApplyTurn(float rotateAmount)
    {
        if (rotateAmount == 0)
        {
            Debug.Log(rotateAmount);
            return;
        }
            
        Quaternion newRotation = Quaternion.Euler(0, rotateAmount, 0);
        Debug.Log(newRotation.eulerAngles.y);
        _rigidbody.MoveRotation(_rigidbody.rotation * newRotation);
        _turnAmountThisFrame = 0;
    }

    void ApplyLook(float lookAmount)
    {
        if (lookAmount == 0)
            return;

        _currentCameraRotationX -= lookAmount;
        _currentCameraRotationX = Mathf.Clamp(_currentCameraRotationX, -_cameraAngleLimit, _cameraAngleLimit);
        _camera.transform.localEulerAngles = new Vector3(_currentCameraRotationX, 0, 0);
        _lookAmountThisFrame = 0;
    }

    void OnGroundDetected()
    {
        _isGrounded = true;
        //Land?.Invoke();
    }

    void OnGroundVanished()
    {
        _isGrounded = false;
    }
}
