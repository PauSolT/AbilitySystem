using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamPrefab : MonoBehaviour
{
    Element element;
    GameObject user;
    PlayerMovement player;
    float damage;
    float interval;

    List<HealthComponent> entities = new List<HealthComponent>();

    bool waiting = false;

    public void Init(GameObject user, float damage, float interval, float duration, Element element)
    {
        this.user = user;
        this.damage = damage;
        this.interval = interval;
        this.element = element;
        StartCoroutine(Unload(duration));
        user.TryGetComponent(out player);
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
        transform.position = player.transform.position +
        new Vector3(SetStreamX(), 0.5f, 0);

        if (entities.Count > 0 && !waiting)
        {
            StartCoroutine(ActiveAbility());
        }
    }

    float SetStreamX()
    {
        return player.facingRight ? transform.localScale.z / 2 + 2f : -(transform.localScale.z / 2 + 2f);
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
