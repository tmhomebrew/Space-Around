using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipSelector : MonoBehaviour
{
    [SerializeField]
    GameObject selectedShip, preSelectedShipGO;
    GameObject newShip; //For Customise button
    [SerializeField]
    Button butLeft, butRight;
    [SerializeField]
    Material matInvisible, matOuterSelected, matNotSelected, matSelected;
    [SerializeField]
    List<GameObject> shipList = new List<GameObject>();
    [SerializeField]
    private int selectionIndex;
    [SerializeField]
    List<GameObject> showList;
    List<GameObject> placementList;

    public GameObject SelectedShip { get => selectedShip; set => selectedShip = value; }
    public List<GameObject> PlacementList { get => placementList; set => placementList = value; }
    public List<GameObject> ShipList { get => shipList; set => shipList = value; }

    private void Awake()
    {
        PlacementList = new List<GameObject>();
        showList = new List<GameObject>();
        foreach (Transform go in GetComponentsInChildren<Transform>())
        {
            if (go.name.Contains("Placement"))
            {
                PlacementList.Add(go.gameObject);
            }
        }
        SetupShips();

        ChangeShip(0);
    }

    private void OnEnable()
    {
        ChangeShip(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (preSelectedShipGO == null)
        {
            SelectedShip = showList[selectionIndex];
            preSelectedShipGO = SelectedShip;
        }
        else
        {
            SelectedShip = preSelectedShipGO;
        }
    }

    #region Right And Left Button System
    private void ChangeShip(int _index)
    {
        selectionIndex += _index;
        if (_index != 0)
        {
            PlacementList[2].transform.localScale = Vector3.one;
            //PlacementList[2].GetComponent<ObjectViewing>().ResetObjPosAndRot(); //<-- To reset Rotation of ship-Obj..
        }
        SelectShip(selectionIndex);
    }

    public void SelectShip(int shipNumb)
    {
        butLeft.interactable = (shipNumb != 0);
        butRight.interactable = (shipNumb != showList.Count - 1);

        for (int i = 0; i < showList.Count; i++)
        {
            showList[i].GetComponent<Renderer>().enabled = true;
            //PlacementCurrent
            if (i == shipNumb)
            {
                selectedShip = showList[i];
                showList[i].transform.SetParent(PlacementList[2].transform);
                showList[i].transform.position = PlacementList[2].transform.position;
                showList[i].transform.localScale = PlacementList[2].transform.localScale;
                showList[i].transform.rotation = PlacementList[2].transform.rotation;

                showList[i].GetComponent<MeshRenderer>().material = matSelected;
            }
            //PlacementPrevPrev
            else if (i == shipNumb - 2)
            {
                showList[i].transform.SetParent(PlacementList[0].transform);
                showList[i].transform.position = PlacementList[0].transform.position;
                showList[i].transform.localScale = PlacementList[0].transform.localScale;
                showList[i].transform.rotation = PlacementList[0].transform.rotation;

                showList[i].GetComponent<MeshRenderer>().material = matOuterSelected;
            }
            //PlacementPrev
            else if (i == shipNumb -1)
            {
                showList[i].transform.SetParent(PlacementList[1].transform);
                showList[i].transform.position = PlacementList[1].transform.position;
                showList[i].transform.localScale = PlacementList[1].transform.localScale;
                showList[i].transform.rotation = PlacementList[1].transform.rotation;

                showList[i].GetComponent<MeshRenderer>().material = matNotSelected;
            }
            //PlacementNext
            else if (i == shipNumb + 1)
            {
                showList[i].transform.SetParent(PlacementList[3].transform);
                showList[i].transform.position = PlacementList[3].transform.position;
                showList[i].transform.localScale = PlacementList[3].transform.localScale;
                showList[i].transform.rotation = PlacementList[3].transform.rotation;

                showList[i].GetComponent<MeshRenderer>().material = matNotSelected;
            }
            //PlacementNextNext
            else if (i == shipNumb + 2)
            {
                showList[i].transform.SetParent(PlacementList[4].transform);
                showList[i].transform.position = PlacementList[4].transform.position;
                showList[i].transform.localScale = PlacementList[4].transform.localScale;
                showList[i].transform.rotation = PlacementList[4].transform.rotation;
                
                showList[i].GetComponent<MeshRenderer>().material = matOuterSelected;
            }
            else
            {
                showList[i].GetComponent<MeshRenderer>().material = matInvisible;
                showList[i].GetComponent<Renderer>().enabled = (i == shipNumb);
            }
        }
    }
    #endregion

    /// <summary>
    /// Setup ships from ShipList to Showlist, based on PlacementHolder-GOs..
    /// When called, arranges from first object in ShipList at PlacementList[2]..
    /// </summary>
    void SetupShips()
    {
        GameObject temp;
        for (int i = 0; i < ShipList.Count; i++)
        {
            //i + 2, To start placement at 'PlacementCurrent'
            if (i + 2 < PlacementList.Count)
            {
                temp = Instantiate(ShipList[i], 
                    PlacementList[i].transform.position,
                    PlacementList[i].transform.rotation, 
                    PlacementList[i].transform);
            }
            else
            {
                temp = Instantiate(ShipList[i], transform);
            }
            showList.Add(temp);
            temp = null;
        }
    }

    public void AddShipToList(GameObject newShip)
    {
        ShipList.Insert(0, newShip);
        print("Ship added to shipList..: + " + newShip.transform.name);
    }

    public void RemoveShipFromList(GameObject shipToRemove)
    {
        ShipList.RemoveAt(selectionIndex); //Primary shipList
        showList.Clear(); //List of ships to be placed in PlacementList
        //Destroys all gameObjects, which is not a Placement or the ShipHolder itself
        foreach (Transform t in transform.GetComponentsInChildren<Transform>())
        {
            if (!t.name.ToLower().Contains("placement") && t != transform)
            {
                Destroy(t.gameObject);
            }
        }
        //Setup the new ShipList to ShowList
        SetupShips();
        //ReArranges new list, with starting point at last index
        if (selectionIndex == 0)
        {
            SelectShip(0);
        }
        else if(selectionIndex < showList.Count)
        {
            SelectShip(selectionIndex);
        }
        else
        {
            SelectShip(selectionIndex - 1);
        }
    }
}