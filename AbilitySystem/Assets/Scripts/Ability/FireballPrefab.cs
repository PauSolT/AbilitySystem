using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballPrefab : MonoBehaviour
{
    public GameObject user;
    public float damage;

    public void Init(GameObject user, float damage)
    {
        this.user = user;
        this.damage = damage;
    }
}
