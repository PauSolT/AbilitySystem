using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePrefab : MonoBehaviour
{
    GameObject user;
    GameObject target;
    float damage;
    [SerializeField]
    float currentSpeed;
    float speedMax;
    float speedMin;
    float timeToChange;
    float changePerSecond;
    float slowDelay;


    bool slowing = false;

    List<HealthComponent> alreadyDamaged = new List<HealthComponent>();

    Vector3 direction;
    public void Init(GameObject user,
    GameObject target,
    float damage,
    float speedMax,
    float speedMin,
    float timeToChange,
    float slowDelay,
    float duration)
    {
        this.user = user;
        this.target = target;
        this.damage = damage;
        this.speedMax = speedMax;
        this.speedMin = speedMin;
        this.timeToChange = timeToChange;
        this.slowDelay = slowDelay;
        direction = (target.transform.position - transform.position).normalized;
        changePerSecond = (speedMin - speedMax) / timeToChange;
        currentSpeed = speedMax;
        StartCoroutine(Unload(duration));
    }

    public void Update()
    {
        if (slowing)
        {
            currentSpeed = Mathf.Clamp(currentSpeed + changePerSecond * Time.deltaTime, speedMin, speedMax);
        }

        if (target != null)
        {
            transform.position += currentSpeed * Time.deltaTime * direction;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out HealthComponent health) && !other.CompareTag(user.tag))
        {
            if (!alreadyDamaged.Contains(health))
            {
                health.TakeDamage(damage);
                alreadyDamaged.Add(health);
            }
        }
    }

    private IEnumerator Unload(float duration)
    {
        yield return new WaitForSeconds(slowDelay);
        slowing = true;
        yield return new WaitForSeconds(duration - slowDelay);
        if (gameObject)
        {
            Destroy(gameObject);
        }
    }
}
