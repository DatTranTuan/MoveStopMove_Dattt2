using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : Character
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject targetPoint;

    private WeaponType currentWeaponType = WeaponType.Hammer;

    private Joystick joystick;
    private Player player;

    private void Start()
    {
        SpawnWeapon();
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
        if (isPlayAble)
        {

            Attack();

            rb.velocity = new Vector3(joystick.Horizontal * moveSpeed, rb.velocity.y, joystick.Vertical /*0*/ * moveSpeed);
            // joystick vertical > => joystick = 0
            if (joystick.Horizontal != 0 || joystick.Vertical != 0)
            {
                //transform.rotation = Quaternion.LookRotation(rb.velocity);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rb.velocity), 0.2f);
                IsIdle = false;
                SetBoolAnimation();
            }
            else
            {
                IsIdle = true;
                SetBoolAnimation();
            }

            if (mainTarget != null)
            {
                targetPoint.transform.position = mainTarget.transform.position + Vector3.down;
                targetPoint.SetActive(true);
            }
            else
            {
                targetPoint.SetActive(false);
            }
        }
    }

    public void OnDeath()
    {
        IsDead = true;
        IsAttack = false;
        IsIdle = false;
        isPlayAble = false;
        SetBoolAnimation();
        targetPoint.SetActive(false);
        targetPoint.transform.position = transform.position;
    }

    //public void SpawnWeapon()
    //{ 
    //    Instantiate(weaponData.weapon, firePos);
    //}

}
