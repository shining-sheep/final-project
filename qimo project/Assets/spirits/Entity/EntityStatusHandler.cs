using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStatusHandler : MonoBehaviour
{
    private Entity entity;
    private Entity_VFX entityVfx;
    private EntityStats stats;
    private ElementType currentEffect = ElementType.None;

    private void Awake()
    {
        stats = GetComponent<EntityStats>();
        entity = GetComponent<Entity>();
        entityVfx = GetComponent<Entity_VFX>();
    }

    public void ApplyChilledEffect(float duration, float slowMultiplier)
    {
        float iceResistance = stats.GetElementalResistance(ElementType.Ice);
        float reducedDuration = duration * (1 - iceResistance);

        StartCoroutine(ChilledEffectCo(reducedDuration,slowMultiplier));

    }

    private IEnumerator ChilledEffectCo(float duration,float slowMultiplier)
    {
        entity.SlowDownEntity(duration, slowMultiplier);
        currentEffect = ElementType.Ice;
        entityVfx.PlayOnStatusVfx(duration, ElementType.Ice);

        yield return new WaitForSeconds(duration);

        currentEffect = ElementType.None;
    }

    public bool CanBeApplied(ElementType element)
    {
        return currentEffect == ElementType.None;
    }
}
