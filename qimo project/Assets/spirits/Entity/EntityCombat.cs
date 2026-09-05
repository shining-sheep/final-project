using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityCombat : MonoBehaviour
{
    public float damage = 10;

    [Header("Target detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1;
    [SerializeField] private LayerMask wahtIsTarget;

    public void PerformAttack()
    {
        

        foreach(var target in GetDetectedColliders())
        {
            IDamgable damgable = target.GetComponent<IDamgable>();
            damgable?.TakeDamage(damage, transform);
        }
    }

    protected Collider2D[] GetDetectedColliders()
    {
       return Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, wahtIsTarget);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }


}
