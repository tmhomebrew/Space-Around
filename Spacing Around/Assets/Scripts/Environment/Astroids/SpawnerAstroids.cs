using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerAstroids : MonoBehaviour
{
    [SerializeField]
    private bool isGameRunning;
    
    public GameObject enemyAstroid;
    public GameObject prefabForECS;
    public Transform astroidHolder;
    public float spawnTimer;
    public List<Transform> spawnPoints;

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
        //if(useECS)
        //{
        //    ecsManager = World.Active.EntityManager;

        //    astroidEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(prefabForECS, World.Active);
        //}

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
        if (!IsGameRunning || NumberOfAstroidsInGame > 1000)
        {
            return;
        }
        
        int spawnPointIndex = Random.Range(0, spawnPoints.Count);
        int astroidIndex = Random.Range(0, astroidList.Count);

        //if (!useECS)
        //{
        GameObject eAstroid = Instantiate(enemyAstroid, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation, astroidHolder) as GameObject;
        eAstroid.GetComponent<SpriteRenderer>().sprite = astroidList[astroidIndex];
        eAstroid.GetComponent<AstroidScript>().MyLaunchDir = spawnPoints[spawnPointIndex];

        astroidsInGame.Add(enemyAstroid);
        NumberOfAstroidsInGame++;
        //}
        //else
        //{
        //Entity astroidEnt = ecsManager.Instantiate(astroidEntityPrefab);

        //ecsManager.SetComponentData(astroidEnt, new Rotation { Value = spawnPoints[spawnPointIndex].rotation });
        //ecsManager.SetComponentData(astroidEnt, new Translation { Value = spawnPoints[spawnPointIndex].position });
        //}
    }
}