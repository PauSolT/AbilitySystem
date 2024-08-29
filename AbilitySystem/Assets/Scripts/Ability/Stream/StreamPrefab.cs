using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamPrefab : MonoBehaviour
{
    GameObject user;
    float damage;
    float interval;

    List<HealthComponent> entities = new List<HealthComponent>();

    bool waiting = false;

    public void Init(GameObject user, float damage, float interval, float duration)
    {
        this.user = user;
        this.damage = damage;
        this.interval = interval;
        StartCoroutine(Unload(duration));
    }

    private IEnumerator Unload(float duration)
    {
        yield return new WaitForSeconds(duration);
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

        transform.Rotate(0, 0, 30 * Time.deltaTime);
    }

    private IEnumerator ActiveAbility()
    {
        waiting = true;

        foreach (HealthComponent entity in entities)
        {
            if (user.CompareTag(entity.gameObject.tag))
            {
                entity.Heal(damage);
            }
            else if (!user.CompareTag(entity.gameObject.tag))
            {
                entity.TakeDamage(damage);
            }
        }

        yield return new WaitForSeconds(interval);

        waiting = false;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out HealthComponent health) && !entities.Contains(health))
        {
            entities.Add(health);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out HealthComponent health) && entities.Contains(health))
        {
            entities.Remove(health);
        }
    }
}
