using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Bot : Character
{
    private WeaponType currentWeaponType;

    [SerializeField] public NavMeshAgent agent;
    [SerializeField] private Bot botPrefab;
    [SerializeField] private HUDIndicator.IndicatorOffScreen offScreen;
    //[SerializeField] private Transform botContainer;

    //[SerializeField] private GameObject indicator;
    //[SerializeField] private MeshRenderer meshRenderer;

    //public static Stack<Bot> stack = new Stack<Bot>();
    public bool isTarget => Vector3.Distance(transform.position, newPos) < 0.1f;

    public Vector3 moveDirection;

    private Vector3 newPos;
    private Vector3 randomPos;

    private float wanderRadius = 20f;
    private IState currentState;


    private void Start()
    {
        OnInit();
        ChangeWeapon(currentWeaponType);
        //SpawnBotWhenStart();
    }

    public void OnInit()
    {
        InRange = false;
        this.gameObject.layer = CacheString.CHARACTER_LAYER;
        if (weaponData == null)
        {
            currentWeaponType = WeaponType.Hammer;
            weaponData = DataManager.Instance.GetWeaponData(currentWeaponType);
        }

        if (bullet == null)
        {
            bullet = weaponData.bullet;
        }

        IsAlive = true;
        offScreen.SetRenderer(Check.Instance.indicatorRenderer);
    }

    protected void Update()
    {
        if (isPlayAble)
        {
            if (currentState != null)
            {
                currentState.OnExecute(this);
            }
        }
    }

    private void OnEnable()
    {
        randomPos = new Vector3(UnityEngine.Random.Range(-10, 11), 0, UnityEngine.Random.Range(-9, 10));
        IsAttack = false;
        SetBoolAnimation();
        ChangeState(new IdleState());
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if (currentState != null)
        {
            newState.OnEnter(this);
        }
    }

    //private void SpawnBot()
    //{
    //    if (stack.Count > 0)
    //    {
    //        Bot spawnBot;
    //        spawnBot = stack.Pop();
    //        spawnBot.gameObject.SetActive(true);
    //        spawnBot.gameObject.transform.position = new Vector3(UnityEngine.Random.Range(-10, 11), 0, UnityEngine.Random.Range(-9, 10));
    //        IsAlive = true;
    //        agent.ResetPath();
    //    }
    //    else
    //    {
    //        Instantiate(botPrefab, randomPos, Quaternion.identity);
    //        OnInit();
    //    }
    //}

    

    //public void ReturnToPool()
    //{
    //    agent.ResetPath();
    //    stack.Push(this);
    //    this.gameObject.SetActive(false);
    //    this.MainTarget = null;
    //    this.otherTarget.Clear();
    //    if (BotManager.Instance.botIndex <= 7)
    //    {
    //        SpawnBot();
    //    }
    //}

    public IEnumerator OnDeath()
    {
        InRange = false;
        this.gameObject.layer = 0;
        IsAlive = false;
        agent.ResetPath();
        isPlayAble = false;
        IsDead = true;
        IsAttack = false;
        IsIdle = false;
        SetBoolAnimation();
        yield return new WaitForSeconds(2f);
        IsDead = false;
        //agent.ResetPath();
        BotManager.Instance.ReturnToPool(this);
    }

    public void DelayDead()
    {
        StartCoroutine(OnDeath());
    }

    public void SetDirection()
    {
        newPos = RandomNavSphere(transform.position, wanderRadius, -1);
        newPos.y = transform.position.y;
        moveDirection = (newPos - transform.position).normalized;
        agent.SetDestination(newPos);
    }

    public Vector3 RandomNavSphere(Vector3 origin, float dist, int layerMask)
    {
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layerMask);

        return navHit.position;
    }

    public bool HasTarget => MainTarget != null;

}
