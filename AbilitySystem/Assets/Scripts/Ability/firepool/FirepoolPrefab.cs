using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirepoolPrefab : MonoBehaviour
{
    Element element;
    GameObject user;
    GameObject parent;
    Vector3 target;
    float damage;
    float interval;

    List<HealthComponent> entities = new List<HealthComponent>();

    bool waiting = false;


    public void Init(GameObject user, Vector3 target, float damage, float interval, float duration, Element element)
    {
        this.user = user;
        this.target = target;
        this.damage = damage;
        this.interval = interval;
        this.element = element;
        StartCoroutine(Unload(duration));

        parent = gameObject.transform.parent.gameObject;
    }

    private IEnumerator Unload(float duration)
    {
        yield return new WaitForSeconds(duration);
        if (parent)
        {
            Destroy(parent);
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
            if (!user.CompareTag(entity.gameObject.tag))
            {
                entity.TakeDamage(damage, element);
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

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out HealthComponent health) && entities.Contains(health))
        {
            entities.Remove(health);
        }
    }
}
