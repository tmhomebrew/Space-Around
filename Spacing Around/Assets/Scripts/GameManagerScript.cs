using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    private static Rotater backgroundHandler;
    private static SpawnerAstroids astroidsSpawnerHandler;

    [SerializeField]
    bool gameIsActive = true;

    // Start is called before the first frame update
    void Start()
    {
        backgroundHandler = FindObjectOfType<Rotater>();
        astroidsSpawnerHandler = FindObjectOfType<SpawnerAstroids>();

        SetupLevel(gameIsActive);
    }

    static void SetupLevel(bool isGameActive)
    {
        backgroundHandler.RotationController(isGameActive);
        astroidsSpawnerHandler.IsGameRunning = isGameActive;
    }
}
