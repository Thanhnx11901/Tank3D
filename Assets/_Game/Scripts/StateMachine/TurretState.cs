using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretState : IState<EnemyTank>
{
    public void OnEnter(EnemyTank t)
    {
        t.canFire = true;
    }

    public void OnExecute(EnemyTank t)
    {
        t.TurretRotation();
    }
    public void OnExit(EnemyTank t)
    {

    }

}
