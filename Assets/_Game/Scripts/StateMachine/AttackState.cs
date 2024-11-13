using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<EnemyTank>
{
    public void OnEnter(EnemyTank t)
    {

    }

    public void OnExecute(EnemyTank t)
    {
        t.TurretRotationPlayer();
    }
    public void OnExit(EnemyTank t)
    {

    }

}
