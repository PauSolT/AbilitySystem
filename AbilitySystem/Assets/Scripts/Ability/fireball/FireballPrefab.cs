using System.Collections;
using UnityEngine;

public class FireballPrefab : MonoBehaviour
{
    Element element;
    GameObject user;
    Vector3 direction;
    float damage;
    float speed;

    public void Init(GameObject user, Vector3 target, float damage, float speed, float duration, Element element)
    {
        this.user = user;
        this.damage = damage;
        this.speed = speed;
        this.element = element;
        direction = (target - transform.position).normalized;
        StartCoroutine(Unload(duration));
    }

    public void Update()
    {
        transform.position += speed * Time.deltaTime * direction;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out HealthComponent health) && !other.CompareTag(user.tag))
        {
            health.TakeDamage(damage, element);
            Destroy(gameObject);
        }
    }

    private IEnumerator Unload(float duration)
    {
        yield return new WaitForSeconds(duration);
        if (gameObject)
        {
            Destroy(gameObject);
        }
    }
}
