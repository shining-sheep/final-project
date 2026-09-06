using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth :EntityHealth
{
    private Enemy enemy => GetComponent<Enemy>();

    public override bool TakeDamage(float damage , float elementalDamage, Transform damageDealer)
    {
       bool  wasHit =  base.TakeDamage(damage,  elementalDamage, damageDealer);

        if (wasHit == false)
            return false;

        if (damageDealer.GetComponent<Player>() != null)
            enemy.TryEnterBattleState(damageDealer);

        return true;

    }
    
}
