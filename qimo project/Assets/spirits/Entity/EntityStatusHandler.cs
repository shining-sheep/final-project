using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStatusHandler : MonoBehaviour
{
    private Entity entity;
    private Entity_VFX entityVfx;
    private EntityStats entityStats;
    private EntityHealth entityHealth;
    private ElementType currentEffect = ElementType.None;

    [Header("Electrify effect details")]
    [SerializeField] private GameObject lightningStirkeVfx;
    [SerializeField] private float currentCharge;
    [SerializeField] private float maximumCharge = 1;
    private Coroutine electrifyCo;

    private void Awake()
    {
        entityStats = GetComponent<EntityStats>();
        entityHealth = GetComponent<EntityHealth>();
        entity = GetComponent<Entity>();
        entityVfx = GetComponent<Entity_VFX>();
    }

    public void ApplyElectrifyEffect(float duration,float damage, float charge)
    {
        float lightningResistance = entityStats.GetElementalResistance(ElementType.Lightning);
        float finalCharge = charge * (1 - lightningResistance);
        currentCharge = currentCharge + charge;

        if(currentCharge >= maximumCharge)
        {
            DoLightningStrike(damage);
            StopElectrifyEffect();
            return;
        }
        if (electrifyCo != null)
            StopCoroutine(electrifyCo);

        electrifyCo = StartCoroutine(ElectrifyEffectCo(duration));
    }

    private void StopElectrifyEffect()
    {
        currentEffect = ElementType.None;
        currentCharge = 0;
        entityVfx.StopAllVfx();
    }

    private IEnumerator ElectrifyEffectCo(float duration)
    {
        currentEffect = ElementType.Lightning;
        entityVfx.PlayOnStatusVfx(duration, ElementType.Lightning);

        yield return new WaitForSeconds(duration);
        StopElectrifyEffect();
    }

    private void DoLightningStrike(float damage)
    {
        Instantiate(lightningStirkeVfx, transform.position, Quaternion.identity);
        entityHealth.ReduceHealth(damage);
    }

    public void ApplyBurnEffect(float duration,float fireDamage)
    {
        float fireResistance = entityStats.GetElementalResistance(ElementType.Fire);
        float  finalDamage = fireDamage * (1 - fireResistance);


        StartCoroutine(BurnEffectCo(duration, finalDamage));
    }

    private IEnumerator BurnEffectCo(float duration, float totalDamage)
    {
        currentEffect = ElementType.Fire;
        entityVfx.PlayOnStatusVfx(duration, ElementType.Fire);

        int ticksPersecond = 2;
        int tickCount = Mathf.RoundToInt(ticksPersecond * duration);

        float damagePerTick = totalDamage / tickCount;
        float tickInterval = 1f / ticksPersecond;

        for(int i = 0; i < tickCount; i++)
        {
            entityHealth.ReduceHealth(damagePerTick);
            yield return new WaitForSeconds(tickInterval);
        }
        currentEffect = ElementType.None;
    }

    public void ApplyChillEffect(float duration, float slowMultiplier)
    {
        float iceResistance = entityStats.GetElementalResistance(ElementType.Ice);
        float finalDuration = duration * (1 - iceResistance);

        StartCoroutine(ChilledEffectCo(finalDuration,slowMultiplier));

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
        if (element == ElementType.Lightning && currentEffect == ElementType.Lightning)
            return true;

        return currentEffect == ElementType.None;
    }
}
