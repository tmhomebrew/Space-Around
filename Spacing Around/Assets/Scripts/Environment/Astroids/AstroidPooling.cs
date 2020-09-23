using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidPooling : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabAstroid, pooledObj;
    [SerializeField]
    private int poolSize;
    [SerializeField]
    private bool canGrow = false;

    private readonly List<GameObject> poolOfAstroid = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            pooledObj = Instantiate(prefabAstroid, this.transform.GetChild(0));
            pooledObj.SetActive(false);
            poolOfAstroid.Add(pooledObj);
        }
    }

    public GameObject GetAvailableObject()
    {
        for (int i = 0; i < poolOfAstroid.Count; i++)
        {
            if (poolOfAstroid[i].activeInHierarchy == false)
            {
                return poolOfAstroid[i];
            }
        }
        if (canGrow)
        {
            pooledObj = Instantiate(prefabAstroid, this.transform.GetChild(0));
            pooledObj.SetActive(false);
            poolOfAstroid.Add(pooledObj);
            
            return pooledObj;
        }
        else
        {
            return null;
        }
    }
}
