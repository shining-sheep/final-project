using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth :EntityHealth
{
    private Enemy enemy => GetComponent<Enemy>();

    public override void TakeDamage(float damage ,Transform damageDealer)
    {
        if (damageDealer.GetComponent<Player>() != null)
            enemy.TryEnterBattleState(damageDealer);

        base.TakeDamage(damage,damageDealer);
    }
    
}
