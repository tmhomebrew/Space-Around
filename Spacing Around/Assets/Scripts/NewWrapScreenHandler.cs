using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWrapScreenHandler : MonoBehaviour
{
    BoxCollider2D myCol;

    private void Start()
    {
        myCol = GetComponent<BoxCollider2D>();
    }

    void Wrap(Transform GO)
    {
        //x-axis, Left side
        if (GO.transform.position.x < (myCol.size.x / 2) * -1)
        {
            GO.transform.position = new Vector3((myCol.size.x / 2) * 1, GO.transform.position.y, GO.transform.position.z);
        }
        //x-axis, Right side
        if (GO.transform.position.x > (myCol.size.x / 2) * 1)
        {
            GO.transform.position = new Vector3((myCol.size.x / 2) * -1, GO.transform.position.y, GO.transform.position.z);
        }
        //y-axis, Top side
        if (GO.transform.position.y > (myCol.size.y / 2) * 1)
        {
            GO.transform.position = new Vector3(GO.transform.position.x, (myCol.size.y / 2) * -1, GO.transform.position.z);
        }
        //y-axis, Right side
        if (GO.transform.position.y < (myCol.size.y / 2) * -1)
        {
            GO.transform.position = new Vector3(GO.transform.position.x, (myCol.size.y / 2) * 1, GO.transform.position.z);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.root.name == "Player")
        {
            Wrap(col.transform.root.GetComponentInChildren<ShipStats>().transform);
            //print("Player has been moved.." + col.transform.root.GetComponentInChildren<ShipStats>().transform);
        }
        else
        {
            Wrap(col.transform);
            //print("Something entered the field");
        }

    }
}
