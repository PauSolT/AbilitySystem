using UnityEngine;

[CreateAssetMenu(menuName = "Element/Grass")]
public class GrassElement : Element
{
    private HealthComponent userHealth;
    [SerializeField] private float healingPower = 5f;

    public override void Init()
    {
        userHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthComponent>();
    }
    public override void PassiveOnSwitch()
    {
        userHealth.HealingPower += healingPower;
    }

    public override void PassiveOffSwitch()
    {
        userHealth.HealingPower -= healingPower;
    }

    public override void PassiveOnAbilitiy()
    {

    }
}
