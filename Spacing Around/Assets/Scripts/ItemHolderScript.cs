using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolderScript : MonoBehaviour
{
    public List<GameObject> itemHolderList;
    // Start is called before the first frame update
    void Start()
    {
        itemHolderList = new List<GameObject>();
        if (itemHolderList.Count < 1)
        {
            //if (GetComponentsInChildren<GameObject>().Length > 0)
            //{
            //    foreach (GameObject childGO in GetComponentsInChildren<GameObject>())
            //    {
            //        if (childGO != this.gameObject)
            //        {
            //            itemHolderList.Add(childGO);
            //        }
            //    }
            //}
            //else
            string strVar = "Prefabs/Weapons";
            GameObject[] weaponsArr = Resources.LoadAll<GameObject>(strVar) as GameObject[];
            if (weaponsArr == null || weaponsArr.Length == 0)
            {
                print("No Files where found");
                return;
            }
            else
            {
                int x = 0;
                foreach (GameObject prefab in weaponsArr)
                {
                    GameObject go = prefab;
                    itemHolderList.Add(go);
                    print(itemHolderList[x]);
                    x++;
                }
            }
        }
        print("Done adding to ItemHolderList");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
