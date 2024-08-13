using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalCoroutines : MonoBehaviour
{
    public static GlobalCoroutines Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Found more than one Input Manager in the scene.");
        }
        Instance = this;
    }
}
