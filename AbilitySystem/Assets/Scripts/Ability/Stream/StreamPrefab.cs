using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StreamPrefab : MonoBehaviour
{
    GameObject user;
    PlayerMovement player;
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
        if (player)
        {
            if (player.facingRight)
            {
                transform.position = player.transform.position + new Vector3(transform.localScale.z / 2 + 2f, 0.5f, 0);
            }
            else
            {
                transform.position = player.transform.position + new Vector3(-(transform.localScale.z / 2 + 2f), 0.5f, 0);
            }
        }

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

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out HealthComponent health) && entities.Contains(health))
        {
            entities.Remove(health);
        }
    }
}
