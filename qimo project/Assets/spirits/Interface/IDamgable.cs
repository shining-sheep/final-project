using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamgable
{
    public bool TakeDamage(float damage,float elementalDamage, Transform damageDealer);
}
