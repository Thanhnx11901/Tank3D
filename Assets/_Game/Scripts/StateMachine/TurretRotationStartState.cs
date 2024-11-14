using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotationStartState : IState<EnemyTank>
{
    public void OnEnter(EnemyTank t)
    {
        t.CanFire = true;
    }

    public void OnExecute(EnemyTank t)
    {
        t.TurretRotationStart();
    }
    public void OnExit(EnemyTank t)
    {

    }

}
