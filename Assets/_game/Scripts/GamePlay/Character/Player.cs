using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Rigidbody rb;

    private WeaponType currentWeaponType;

    private Joystick joystick;
    private Player player;

    //private override WeaponData weaponData;

    private void Start()
    {
        SpawnWeapon();
        currentWeaponType = WeaponType.Hammer;
        player = LevelManager.Instance.player;
        joystick = JoystickManager.Instance._joystick;
    }

    public void OnInit()
    {
        if (weaponData == null)
        {
            weaponData = DataManager.Instance.GetWeaponData(currentWeaponType);
        }

        if (bullet == null)
        {
            bullet = weaponData.bullet;
        }
    }

    protected void FixedUpdate()
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
                SetBoolAnimation();
            }
            else
            {
                IsIdle = true;
                SetBoolAnimation();
            }
        }
    }

    //public void SpawnWeapon()
    //{ 
    //    Instantiate(weaponData.weapon, firePos);
    //}

}
