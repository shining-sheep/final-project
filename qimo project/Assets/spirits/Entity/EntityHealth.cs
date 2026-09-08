using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityHealth : MonoBehaviour,IDamgable
{
    private Slider healthBar;
    private Entity_VFX entityVfx;
    private Entity entity;
    private EntityStats entityStats;

    [SerializeField] protected float currentHealth;
  
    [SerializeField] protected bool isDead;

    [Header("Health regen")]
    [SerializeField] private float regenInterval = 1;//回血间隔
    [SerializeField] private bool canRegenerateHealth = true;//是否可以回血


    [Header("On Damage Knockback")]
    [SerializeField] private Vector2 knockbackPower = new Vector2(1.5f,2.5f);
    [SerializeField] private Vector2 heavyKnockbackPower = new Vector2(7, 7);
    [SerializeField] private float konckbackDuration = 0.2f;
    [SerializeField] private float heavyKnockbackDuration = 0.5f;

    [Header("On Heavy Damage")]
    [SerializeField] private float heavyDamageThreshold = 0.3f;

    protected virtual void Awake()
    {
        entityVfx = GetComponent<Entity_VFX>();
        entity = GetComponent<Entity>();
        entityStats = GetComponent<EntityStats>();
        healthBar = GetComponentInChildren<Slider>();

        currentHealth = entityStats.GetMaxHealth();
        UpdateHealthBar();

        InvokeRepeating(nameof(RegenrateHealth), 0, regenInterval);
    }

    public virtual bool TakeDamage(float damage,float elementalDamage, ElementType element, Transform damageDealer)
    {
        if (isDead)
            return false;

        if (AttackEvaded())
        {
            Debug.Log($"{gameObject.name})躲避之");
            return false;
        }

        EntityStats attackerStats = damageDealer.GetComponent<EntityStats>();
        float armorReduction = attackerStats != null ? attackerStats.GetArmorReduction() : 0;

        float mitigation = entityStats.GetArmorMitigation(armorReduction);
        float physicalDamageTaken = damage * (1 - mitigation);

        float resistance = entityStats.GetElementalResistance(element);
        float elementalDamageTaken = elementalDamage * (1 - resistance);
        TakeKnockback(damageDealer, physicalDamageTaken);
        ReduceHealth(physicalDamageTaken + elementalDamageTaken);

        return true;
    }

    

    //攻击闪避
    private bool AttackEvaded() => Random.Range(0, 100) < entityStats.GetEvasion();

    private void RegenrateHealth()
    {
        if (canRegenerateHealth == false)
            return;

        float regenAmount = entityStats.resources.healthRegen.GetValue();
        IncreaseHealth(regenAmount);
    }

    public void IncreaseHealth(float healAmount)
    {
        if (isDead)
            return;

        float newHealth = currentHealth + healAmount;
        float maxHealth = entityStats.GetMaxHealth();
        currentHealth = Mathf.Min(newHealth, maxHealth);
        UpdateHealthBar();
        

     }
    
    public  void ReduceHealth(float damage)
    {
        entityVfx?.PlayOnDamegeVfx();
        currentHealth -= damage;
        UpdateHealthBar();

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        isDead = true;
        entity?.EntityDeath();
    }

    private void UpdateHealthBar()
    {
        if (healthBar == null)
            return;

        healthBar.value = currentHealth / entityStats.GetMaxHealth();
    }

    private void TakeKnockback(Transform damageDealer, float finalDamage)
    {
        Vector2 knockback = CalculateKnockback(finalDamage, damageDealer);
        float duration = CalculateDuration(finalDamage);

        entity?.ReciveKnockback(knockback, duration);
    }
    private Vector2 CalculateKnockback(float damage ,Transform damageDealer)
    {
        int direction = transform.position.x > damageDealer.position.x ? 1 : -1;

        Vector2 knockback = IsHeavyDamage(damage) ? heavyKnockbackPower : knockbackPower;

        knockback.x = knockback.x * direction;

        return knockback;
    }

    private float CalculateDuration(float damage) => IsHeavyDamage(damage) ? heavyKnockbackDuration : konckbackDuration;

    private bool IsHeavyDamage(float damage) => damage / entityStats.GetMaxHealth() > heavyDamageThreshold;
}
