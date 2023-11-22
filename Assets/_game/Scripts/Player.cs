using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : Character
{
    private Joystick joystick;
    private Rigidbody rb;
    private Player player;


    private void Start()
    {
        player = LevelManager.Instance.player;
        joystick = JoystickManager.Instance._joystick;
        rb = player.GetComponent<Rigidbody>();
    }

    protected override void FixedUpdate()
    {
        if (isMoving == true)
        {
            Attack();

            rb.velocity = new Vector3(joystick.Horizontal * moveSpeed, rb.velocity.y, joystick.Vertical /*0*/ * moveSpeed);
            // joystick vertical > => joystick = 0
            if (joystick.Horizontal != 0 || joystick.Vertical != 0)
            {
                transform.rotation = Quaternion.LookRotation(rb.velocity);
                IsIdle = false;
            }
            else
            {
                IsIdle = true;
            }
        }
        base.FixedUpdate();
    }


}
