using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : Character
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject targetPoint;

    private WeaponType currentWeaponType;
    private HatType currentHatType;

    private Joystick joystick;
    private Player player;
    private PlayerData playerData;


    public Joystick Joystick { get => joystick; set => joystick = value; }

    private void Start()
    {
        player = LevelManager.Instance.player;
    }

    public void OnInit()
    {
        IsAlive = true;
        if (weaponData == null)
        {
            //Take Data from Player Data
            currentWeaponType = DataManager.Instance.LoadPlayerData().weaponTypeData;
            weaponData = DataManager.Instance.GetWeaponData(currentWeaponType);
        }
        ChangeWeapon(currentWeaponType);
        Debug.Log(currentWeaponType);
        bullet = weaponData.bullet;

        //hatType = HatType.Arrow;
        if (hatData == null)
        {
            hatData = DataManager.Instance.GetHatData(currentHatType);
        }
        ChangeHat(currentHatType);
    }

    protected void FixedUpdate()
    {
        if (isPlayAble)
        {
            rb.velocity = new Vector3(Joystick.Horizontal * moveSpeed, rb.velocity.y, Joystick.Vertical /*0*/ * moveSpeed);
            // joystick vertical > => joystick = 0
            if (Joystick.Horizontal != 0 || Joystick.Vertical != 0)
            {
                StopAllCoroutines();
                IsIdle = false;
                SetBoolAnimation();
                //transform.rotation = Quaternion.LookRotation(rb.velocity);
                //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rb.velocity), 0.2f);
                transform.forward = rb.velocity;
                if (IsAttack)
                {
                    ResetAttack();
                }
            }
            else if (!IsAttack)
            {
                IsIdle = true;
                SetBoolAnimation();
            }

            if (mainTarget != null)
            {
                Attack();
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
        IsAlive = false;
        IsDead = true;
        IsAttack = false;
        IsIdle = false;
        isPlayAble = false;
        SetBoolAnimation();
        targetPoint.SetActive(false);
        targetPoint.transform.position = transform.position;
    }

    public void EquipWeapon()
    {
        LevelManager.Instance.player.currentWeaponType = ShopManager.Instance.listWeapon[ShopManager.Instance.weaponIndex].weaponType;
        ChangeWeapon(LevelManager.Instance.player.currentWeaponType);
        LevelManager.Instance.player.WeponSpawn = ShopManager.Instance.listWeapon[ShopManager.Instance.weaponIndex].weapon;
        DataManager.Instance.SeekWeaponPlayerData(LevelManager.Instance.player.currentWeaponType);
    }

    public void EquipHat()
    {
        LevelManager.Instance.player.currentHatType = ShopManager.Instance.listHat[ShopManager.Instance.hatIndex].hatType;
        ChangeHat(LevelManager.Instance.player.currentHatType);
        LevelManager.Instance.player.HatSpawn = ShopManager.Instance.listHat[ShopManager.Instance.hatIndex].hat;

        DataManager.Instance.SeekHatPlayerData(LevelManager.Instance.player.currentHatType);
    }
}
