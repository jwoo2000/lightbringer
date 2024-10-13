using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : EnemyBehaviour
{
    [SerializeField]
    public GameManager gameManager;

    protected override void OnDeath()
    {
        base.OnDeath();
        if (behaviourType == EnemyType.Boss)
        {
            gameManager.playerWin();
        }
    }
}
