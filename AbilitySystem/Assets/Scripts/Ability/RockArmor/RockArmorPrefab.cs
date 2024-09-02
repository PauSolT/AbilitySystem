using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockArmorPrefab : MonoBehaviour
{

    HealthComponent playerHealth;
    Element element;
    float damage;
    float damageReduction;
    float interval;

    [SerializeField] List<HealthComponent> entities = new List<HealthComponent>();

    bool waiting = false;

    public void Init(float damage, float duration, float interval, Element element, HealthComponent playerHealth, float damageReduction)
    {
        this.damage = damage;
        this.interval = interval;
        this.element = element;
        this.playerHealth = playerHealth;
        this.damageReduction = damageReduction;
        StartCoroutine(Unload(duration));
    }

    private IEnumerator Unload(float duration)
    {
        yield return new WaitForSeconds(duration);
        playerHealth.DamageReduction -= damageReduction;
        if (gameObject)
        {
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        if (entities.Count > 0 && !waiting)
        {
            StartCoroutine(ActiveAbility());
        }
    }

    private IEnumerator ActiveAbility()
    {
        waiting = true;

        foreach (HealthComponent entity in entities)
        {
            entity.TakeDamage(damage, element);
        }

        yield return new WaitForSeconds(interval);
        waiting = false;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            entities.Add(other.GetComponent<HealthComponent>());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            entities.Remove(other.GetComponent<HealthComponent>());
        }
    }
}
