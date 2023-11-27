using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected Animator animator;
    [SerializeField] protected LayerMask enemyLayerMask;
    [SerializeField] protected Transform firePos;

    protected bool CanAttack => !isAttack && isIdle == true && mainTarget != null;

    private Bot bot;

    protected Character mainTarget;
    protected List<Character> otherTarget = new List<Character>();

    protected Transform nearEnemy;
    protected Vector3 direc;

    protected WeaponData weaponData;
    protected Bullet bullet;
    protected int targetCount;
    protected float circleRadius = 4f;
    protected bool isDelay;
    private bool isIdle;
    private bool isAttack = false;
    private bool isDead;

    protected float bulletSpeed = 500f;
    protected List<Bullet> bulletList = new List<Bullet>();

    public bool IsAttack { get => isAttack; set => isAttack = value; }
    public bool IsIdle { get => isIdle; set => isIdle = value; }
    public Animator Animator { get => animator; set => animator = value; }
    public bool IsDead { get => isDead; set => isDead = value; }

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
            transform.LookAt(mainTarget.transform.position);
            isAttack = true;
            SetBoolAnimation();
            StartCoroutine(DelayBeforeAttack(0.25f));
        }
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
        if (Bullet.stack.Count > 0)
        {
            // if pool have bullet
            spawnBullet = Bullet.stack.Pop();
            spawnBullet.transform.position = firePos.position;
            spawnBullet.transform.rotation = firePos.rotation;
        }
        else
        {
            // if pool doesn't have any bullet then it will spawn
            spawnBullet = Instantiate(bullet, firePos.position, firePos.rotation);
        }
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
        if (mainTarget is null)
        {
            mainTarget = target;
        }
        else
        {
            otherTarget.Add(target);
        }
    }

    protected void OnTargetExit(Character target)
    {
        if (mainTarget == target)
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

    public void SpawnWeapon()
    {
        Instantiate(weaponData.weapon, firePos);
    }
}
