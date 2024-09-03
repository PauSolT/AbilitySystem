using UnityEngine;

public class GameObjectClickable : MonoBehaviour
{
    private void OnMouseEnter()
    {
        PlayerElements.Enemy = gameObject;
    }

    private void OnMouseExit()
    {
        PlayerElements.Enemy = null;
    }
}
