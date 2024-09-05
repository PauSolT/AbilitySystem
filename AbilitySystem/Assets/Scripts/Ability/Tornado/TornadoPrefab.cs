using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class TornadoPrefab : MonoBehaviour
{
    Element element;
    GameObject user;
    float damage;
    float duration;
    float speed;
    float penaltySpeed;
    float sizeMultiplier;
    float damageMultiplier;
    float interval;
    int combined = 1;
    Coroutine unload;

    List<HealthComponent> entities = new List<HealthComponent>();
    Rigidbody2D rb;
    bool waiting = false;

    Transform parent;


    public void Init(GameObject user, Vector3 target, float damage, float speed, float penaltySpeed, float sizeMultiplier, float damageMultiplier, float duration, float interval, Element element)
    {
        this.user = user;
        this.damage = damage;
        this.duration = duration;
        this.speed = speed;
        this.penaltySpeed = penaltySpeed;
        this.sizeMultiplier = sizeMultiplier;
        this.damageMultiplier = damageMultiplier;
        this.element = element;
        this.interval = interval;
        unload = StartCoroutine(Unload(duration));
        rb = GetComponentInParent<Rigidbody2D>();
        if ((target - user.transform.position).normalized.x < 0)
        {
            this.speed = -speed;
        }

        parent = transform.parent;
    }
    public void FixedUpdate()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
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
        if (other.TryGetComponent(out HealthComponent health) && !other.CompareTag(user.tag))
        {
            entities.Add(health);
        }

        if (other.TryGetComponent(out TornadoPrefab tornado))
        {
            if (tornado != this && combined >= tornado.combined)
            {
                Destroy(tornado.parent.gameObject);
                CombineTornado();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out HealthComponent health) && entities.Contains(health))
        {
            entities.Remove(health);
        }
    }

    void CombineTornado()
    {
        if (combined < 3)
        {
            parent.localScale *= sizeMultiplier;
            damage *= damageMultiplier;
            speed *= penaltySpeed;
        }
        StopCoroutine(unload);
        unload = StartCoroutine(Unload(duration));
        combined++;
    }


    private IEnumerator Unload(float duration)
    {
        yield return new WaitForSeconds(duration);
        if (gameObject)
        {
            Destroy(parent.gameObject);
        }
    }
}
