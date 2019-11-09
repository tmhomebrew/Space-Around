using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerAstroids : MonoBehaviour
{
    [SerializeField]
    private bool isGameRunning;
    
    public GameObject enemyAstroid;
    //public GameObject astroidDir;
    public Transform astroidHolder;
    public float spawnTimer;
    public List<Transform> spawnPoints;
    public int numberOfAstroidsMax;

    //Astroid-settings
    [SerializeField]
    List<Sprite> astroidList = new List<Sprite>();
    List<GameObject> astroidsInGame = new List<GameObject>();
    [SerializeField]
    private int numberOfAstroidsInGame;

    public bool IsGameRunning { get => isGameRunning; set => isGameRunning = value; }
    public int NumberOfAstroidsInGame { get => numberOfAstroidsInGame; set => numberOfAstroidsInGame = value; }

    // Start is called before the first frame update
    void Start()
    {
        numberOfAstroidsMax = 20; //<--- For Test purpose..
        foreach (Transform go in GetComponentsInChildren<Transform>())
        {
            if (go.gameObject.name.Contains("SpawnPosHolder"))
            {
                spawnPoints.Add(go);
            }
        }

        NumberOfAstroidsInGame = astroidsInGame.Count;
        InvokeRepeating("Spawn", spawnTimer, spawnTimer);
    }

    void Spawn()
    {
        if (!IsGameRunning || NumberOfAstroidsInGame > numberOfAstroidsMax)
        {
            return;
        }

        int spawnPointIndex = Random.Range(0, spawnPoints.Count);
        int astroidIndex = Random.Range(0, astroidList.Count);

        Instantiate(enemyAstroid, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation, astroidHolder);
        enemyAstroid.GetComponent<SpriteRenderer>().sprite = astroidList[astroidIndex];
        enemyAstroid.GetComponent<AstroidScript>().MyLaunchDir = spawnPoints[spawnPointIndex];

        astroidsInGame.Add(enemyAstroid);
        NumberOfAstroidsInGame++;
    }
}