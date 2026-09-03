using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    private Entity_VFX entityVfx;

    [SerializeField] protected float maxHp = 100;
    [SerializeField] protected bool isDead;

    protected virtual void Awake()
    {
        entityVfx = GetComponent<Entity_VFX>();
    }

    public virtual void TakeDamage(float damage,Transform damageDealer)
    {
        if (isDead)
            return;

        entityVfx?.PlayOnDamegeVfx();
        ReduceHp(damage);
    }

    protected void ReduceHp(float damage)
    {
        maxHp -= damage;

        if (maxHp < 0)
            Die();
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Entity dead!");
    }
}
