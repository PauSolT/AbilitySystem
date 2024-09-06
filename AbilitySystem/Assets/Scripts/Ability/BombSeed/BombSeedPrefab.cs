using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSeedPrefab : MonoBehaviour
{
    Element element;
    GameObject user;
    Vector3 direction;
    float damage;
    float heal;
    float speed;
    HashSet<HealthComponent> entities = new HashSet<HealthComponent>();
    bool attached = false;

    public void Init(GameObject user, Vector3 target, float damage, float heal, float speed, float duration, Element element)
    {
        this.user = user;
        this.damage = damage;
        this.heal = heal;
        this.speed = speed;
        this.element = element;
        direction = (target - transform.position).normalized;
        StartCoroutine(Unload(duration));
    }

    public void Update()
    {
        if (!attached)
        {
            transform.position += speed * Time.deltaTime * direction;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out HealthComponent health))
        {
            entities.Add(health);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out HealthComponent health) && entities.Contains(health))
        {
            entities.Remove(health);
            //check here and not on collision because
            //it takes too long on collision
            if (attached)
            {
                entities.Add(health);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        attached = true;
        gameObject.transform.SetParent(other.transform);
    }

    private IEnumerator Unload(float duration)
    {
        yield return new WaitForSeconds(duration);
        OnUnload();

        if (gameObject)
        {
            Destroy(gameObject);
        }
    }

    void OnUnload()
    {
        foreach (HealthComponent enemy in entities)
        {
            enemy.TakeDamage(damage, element);
        }
        if (transform.GetChild(0).GetComponent<BombSeedPrefabDetectHealing>().userToHeal)
        {
            user.GetComponent<HealthComponent>().Heal(heal);
        }
    }

}
