using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidPooling : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabAstroid;
    [SerializeField]
    private int poolSize;
    [SerializeField]
    private bool canGrow = true;

    private readonly List<GameObject> poolOfAstroid = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject pooledObject = Instantiate(prefabAstroid);
            pooledObject.SetActive(false);
            poolOfAstroid.Add(pooledObject);
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
            GameObject pooledObject = Instantiate(prefabAstroid);
            pooledObject.SetActive(false);
            poolOfAstroid.Add(pooledObject);
            
            return pooledObject;
        }
        else
        {
            return null;
        }
    }
}
