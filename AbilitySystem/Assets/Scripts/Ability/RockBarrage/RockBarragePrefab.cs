using System.Collections;
using UnityEngine;

public class RockBarragePrefab : MonoBehaviour
{
    Element element;
    GameObject user;
    Vector3 target;
    Vector3 direction;
    float damage;
    float speed;
    float angle;
    bool readyToLaunch = false;

    GameObject enemy;


    public void Init(GameObject user, float damage, float speed, float duration, Element element, GameObject enemy)
    {
        this.enemy = enemy;
        this.user = user;
        this.damage = damage;
        this.speed = speed;
        this.element = element;
        StartCoroutine(Unload(duration));
    }

    public void Update()
    {
        if (enemy != null)
        {
            transform.rotation = SetRockAngle();
            if (readyToLaunch)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            }
        }
    }

    Quaternion SetRockAngle()
    {
        target = enemy.transform.position;
        direction = target - user.transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    public void ReadyToLaunch()
    {
        readyToLaunch = true;
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
