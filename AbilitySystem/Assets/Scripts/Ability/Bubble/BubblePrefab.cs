using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePrefab : MonoBehaviour
{
    GameObject user;
    Vector3 target;
    Vector3 direction;
    float damage;
    float speed;

    public void Init(GameObject user, Vector3 target, float damage, float speed, float duration)
    {
        this.user = user;
        this.target = target;
        this.damage = damage;
        this.speed = speed;
        StartCoroutine(Unload(duration));
        direction = (target - transform.position).normalized;
    }

    public void Update()
    {
        if (target != null)
        {
            transform.position += speed * Time.deltaTime * direction;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out HealthComponent health) && !other.CompareTag(user.tag))
        {
            health.TakeDamage(damage);
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
