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
            EntityHealth targetHealth = target.GetComponent<EntityHealth>();
            targetHealth?.TakeDamage(damage,transform);
        }
    }

    private Collider2D[] GetDetectedColliders()
    {
       return Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, wahtIsTarget);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }


}
