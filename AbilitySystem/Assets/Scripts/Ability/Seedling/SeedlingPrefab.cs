using System.Collections;
using UnityEngine;

public class SeedlingPrefab : MonoBehaviour
{
    Element element;
    GameObject user;
    Vector3 target;
    float damage;

    //Parabola
    float flightDuration;
    private Vector3 startPosition;

    public void Init(GameObject user, Vector3 target, float damage, float duration, float flightDuration, Element element)
    {
        this.user = user;
        this.target = target;
        this.damage = damage;
        this.element = element;
        this.flightDuration = flightDuration;
        StartCoroutine(Unload(duration));

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;

        Vector2 initialVelocity = CalculateInitialVelocity();
        rb.velocity = initialVelocity;
    }


    Vector2 CalculateInitialVelocity()
    {
        // Calculate the distance between the start and target positions
        Vector2 distance = target - startPosition;

        // Calculate the required initial velocity components
        float vx = distance.x / flightDuration;
        float vy = (distance.y - 0.5f * Physics2D.gravity.y * flightDuration * flightDuration) / flightDuration;

        return new Vector2(vx, vy);
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
