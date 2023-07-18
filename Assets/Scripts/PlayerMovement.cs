using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody _rb;
    Vector3 _joystickPos;
    float _horizontal;
    float _vertical;
    public float speed = 10;
    public float lerpRotateSpeed = 10;
    public DynamicJoystick dynamicJoystick;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();

    }


    void Update()
    {
        JoystickControl();

    }
    void MoveKeybord()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
        Vector3 vec = new Vector3(_horizontal, 0, _vertical);
        _rb.AddForce(vec * speed * Time.deltaTime);
    }
    void JoystickControl()
    {
        if (Input.GetMouseButton(0))
        {
            _horizontal = dynamicJoystick.Horizontal;
            _vertical = dynamicJoystick.Vertical;
            _joystickPos = new Vector3(_horizontal, 0, _vertical);
            //transform.position += _joystickPos;
           transform.position+=(_joystickPos * speed * Time.deltaTime);

            transform.rotation = Quaternion.LookRotation(_joystickPos*1);

        }
        else if (Input.GetMouseButtonUp(0))
        {
            _rb.velocity = Vector3.zero;
        }
    }

}
