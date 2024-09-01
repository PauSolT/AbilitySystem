using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<ShieldComponent>().Init(20, 5);
        gameObject.AddComponent<ShieldComponent>().Init(20, -1);

    }

    // Update is called once per frame
    void Update()
    {
    }
}
