using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using UnityEditor.Experimental.GraphView;

public class Character : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected Animator animator;
    [SerializeField] protected LayerMask enemyLayerMask;
    [SerializeField] protected Transform firePos;
    [SerializeField] protected Transform headPos;
    [SerializeField] protected Transform armPos;
    [SerializeField] protected SkinnedMeshRenderer skinned;

    protected bool CanAttack => !isAttack && isIdle == true && mainTarget != null;

    private Bot bot;

    protected Character mainTarget;
    protected List<Character> otherTarget = new List<Character>();

    protected Transform nearEnemy;
    protected Vector3 direc;

    protected int kill;

    protected WeaponData weaponData;
    protected HatData hatData;
    protected PantData pantData;
    protected ShieldData shieldData;

    protected Bullet bullet;
    protected int targetCount;
    protected float circleRadius = 4f;
    protected bool isDelay;
    protected bool isPlayAble = true;

    private Weapon weponSpawn;
    private Hat hatSpawn;
    private Shield shieldSpawn;
    private Sprite pantSpawn;

    private bool isIdle;
    private bool isAttack = false;
    private bool isDead;
    private bool isAlive;
    private bool inRange; // Later: Check inRange or not 

    protected float bulletSpeed = 500f;
    protected List<Bullet> bulletList = new List<Bullet>();

    protected Weapon currentWeapon;

    public bool IsAttack { get => isAttack; set => isAttack = value; }
    public bool IsIdle { get => isIdle; set => isIdle = value; }
    public Animator Animator { get => animator; set => animator = value; }
    public bool IsDead { get => isDead; set => isDead = value; }
    public bool IsPlayAble { get => isPlayAble; set => isPlayAble = value; }
    public Bot Bot { get => bot; }
    public bool IsAlive { get => isAlive; set => isAlive = value; }
    public Transform FirePos { get => firePos; set => firePos = value; }
    public Bullet Bullet { get => bullet; set => bullet = value; }
    public Weapon WeponSpawn { get => weponSpawn; set => weponSpawn = value; }
    public Hat HatSpawn { get => hatSpawn; set => hatSpawn = value; }
    public Shield ShieldSpawn { get => shieldSpawn; set => shieldSpawn = value; }
    public int Kill { get => kill; set => kill = value; }

    //public Weapon CurrentWeapon { get => currentWeapon; set => currentWeapon = value; }

    public void SetBoolAnimation()
    {
        animator.SetBool(CacheString.ATTACK_ANIMATION, isAttack);
        animator.SetBool(CacheString.IDLE_ANIMATION, isIdle);
        animator.SetBool(CacheString.DEAD_ANIMATION, isDead);
    }

    public void Attack()
    {
        if (CanAttack)
        {
            transform.LookAt(new Vector3(mainTarget.transform.position.x, transform.position.y, mainTarget.transform.position.z));
            isAttack = true;
            SetBoolAnimation();
            StartCoroutine(DelayBeforeAttack(0.25f));
        }
    }

    public void ResetAttack()
    {
        isAttack = false;
    }

    protected IEnumerator DelayBeforeAttack(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        StartCoroutine(DelayAttack(1f));
    }

    protected IEnumerator DelayAttack(float delayTime)
    {
        Fire();
        yield return new WaitForSeconds(delayTime);
        isAttack = false;
        SetBoolAnimation();
    }

    public void Fire()
    {
        if (mainTarget == null)
        {
            direc = transform.forward;
        }
        else
        {
            direc = mainTarget.transform.position - firePos.position;
        }

        // Shoot
        Bullet spawnBullet;
        // if pool doesn't have any bullet then it will spawn
        spawnBullet = LeanPool.Spawn(bullet, firePos.position, Quaternion.identity);
        spawnBullet.transform.localScale = (1f + Mathf.Log10(kill + 1f)) * Vector3.one;
        direc.y = 0f;
        spawnBullet.Init(direc, this, OnHitTarget);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == CacheString.CHARACTER_LAYER)
        {
            Character target = other.GetComponent<Character>();
            if (target != this)
            {
                OnTargetEnter(target);
            }
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == CacheString.CHARACTER_LAYER)
        {
            Character target = other.GetComponent<Character>();
            OnTargetExit(target);
        }
    }

    public void OnTargetEnter(Character target)
    {
        if (IsAlive)
        {
            if (mainTarget is null)
            {
                mainTarget = target;
            }
            else
            {
                otherTarget.Add(target);
            }
        }
    }

    protected void OnTargetExit(Character target)
    {
        if (mainTarget == target || !IsAlive)
        {
            if (otherTarget.Count > 0)
            {
                mainTarget = otherTarget[0];
                otherTarget.RemoveAt(0);
            }
            else
            {
                mainTarget = null;
            }
        }
        else if (!IsAlive)
        {
            otherTarget.Remove(target);
        }
        else
        {
            otherTarget.Remove(target);
        }
    }

    protected void OnHitTarget(Character target, Bullet bullet)
    {
        OnTargetExit(target);
        bullet.ReturnToPool();
    }

    public void ChangeWeapon(WeaponType weaponType)
    {
        weaponData = DataManager.Instance.listWeaponData[(int)weaponType];

        if (WeponSpawn != null)
        {
            Destroy(WeponSpawn.gameObject);
        }

        WeponSpawn = Instantiate(weaponData.weapon, firePos);
    }

    public void ChangeHat (HatType hatType)
    {
        hatData = DataManager.Instance.listHatData[(int)hatType];
        if (hatSpawn != null)
        {
            Destroy(hatSpawn.gameObject);
        }
        hatSpawn = Instantiate(hatData.hat, headPos);
    }

    public void ChangePant(PantType pantType)
    {
        skinned.material = DataManager.Instance.pantDataSO.listPantData[(int)pantType].material;
    }

    public void ChangeShield(ShieldType shieldType)
    {
        shieldData = DataManager.Instance.listShieldData[(int)shieldType];
        if (shieldSpawn != null)
        {
            Destroy(shieldSpawn.gameObject);
        }
        shieldSpawn = Instantiate(shieldData.shield, armPos);
    }
}
