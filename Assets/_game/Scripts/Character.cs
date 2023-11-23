using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected Animator animator;
    [SerializeField] protected LayerMask enemyLayerMask;
    [SerializeField] protected Bullet bulletPrefab;
    [SerializeField] protected Bullet bulletSpawn;
    [SerializeField] protected Transform firePos;

    protected Character mainTarget;
    protected List<Character> otherTarget = new List<Character>();

    protected Transform nearEnemy;
    protected Vector3 direc;

    protected WeaponData weaponData;
    protected int targetCount;
    protected float circleRadius = 4f;
    protected bool isDelay;
    private bool isIdle;
    private bool isAttack = false;
    protected bool isMoving = true;

    protected float bulletSpeed = 500f;
    protected List<Bullet> bulletList = new List<Bullet>();

    public bool IsAttack { get => isAttack; set => isAttack = value; }
    public bool IsIdle { get => isIdle; set => isIdle = value; }
    public Animator Animator { get => animator; set => animator = value; }

    protected virtual void FixedUpdate()
    {
        SetBoolAnimation();
    }

    protected void SetBoolAnimation()
    {
        animator.SetBool(CacheString.ATTACK_ANIMATION, isAttack);
        animator.SetBool(CacheString.IDLE_ANIMATION, isIdle);
    }

    public void Attack()
    {
        if (!isAttack && isIdle == true && mainTarget != null)
        {
            transform.LookAt(mainTarget.transform.position);
            isAttack = true;
            StartCoroutine(DelayBeforeAttack(0.35f));
        }
        //else
        //{
        //    IsAttack = false;
        //    Animator.SetBool("IsAttack", IsAttack);
        //}
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
    }

    public void Fire()
    {
        if (mainTarget == null)
        {
            direc = transform.forward;
        }
        else
        {
            direc = mainTarget.transform.position - transform.position;
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
            Debug.Log(bulletPrefab == null);
            Debug.Log(firePos == null);
            spawnBullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        }
        Bullet bulletOjb = spawnBullet.GetComponent<Bullet>();
        direc.y = 0f;
        bulletOjb.Init(direc, this, OnHitTarget);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(CacheString.BULLET_TAG))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            Bot bot = GetComponent<Bot>();
            if (bullet.Attacker != this)
            {
                bullet.OnHitTarget(this);
                //Destroy(this.gameObject);
                this.gameObject.SetActive(false);
                bullet.ReturnToPool();
                bot.ReturnToPool();
            }
        }

        if (other.CompareTag(CacheString.ENEMY_TAG) || other.CompareTag(CacheString.PLAYER_TAG))
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
        if (other.gameObject.layer == 3 || other.gameObject.layer == 6)
        {
            Character target = other.GetComponent<Character>();
            OnTargetExit(target);
        }
    }

    protected void OnTargetEnter(Character target)
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

    protected void OnHitTarget(Character target)
    {
        OnTargetExit(target);
    }

    public void SpawnWeapon()
    {
        Instantiate(weaponData.weapon, firePos);
    }
}
