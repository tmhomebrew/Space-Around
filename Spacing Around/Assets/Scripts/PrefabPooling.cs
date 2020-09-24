using System.Collections;
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

    private readonly List<GameObject> poolOfPrefabs = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            pooledObj = Instantiate(prefabObj, parentTransform);
            pooledObj.SetActive(false);
            poolOfPrefabs.Add(pooledObj);
        }
    }

    public GameObject GetAvailableObject()
    {
        for (int i = 0; i < poolOfPrefabs.Count; i++)
        {
            if (poolOfPrefabs[i].activeInHierarchy == false)
            {
                return poolOfPrefabs[i];
            }
        }
        if (canGrow)
        {
            pooledObj = Instantiate(prefabObj, parentTransform);
            pooledObj.SetActive(false);
            poolOfPrefabs.Add(pooledObj);
            poolSize++;
            
            return pooledObj;
        }
        else
        {
            return null;
        }
    }
}
