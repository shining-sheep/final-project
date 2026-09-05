using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityCombat : MonoBehaviour
{
    private Entity_VFX vfx;
    private EntityStats stats;
    


    [Header("Target detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1;
    [SerializeField] private LayerMask wahtIsTarget;

    private void Awake()
    {
        vfx = GetComponent<Entity_VFX>();
        stats = GetComponent<EntityStats>();
    }

    public void PerformAttack()
    {
        

        foreach(var target in GetDetectedColliders())
        {
            IDamgable damgable = target.GetComponent<IDamgable>();

            if (damgable == null)
                continue;

            
            float damage = stats.GetPhyiscalDamge(out bool isCrit);
            bool targetGoHit = damgable.TakeDamage(damage, transform);

            if(targetGoHit)
              vfx.CreateOnHitVFX(target.transform,isCrit);
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
