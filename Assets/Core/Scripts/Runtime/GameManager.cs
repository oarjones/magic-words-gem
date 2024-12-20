
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameManager class orchestrates the overall game flow.
/// It should be implemented as a Singleton.
/// </summary>
public class GameManager : MonoBehaviour
{
    // Instance of the GameManager for the Singleton pattern
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
