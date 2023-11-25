using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveState : IState
{
    private float wanderTimer = 5f;
    private float timer = 0f;


    public void OnEnter(Bot bot)
    {
        bot.IsIdle = false;
        bot.SetBoolAnimation();
        bot.SetDirection();
    }

    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;

        if (bot.HasTarget)
        {
            bot.ChangeState(new IdleState());
            return;
        }

        if (bot.isTarget)
        {
            //bot.IsIdle = false;
            bot.ChangeState(new IdleState());
        }

        if(timer >= wanderTimer && !bot.IsAttack)
        {
            bot.SetDirection();
            timer = 0f;
            wanderTimer = Random.Range(3f, 5f);
        }
    }

    public void OnExit(Bot bot)
    {

    }

}
