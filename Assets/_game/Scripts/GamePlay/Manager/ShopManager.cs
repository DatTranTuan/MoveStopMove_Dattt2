using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using UnityEngine.UI;

public class ShopManager : Singleton<ShopManager>
{
    [SerializeField] private Transform weaponPos;
    [SerializeField] private Transform hatPos;
    [SerializeField] private Transform pantPos;
    [SerializeField] private Text textWeapon;

    [SerializeField] private HatItemUI hatItemUI;
    [SerializeField] private PantItemUI pantItemUI;

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
    private HatImage hatImage;
    private HatData currentSelectHatData;

    public List<PantData> listPant;
    private PantImage pantImage;
    private PantData currentSelectPantData;

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
            hatItemUI = Instantiate(hatItemUI, hatPos);
            hatItemUI.SetData(listHat[i], OnCLickHatButton);
        }
    }

    public void SpawnPantShop()
    {
        for (int i = 0; i < listPant.Count; i++)
        {
            pantItemUI = Instantiate(pantItemUI, pantPos);
            pantItemUI.SetData(listPant[i], OnCLickPantButton);
        }
    }

    public void OnCLickPantButton (PantData pantData)
    {
        currentSelectPantData = pantData;
    }

    public void OnClickEquipPantButton ()
    {
        LevelManager.Instance.player.CurrentPantType = currentSelectPantData.pantType;
        LevelManager.Instance.player.EquipPant();
    }

    public void OnCLickHatButton (HatData hatData)
    {
        currentSelectHatData = hatData;
    }

    public void OnClickEquipHatButton()
    {
        LevelManager.Instance.player.CurrentHatType = currentSelectHatData.hatType;
        LevelManager.Instance.player.EquipHat();
        //LevelManager.Instance.player.HatSpawn = hatData.hat;
    }

    public void OnDespawnHatShop()
    {
        for (int i = 0; i < listHat.Count; i++)
        {
            Destroy(hatItemUI);
        }
    }

    public void OnDespawnPantShop ()
    {
        for (int i = 0; i < listHat.Count; i++)
        {
            Destroy(pantItemUI);
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
