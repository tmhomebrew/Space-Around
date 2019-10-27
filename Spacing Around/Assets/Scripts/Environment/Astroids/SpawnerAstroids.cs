using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerAstroids : MonoBehaviour
{
    [SerializeField]
    bool IsGameRunning;
    [SerializeField]
    ShipStats myShip; //Test
    public AstroidScript myAstroid;

    public GameObject enemyAstroid;
    //public GameObject astroidDir;
    public Transform astroidHolder;
    public float spawnTimer;
    public Transform[] spawnPoints;

    //Astroid-settings
    [SerializeField]
    List<Sprite> astroidList = new List<Sprite>();
    List<GameObject> astroidsInGame = new List<GameObject>();
    [SerializeField]
    int numberOfAstroidsInGame;


    // Start is called before the first frame update
    void Start()
    {
        if (myShip == null)
        {
            myShip = GameObject.FindGameObjectWithTag("Player").transform.GetComponentInChildren<ShipStats>();
        }
        numberOfAstroidsInGame = astroidsInGame.Count;
        IsGameRunning = true;
        InvokeRepeating("Spawn", spawnTimer, spawnTimer);
    }

    void Spawn()
    {
        if (!IsGameRunning || numberOfAstroidsInGame > 5)
        {
            return;
        }

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        int astroidIndex = Random.Range(0, astroidList.Count);

        Instantiate(enemyAstroid, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation, astroidHolder);
        if (myAstroid == null)
        {
            myAstroid = enemyAstroid.GetComponent<AstroidScript>();
            myAstroid.GetComponent<AstroidScript>().MySpawner = spawnPoints[spawnPointIndex];
        }
        enemyAstroid.GetComponent<SpriteRenderer>().sprite = astroidList[astroidIndex];
        enemyAstroid.GetComponent<AstroidScript>().MySpawner = spawnPoints[spawnPointIndex];

        astroidsInGame.Add(enemyAstroid);
        numberOfAstroidsInGame = astroidsInGame.Count;
    }
}