using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private float timer = 2f;

    public void OnEnter(Bot bot)
    {
        bot.IsIdle = true;
        bot.SetBoolAnimation();
    }

    public void OnExecute(Bot bot)
    {
        timer -= Time.deltaTime;

        if (bot.HasTarget)
        {
            bot.ChangeState(new AttackState());
        }

        if (timer <= 0f)
        {
            bot.ChangeState(new MoveState());
        }
    }

    public void OnExit(Bot bot)
    {

    }
}
