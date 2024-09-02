using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectClickable : MonoBehaviour
{
    private void OnMouseDown()
    {
        PlayerElements.Enemy = gameObject;
    }
}
