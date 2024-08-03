using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballPrefab : MonoBehaviour
{
    public GameObject target;
    public float damage;
    public float speed;

    public void Init(GameObject target, float damage, float speed)
    {
        this.target = target;
        this.damage = damage;
        this.speed = speed;
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
        Debug.Log("" + other.gameObject.name);
    }
}
