using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerAstroids : MonoBehaviour
{
    [SerializeField]
    private bool isGameRunning;
    [SerializeField]

    public Transform astroidHolder;
    public float spawnTimer;
    public List<Transform> spawnPoints;

    //Astroid-settings
    [SerializeField]
    List<Sprite> astroidList = new List<Sprite>();
    List<GameObject> astroidsInGame = new List<GameObject>();
    [SerializeField]
    private int numberOfAstroidsInGame;

    public bool IsGameRunning
    {
        get => isGameRunning; 
        set
        {
            isGameRunning = value;
            if (isGameRunning)
            {
                StartCoroutine(Spawn());
            }
            else
            {
                StopCoroutine(Spawn());
            }
        }
    }
    public int NumberOfAstroidsInGame { get => numberOfAstroidsInGame; set => numberOfAstroidsInGame = value; }

    //Object Pool
    private PrefabPooling myPool;
    int spawnPointIndex, astroidIndex;
    GameObject newAstroid;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform go in GetComponentsInChildren<Transform>())
        {
            if (go.gameObject.name.Contains("SpawnPosHolder"))
            {
                spawnPoints.Add(go.GetChild(0));
            }
        }

        //Object Pool
        myPool = GetComponent<PrefabPooling>();

        NumberOfAstroidsInGame = astroidsInGame.Count;
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTimer);
            if (myPool.GetAvailableObject() != null)
            {
                SetupNewAstroid();
            }
        }
    }

    void SetupNewAstroid()
    {
        spawnPointIndex = Random.Range(0, spawnPoints.Count);
        astroidIndex = Random.Range(0, astroidList.Count);
        newAstroid = myPool.GetAvailableObject(); //Object
        newAstroid.transform.position = spawnPoints[spawnPointIndex].position; //Position
        newAstroid.transform.rotation = spawnPoints[spawnPointIndex].rotation; //Rotation
        newAstroid.transform.SetParent(astroidHolder); //Parent
        newAstroid.GetComponent<SpriteRenderer>().sprite = astroidList[astroidIndex];
        Destroy(newAstroid.GetComponent<PolygonCollider2D>());
        newAstroid.AddComponent<PolygonCollider2D>();
        newAstroid.GetComponent<AstroidScript>().MyLaunchDir = spawnPoints[spawnPointIndex];

        astroidsInGame.Add(newAstroid);
        NumberOfAstroidsInGame++;
        newAstroid.SetActive(true);
    }
}