using System.Collections.Generic;
using UnityEngine;

public class PrefabPooling : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabObj, pooledObj;
    [SerializeField]
    private Transform parentTransform;
    [SerializeField]
    private int poolSize;
    [SerializeField]
    private bool canGrow = false;

    private List<GameObject> poolOfPrefabs;

    public List<GameObject> PoolOfPrefabs { get => poolOfPrefabs; private set => poolOfPrefabs = value; }

    /// <summary>
    /// Instantiates a new ObjPool, as a List of GameObjects.
    /// Depending on size(x) of pool, fills the list with x-amount of GameObjs.
    /// Sets each GameObj to InActive.
    /// Adds GameObj to ObjPool-List.
    /// </summary>
    private void Awake()
    {
        PoolOfPrefabs = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            pooledObj = Instantiate(prefabObj, parentTransform);
            pooledObj.SetActive(false);
            PoolOfPrefabs.Add(pooledObj);
        }
    }

    /// <summary>
    /// Fetches an Available GameObject from defined GameObjPool.
    /// ObjPool can dynamic grow in size if required (if 'canGrow' is true), and returns new GameObj.
    /// </summary>
    /// <returns>If there is an availabe GameObj, return one. Else, return null.</returns>
    public GameObject GetAvailableObject()
    {
        for (int i = 0; i < PoolOfPrefabs.Count; i++)
        {
            if (PoolOfPrefabs[i].activeInHierarchy == false)
            {
                return PoolOfPrefabs[i];
            }
        }
        if (canGrow)
        {
            pooledObj = Instantiate(prefabObj, parentTransform);
            pooledObj.SetActive(false);
            PoolOfPrefabs.Add(pooledObj);
            poolSize++;
            
            return pooledObj;
        }
        else
        {
            return null;
        }
    }
}
