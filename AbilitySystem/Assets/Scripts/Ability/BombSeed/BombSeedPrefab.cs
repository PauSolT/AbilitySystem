using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class BombSeedPrefab : MonoBehaviour
{
    Element element;
    GameObject user;
    Vector3 target;
    Vector3 direction;
    float damage;
    float heal;
    float speed;
    HashSet<HealthComponent> entities = new HashSet<HealthComponent>();

    bool attached = false;

    public void Init(GameObject user, Vector3 target, float damage, float heal, float speed, float duration, Element element)
    {
        this.user = user;
        this.target = target;
        this.damage = damage;
        this.heal = heal;
        this.speed = speed;
        this.element = element;
        StartCoroutine(Unload(duration));
        direction = (target - transform.position).normalized;
    }

    public void Update()
    {
        if (target != null && !attached)
        {
            transform.position += speed * Time.deltaTime * direction;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out HealthComponent health) && !other.CompareTag(user.tag))
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
        foreach (HealthComponent enemy in entities)
        {
            enemy.TakeDamage(damage, element);
        }
        if (transform.GetChild(0).GetComponent<BombSeedPrefabDetectHealing>().userToHeal)
        {
            user.GetComponent<HealthComponent>().Heal(heal);
        }

        if (gameObject)
        {
            Destroy(gameObject);
        }
    }
}
