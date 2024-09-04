using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/RockBarrage")]
public class RockBarrageAbility : Ability
{

    [NonSerialized] List<GameObject> rocks = new List<GameObject>();
    public int numebrOfRocks;
    public float speed;
    public float rockInterval;

    GameObject enemy;

    public override void Init()
    {
        base.Init();
    }
    public override void AbilityUse(GameObject user, Vector3 target)
    {
        element.PassiveOnAbilitiy();
        rocks.Clear();
        GlobalCoroutines.Instance.StartCoroutine(ReadyRocks(user));

    }

    IEnumerator ReadyRocks(GameObject user)
    {
        enemy = PlayerElements.Enemy;
        for (int i = 0; i < numebrOfRocks; i++)
        {
            Vector3 spawnPosition = user.transform.position + new Vector3(-2f + i * 1f, -Mathf.Abs(i - 2) + 2, 0);
            GameObject rock = Instantiate(prefab, spawnPosition, Quaternion.identity, user.transform);
            rock.GetComponent<RockBarragePrefab>().Init(user, damage, speed, duration, element, enemy);
            rocks.Add(rock);
            yield return new WaitForSeconds(rockInterval);
        }

        foreach (GameObject rock in rocks)
        {
            rock.GetComponent<RockBarragePrefab>().ReadyToLaunch();
        }
    }

    public override void Unload()
    {
        if (rocks.Count > 0)
        {
            foreach (GameObject rock in rocks)
            {
                Destroy(rock);
            }
        }
    }
}
