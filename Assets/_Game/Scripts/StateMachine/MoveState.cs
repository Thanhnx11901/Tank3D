using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IState<EnemyTank>
{
    public void OnEnter(EnemyTank t)
    {
       Vector3 pointRandom =  t.GetRandomPosition();
       t.agent.SetDestination(pointRandom);
    }

    public void OnExecute(EnemyTank t)
    {
        if (t.agent.pathPending == false && t.agent.remainingDistance <= 0.1f)
        {
            t.ChangeState(new AttackState());
        }

    }

    public void OnExit(EnemyTank t)
    {
        
    }

}
