using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleShiledPrefab : MonoBehaviour
{

    float damage;
    [SerializeField] List<HealthComponent> entities = new List<HealthComponent>();


    public void Init(float damage, float duration)
    {
        this.damage = damage;
        StartCoroutine(Unload(duration));
    }

    private IEnumerator Unload(float duration)
    {
        yield return new WaitForSeconds(duration);
        foreach (HealthComponent enemy in entities)
        {
            enemy.TakeDamage(damage);
        }
        if (gameObject)
        {
            Destroy(gameObject);
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
