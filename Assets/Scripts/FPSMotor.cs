using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FPSMotor : MonoBehaviour
{
    //This is where I left of on page 17 of the document
    public void Move(Vector3 requestedMovement)
    {
        Debug.Log("Move: " + requestedMovement);
    }

    public void Turn(float turnAmount)
    {
        Debug.Log("Turn: " + turnAmount);
    }

    public void Look(float lookAmount)
    {
        Debug.Log("Look: " + lookAmount);
    }

    public void Jump(float jumpForce)
    {
        Debug.Log("Jump!");
    }
}
