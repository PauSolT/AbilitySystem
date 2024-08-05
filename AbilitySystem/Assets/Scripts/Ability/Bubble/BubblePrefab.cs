using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePrefab : MonoBehaviour
{
    GameObject user;
    GameObject target;
    float damage;
    float speed;

    public void Init(GameObject user, GameObject target, float damage, float speed, float duration)
    {
        this.user = user;
        this.target = target;
        this.damage = damage;
        this.speed = speed;
        StartCoroutine(Unload(duration));
    }

    public void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            transform.position += speed * Time.deltaTime * direction;
        }
    }

    private void OnTriggerEnter(Collider other)
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
