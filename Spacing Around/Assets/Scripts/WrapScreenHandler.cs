using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapScreenHandler : MonoBehaviour
{
    BoxCollider2D myCol;
    float boundingBoxSizeX, boundingBoxSizeY;

    private void Start()
    {
        myCol = GetComponent<BoxCollider2D>();
        boundingBoxSizeX = myCol.size.x;
        boundingBoxSizeY = myCol.size.y;
    }

    /// <summary>
    /// Wrap-methoed - Teleports an objects.transform to the other side of the map, depending on the point in worldSpace, where it leaves the boundingBoxArea.
    /// </summary>
    /// <param name="GO">transform of the Gameobject.</param>
    void Wrap(Transform GO)
    {
        //x-axis, Left side
        if (GO.transform.position.x < (boundingBoxSizeX / 2) * -1)
        {
            GO.transform.position = new Vector3((boundingBoxSizeX / 2) * 1, GO.transform.position.y, GO.transform.position.z);
        }
        //x-axis, Right side
        if (GO.transform.position.x > (boundingBoxSizeX / 2) * 1)
        {
            GO.transform.position = new Vector3((boundingBoxSizeX / 2) * -1, GO.transform.position.y, GO.transform.position.z);
        }
        //y-axis, Top side
        if (GO.transform.position.y > (boundingBoxSizeY / 2) * 1)
        {
            GO.transform.position = new Vector3(GO.transform.position.x, (boundingBoxSizeY / 2) * -1, GO.transform.position.z);
        }
        //y-axis, Right side
        if (GO.transform.position.y < (boundingBoxSizeY / 2) * -1)
        {
            GO.transform.position = new Vector3(GO.transform.position.x, (boundingBoxSizeY / 2) * 1, GO.transform.position.z);
        }
    }

    /// <summary>
    /// Registres if an object leaves the boundingBox of the Level.
    /// </summary>
    /// <param name="col">Object.Collider leaving the boundingBoxArea</param>
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.root.name == "Player")
        {
            Wrap(col.transform.root.GetComponentInChildren<ShipStats>().transform); //PlayerPrefab-->SpaceShip.transform
            //print("Player has been moved.." + col.transform.root.GetComponentInChildren<ShipStats>().transform);
        }
        else
        {
            Wrap(col.transform);
            //print("Something entered the field");
        }
    }
}