using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Lean.Pool;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject menuStartCanvas;
    [SerializeField] private GameObject weaponShopCanvas;
    [SerializeField] private GameObject clotheShopCanvas;
    [SerializeField] private GameObject hatShopCanvas;
    [SerializeField] private GameObject pantShopCanvas;
    [SerializeField] private GameObject loseCanvas;

    [SerializeField] private Button buttonClickPlay;
    [SerializeField] private Button buttonClickClotheShop;

    [SerializeField] private Button buttonClickWeaponShop;
    [SerializeField] private Button buttonCloseWeaponShop;
    [SerializeField] private Button buttonNextWeapon;
    [SerializeField] private Button buttonPreWeapon;
    [SerializeField] private Button buttonEquipWeapon;

    [SerializeField] private Button buttonClickHatShop;
    [SerializeField] private Button buttonCloseHatShop;
    [SerializeField] private Button buttonSelectHat;

    [SerializeField] private Button buttonClickPantShop;
    [SerializeField] private Button buttonClosePantShop;
    [SerializeField] private Button buttonSelectPantShop;

    [SerializeField] private Button buttonReloadGame;

    private ShopManager shopManager;
    private Player player;

    private void OnEnable()
    {
        buttonClickPlay.onClick.AddListener(OnClickPlayButton);

        buttonClickWeaponShop.onClick.AddListener(OnClickWeaponShopButton);
        buttonCloseWeaponShop.onClick.AddListener(OnCloseWeaponShop);
        buttonNextWeapon.onClick.AddListener(PressNextWeaponShop);
        buttonPreWeapon.onClick.AddListener(PressPrevWeaponShop);
        buttonEquipWeapon.onClick.AddListener(PressEquipWeaponShop);

        buttonReloadGame.onClick.AddListener(ReloadGame);

        buttonClickHatShop.onClick.AddListener(OnClickHatShop);
        buttonCloseHatShop.onClick.AddListener(OnCloseHatShop);
        buttonSelectHat.onClick.AddListener(OnClickSelectHatButton);

        buttonClickPantShop.onClick.AddListener(OnClickPantShop);
        buttonClosePantShop.onClick.AddListener(OnClosePantShop);
        buttonSelectPantShop.onClick.AddListener(OnSelectPantButton);

        buttonClickClotheShop.onClick.AddListener(OnClickClotheShop);
    }

    private void Start()
    {
        Time.timeScale = 0;
    }

    public void OnClickPlayButton()
    {
        Time.timeScale = 1;
        menuStartCanvas.SetActive(false);
        CameraFollow.Instance.SmoothCameraChange();
    }

    public void OnClickWeaponShopButton()
    {
        menuStartCanvas.SetActive(false);
        weaponShopCanvas.SetActive(true);
        ShopManager.Instance.SpawnWeapon();
    }

    public void OnCloseWeaponShop()
    {
        ShopManager.Instance.OnDespawnWeapon();
        menuStartCanvas.SetActive(true);
        weaponShopCanvas.SetActive(false);
    }

    public void OnLoseUI()
    {
        loseCanvas.SetActive(true);
    }

    public void PressNextWeaponShop()
    {
        if (ShopManager.Instance.weaponIndex < 2)
        {
            ShopManager.Instance.OnDespawnWeapon();
        }
        ShopManager.Instance.NextWeapon();
    }

    public void PressPrevWeaponShop()
    {
        if (ShopManager.Instance.weaponIndex > 0)
        {
            ShopManager.Instance.OnDespawnWeapon();
        }
        ShopManager.Instance.PreviousWeapon();
    }

    public void PressEquipWeaponShop()
    {
        Destroy(LevelManager.Instance.player.WeponSpawn.gameObject);
        LevelManager.Instance.player.EquipWeapon();
    }

    public void OnClickClotheShop ()
    {
        menuStartCanvas.SetActive(false);
        clotheShopCanvas.SetActive(true);
    }

    public void OnClickHatShop()
    {
        menuStartCanvas.SetActive(false);
        hatShopCanvas.SetActive(true);
        pantShopCanvas.SetActive(false);
        ShopManager.Instance.SpawnHatShop();

    }

    public void OnCloseHatShop()
    {
        menuStartCanvas.SetActive(true);
        hatShopCanvas.SetActive(false);
        ShopManager.Instance.OnDespawnHatShop();
    }

    public void OnClickSelectHatButton()
    {
        ShopManager.Instance.OnClickEquipHatButton();
    }

    public void OnClickPantShop()
    {
        menuStartCanvas.SetActive(false);
        hatShopCanvas.SetActive(false);
        pantShopCanvas.SetActive(true);
        ShopManager.Instance.SpawnPantShop();
    }

    public void OnClosePantShop() 
    {
        menuStartCanvas.SetActive(true);
        pantShopCanvas.SetActive(false);
        ShopManager.Instance.OnDespawnPantShop();
    }

    public void OnSelectPantButton ()
    {
        ShopManager.Instance.OnClickEquipPantButton();
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(CacheString.SCENE_NAME);
    }
}
