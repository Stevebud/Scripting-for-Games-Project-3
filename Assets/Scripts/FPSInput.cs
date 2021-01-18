using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FPSInput : MonoBehaviour
{
    [SerializeField] bool _invertVertical = false;

    public event Action<Vector3> MoveInput = delegate { };
    public event Action<Vector3> RotateInput = delegate { };
    public event Action JumpInput = delegate { };
    public event Action SkiInput = delegate { };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectMoveInput();
        DetectRotateInput();
        DetectJumpInput();
        DetectSkiInput();
    }

    void DetectMoveInput()
    {
        //process input as a 0 or 1 value if we have it
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        //if we have either input
        if(xInput != 0 || yInput != 0)
        {
            //convert to local directions based on player orientation
            Vector3 _horizontalMovement = xInput * transform.right;
            Vector3 _verticalMovement = yInput * transform.forward;
            //combine movement into single vector
            Vector3 movement = (_horizontalMovement + _verticalMovement).normalized;
            //notify that we have movement
            MoveInput?.Invoke(movement);
        }
    }

    void DetectRotateInput()
    {
        //get inputs from input controller
        float xInput = Input.GetAxisRaw("Mouse X");
        float yInput = Input.GetAxisRaw("Mouse Y");

        if(xInput != 0 || yInput != 0)
        {
            //account for inverted camera, if specified
            if(_invertVertical == true)
            {
                yInput = -yInput;
            }
            //mouse left/right should be x axis, up/down should be y
            Vector3 rotation = new Vector3(yInput, xInput, 0);
            //notify that we have rotated
            RotateInput?.Invoke(rotation);
        }
    }
    void DetectJumpInput()
    {
        //Right mouse button press
        if(Input.GetMouseButton(1))
        {
            JumpInput?.Invoke();
        }
    }

    void DetectSkiInput()
    {
        //Spacebar press or lift
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyUp(KeyCode.Space))
        {
            SkiInput?.Invoke();
        }
    }
}
