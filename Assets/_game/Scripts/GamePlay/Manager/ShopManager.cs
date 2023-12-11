using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using UnityEngine.UI;

public class ShopManager : Singleton<ShopManager>
{
    [SerializeField] private Transform weaponPos;
    [SerializeField] private Transform hatPos;
    [SerializeField] private Text textWeapon;

    private WeaponData weaponData;
    private WeaponType currentWeaponShop;

    public int weaponIndex;
    public int hatIndex;

    public List<WeaponData> listWeapon;
    private Weapon currentWeapon;
    private Weapon lastWeapon;
    private PlayerData playerData;

    public List<Weapon> list = new List<Weapon>();

    public List<HatData> listHat;
    //private HatData hatData;
    //private HatType hatType;
    private Hat hat;

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
        weaponIndex = 0;
        listWeapon = DataManager.Instance.listWeaponData;
        listHat = DataManager.Instance.listHatData;

        currentWeaponShop = WeaponType.Axe;
        if (weaponData == null)
        {
            weaponData = DataManager.Instance.GetWeaponData(currentWeaponShop);
        }


        //hatType = HatType.Arrow;
        //if (hatData == null)
        //{
        //    hatData = DataManager.Instance.GetHatData(hatType);
        //}
    }

    public void SpawnHatShop()
    {
        for (int i = 0; i < listHat.Count; i++)
        {
            Debug.Log("inside");
            hat = Instantiate(listHat[i].hat, hatPos);
        }
    }

    public void OnDespawnHatShop()
    {
        for (int i = 0; i < listHat.Count; i++)
        {
            Destroy(hat);
        }
    }

    public void OnDespawnWeapon()
    {
        LeanPool.Despawn(lastWeapon);
        list.RemoveAt(0);
    }

    public void SpawnWeapon()
    {
        SpawnWeaponShop(listWeapon[weaponIndex].weapon);
    }

    public void NextWeapon()
    {
        if (weaponIndex < 2)
        {
            weaponIndex++;
            WeaponData weaponData = listWeapon[weaponIndex];
            SpawnWeaponShop(weaponData.weapon);
            textWeapon.text = weaponData.weaponType.ToString();
        }
    }

    public void PreviousWeapon()
    {
        if (weaponIndex > 0)
        {
            weaponIndex--;
            WeaponData weaponData = listWeapon[weaponIndex];
            SpawnWeaponShop(weaponData.weapon);
            textWeapon.text = weaponData.weaponType.ToString();
        }
    }

}
