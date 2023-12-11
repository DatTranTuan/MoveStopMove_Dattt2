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
    [SerializeField] private GameObject clothesShopCanvas;
    [SerializeField] private GameObject loseCanvas;

    [SerializeField] private Button buttonClickPlay;

    [SerializeField] private Button buttonClickWeaponShop;
    [SerializeField] private Button buttonCloseWeaponShop;
    [SerializeField] private Button buttonNextWeapon;
    [SerializeField] private Button buttonPreWeapon;
    [SerializeField] private Button buttonEquipWeapon;

    [SerializeField] private Button buttonClickClothesShop;
    [SerializeField] private Button buttonCloseClothesShop;

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

        buttonClickClothesShop.onClick.AddListener(OnClickClothesShop);
        buttonCloseClothesShop.onClick.AddListener(OnCloseClothesShop);
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

    public void OnClickClothesShop()
    {
        menuStartCanvas.SetActive(false);
        clothesShopCanvas.SetActive(true);
        ShopManager.Instance.SpawnHatShop();

    }

    public void OnCloseClothesShop()
    {
        menuStartCanvas.SetActive(true);
        clothesShopCanvas.SetActive(false);
        ShopManager.Instance.OnDespawnHatShop();
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(CacheString.SCENE_NAME);
    }
}
