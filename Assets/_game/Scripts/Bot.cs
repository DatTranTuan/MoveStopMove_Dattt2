using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Bot : Character
{
    private WeaponType currentWeaponType;

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Bot botPrefab;

    public static Stack<Bot> stack = new Stack<Bot>();
    public bool isTarget => Vector3.Distance(transform.position, newPos) < 0.1f;

    private Vector3 newPos;
    private Vector3 randomPos;

    private float wanderRadius = 4f;
    private IState currentState;

    private void Start()
    {
        OnInit();
        SpawnWeapon();
    }

    public void OnInit()
    {
        if (weaponData == null)
        {
            currentWeaponType = WeaponType.Axe;
            weaponData = DataManager.Instance.GetWeaponData(currentWeaponType);
        }
    }

    protected void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }

    private void OnEnable()
    {
        randomPos = new Vector3(UnityEngine.Random.Range(-10, 11), 0, UnityEngine.Random.Range(-9, 10));
        IsAttack = false;
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

    private void SpawnBot()
    {
        if (stack.Count > 0)
        {
            Bot spawnBot;
            spawnBot = stack.Pop();
            spawnBot.gameObject.SetActive(true);
            spawnBot.gameObject.transform.position = new Vector3(UnityEngine.Random.Range(-10, 11), 0, UnityEngine.Random.Range(-9, 10));
            agent.ResetPath();
        }
        else
        {
            Instantiate(botPrefab, randomPos, Quaternion.identity);
            OnInit();
        }
    }

    public void ReturnToPool()
    {
        stack.Push(this);
        this.gameObject.SetActive(false);
        this.mainTarget = null;
        this.otherTarget.Clear();
        SpawnBot();
    }

    public void SetDirection()
    {
        newPos = RandomNavSphere(transform.position, wanderRadius, -1);
        newPos.y = transform.position.y;
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

    public bool HasTarget => mainTarget!= null;
}
