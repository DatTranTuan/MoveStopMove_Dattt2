using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    public void OnEnter(Bot bot)
    {
        bot.agent.ResetPath();
    }

    public void OnExecute(Bot bot)
    {
        bot.Attack();
        if (!bot.HasTarget)
        {
            bot.ChangeState(new IdleState());
        }
    }

    public void OnExit(Bot bot)
    {

    }
}
