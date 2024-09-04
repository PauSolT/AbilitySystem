using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlashPrefab : MonoBehaviour
{
    Element element;
    GameObject user;
    float damage;
    float speed;
    PlayerMovement movement;

    Vector3 direction;
    public void Init(GameObject user, float damage, float speed, float duration, Element element)
    {
        this.user = user;
        this.damage = damage;
        this.speed = speed;
        this.element = element;
        movement = user.GetComponent<PlayerMovement>();
        StartCoroutine(Unload(duration));
        direction = movement.facingRight ? Vector3.right : Vector3.left;
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
