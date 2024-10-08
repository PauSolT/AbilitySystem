using UnityEngine;

public class BombSeedPrefabDetectHealing : MonoBehaviour
{
    public bool userToHeal = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out HealthComponent health) && other.CompareTag("Player"))
        {
            userToHeal = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out HealthComponent health) && other.CompareTag("Player"))
        {
            userToHeal = false;
        }
    }

}
