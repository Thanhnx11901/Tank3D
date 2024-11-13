using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<EnemyTank>
{
    public void OnEnter(EnemyTank t)
    {
        t.ChangeState(new MoveState());
    }

    public void OnExecute(EnemyTank t)
    {


    }

    public void OnExit(EnemyTank t)
    {

    }

}
