using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleShiledPrefab : MonoBehaviour
{

    Element element;
    float damage;
    [SerializeField] List<HealthComponent> entities = new List<HealthComponent>();


    public void Init(float damage, float duration, Element element)
    {
        this.damage = damage;
        this.element = element;
        StartCoroutine(Unload(duration));
        ShieldComponent.OnShieldDestroy += DestroyShield;
    }

    private IEnumerator Unload(float duration)
    {
        yield return new WaitForSeconds(duration);
        DestroyShield();
    }

    private void DestroyShield()
    {
        Explode();
        ShieldComponent.OnShieldDestroy -= DestroyShield;
        if (gameObject)
        {
            Destroy(gameObject);
        }
    }

    private void Explode()
    {
        foreach (HealthComponent enemy in entities)
        {
            enemy.TakeDamage(damage, element);
        }
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
