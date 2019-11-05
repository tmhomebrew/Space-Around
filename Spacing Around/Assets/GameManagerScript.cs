using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    private static Rotater backgroundHandler;
    private static SpawnerAstroids astroidsSpawnerHandler;
    
    // Start is called before the first frame update
    void Start()
    {
        backgroundHandler = FindObjectOfType<Rotater>();
        astroidsSpawnerHandler = FindObjectOfType<SpawnerAstroids>();

        SetupLevel(true);
    }

    static void SetupLevel(bool isGameActive)
    {
        backgroundHandler.RotationController(isGameActive);
        astroidsSpawnerHandler.IsGameRunning = isGameActive;
    }
}
