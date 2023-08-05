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
    public FloatingJoystick floatingJoystick;
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
            _horizontal = floatingJoystick.Horizontal;
            _vertical = floatingJoystick.Vertical;
            _joystickPos = new Vector3(_horizontal, 0, _vertical);

            transform.position+=(_joystickPos * speed * Time.deltaTime);

            if (_joystickPos != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(_joystickPos);
                EventManager.StartWalkingAnim();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _rb.velocity = Vector3.zero;
            EventManager.StopWalkingAnim();
        }
    }

}
