using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class ShopManager : Singleton<ShopManager>
{
    [SerializeField] private Transform weaponPos;

    private WeaponData weaponData;
    private WeaponType currentWeaponShop;

    private List<WeaponData> listWeapon;
    private int index;
    private Weapon currentWeapon;
    private Weapon lastWeapon;
    private Player player;

    public List<Weapon> list = new List<Weapon>();

    private void Start()
    {
        OnInit();
    }

    public void SpawnWeaponShop(Weapon wepon)
    {
        Weapon wp = LeanPool.Spawn(wepon, weaponPos);
        list.Add(wp);
        if (list.Count == 0)
        {
            currentWeapon = wp;
            lastWeapon = currentWeapon;
        }
        else if (list.Count > 0)
        {
            lastWeapon = list[0];
        }
    }

    public void OnInit()
    {
        index = 0;
        listWeapon = DataManager.Instance.listWeaponData;
        currentWeaponShop = WeaponType.Axe;
        if (weaponData == null)
        {
            weaponData = DataManager.Instance.GetWeaponData(currentWeaponShop);
        }
        //SpawnWeapon();
    }

    public void OnDespawnWeapon()
    {
        LeanPool.Despawn(lastWeapon);
        list.RemoveAt(0);
        //Destroy(lastWeapon.gameObject);
        //lastWeapon.gameObject.SetActive(false);
    }

    public void SpawnWeapon()
    {
        SpawnWeaponShop(listWeapon[index].weapon);
    }

    public void NextWeapon()
    {
        index++;
        SpawnWeaponShop(listWeapon[index].weapon);
    }

    public void PreviousWeapon()
    {
        index--;
        SpawnWeaponShop(listWeapon[index].weapon);
    }

    public void EquipWeapon()
    {
        LevelManager.Instance.player.WeponSpawn = listWeapon[index].weapon;
        LevelManager.Instance.player.Bullet = listWeapon[index].bullet;
        Weapon spawn = LevelManager.Instance.player.WeponSpawn;
        Instantiate(spawn, LevelManager.Instance.player.FirePos);
    }
}
