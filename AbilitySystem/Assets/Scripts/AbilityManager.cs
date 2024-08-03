using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public List<Ability> abilities;
    private Dictionary<Ability, float> cooldownTimers = new Dictionary<Ability, float>();

    // Update is called once per frame
    void Update()
    {
        ManageCooldowns();

        if (Input.GetKeyDown(KeyCode.Q) && !cooldownTimers.ContainsKey(abilities[0]))
        {
            UseAbility(abilities[0]);
        }
    }

    void ManageCooldowns()
    {
        foreach (Ability ability in abilities)
        {
            if (cooldownTimers.ContainsKey(ability))
            {
                cooldownTimers[ability] -= Time.deltaTime;
                if (cooldownTimers[ability] <= 0)
                {
                    cooldownTimers.Remove(ability);
                }
            }
        }
    }

    public void UseAbility(Ability ability)
    {
        if (!cooldownTimers.ContainsKey(ability))
        {
            StartCoroutine(ability.AbilityUse(gameObject.transform.gameObject));
            cooldownTimers[ability] = ability.cooldown;
            StartCoroutine(HandleAbilityDuration(ability));
            Debug.Log(ability.abilityName + " used!");

        }


    }

    private IEnumerator HandleAbilityDuration(Ability ability)
    {
        yield return new WaitForSeconds(ability.duration);
        ability.Unload();
    }
}
