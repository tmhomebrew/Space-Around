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
    Material matInvisible, matNotselected, matSelected;
    [SerializeField]
    List<GameObject> shipList = new List<GameObject>();
    List<GameObject> nonPrefabList, placementList;
    [SerializeField]
    private int selectionIndex;
    [SerializeField]
    List<GameObject> showList;

    public GameObject SelectedShip { get => selectedShip; set => selectedShip = value; }
    public List<GameObject> PlacementList { get => placementList; set => placementList = value; }

    private void Awake()
    {
        PlacementList = new List<GameObject>();
        nonPrefabList = new List<GameObject>();
        showList = new List<GameObject>();
        foreach (Transform go in GetComponentsInChildren<Transform>())
        {
            if (go.name.Contains("Placement"))
            {
                PlacementList.Add(go.gameObject);
            }
        }
        SetupShips(selectionIndex);
        ArrangeAvailableShips(selectionIndex);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (preSelectedShipGO == null)
        {
            SelectedShip = nonPrefabList[selectionIndex];
            preSelectedShipGO = SelectedShip;
        }
        else
        {
            SelectedShip = preSelectedShipGO;
        }
    }

    #region Right And Left Button System
    public void RightButton()
    {
        ArrangeAvailableShips(1);
    }

    public void LeftButton()
    {
        ArrangeAvailableShips(-1);
    }

    public void ArrangeAvailableShips(int i = 0)
    {
        selectionIndex += i;

        if (selectionIndex < nonPrefabList.Count -2)
        {
            showList[4] = nonPrefabList[selectionIndex +2];
        }
        else
        {
            showList[4] = null;
        }
        if (selectionIndex < nonPrefabList.Count -1)
        {
            showList[3] = nonPrefabList[selectionIndex +1];
        }
        else
        {
            showList[3] = null;
        }
        if (selectionIndex - 2 >= 0)
        {
            showList[0] = nonPrefabList[selectionIndex -2];
        }
        else
        {
            showList[0] = null;
        }
        if (selectionIndex - 1 >= 0)
        {
            showList[1] = nonPrefabList[selectionIndex - 1];
        }
        else
        {
            showList[1] = null;
        }

        showList[2] = nonPrefabList[selectionIndex];
        SelectedShip = nonPrefabList[selectionIndex];
        AvailableChoiseButtons();
    }
    #endregion

    void AvailableChoiseButtons()
    {
        if (selectionIndex == nonPrefabList.Count - 1)
        {
            butRight.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            butRight.interactable = false;
            butRight.image.enabled = false;
        }
        else
        {
            butRight.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
            butRight.interactable = true;
            butRight.image.enabled = true;
        }
        if (selectionIndex == 0)
        {
            butLeft.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            butLeft.interactable = false;
            butLeft.image.enabled = false;
        }
        else
        {
            butLeft.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
            butLeft.interactable = true;
            butLeft.image.enabled = true;
        }

        SetMatsForShowList();
        SetNewPlacements();
    }

    void SetNewPlacements()
    {
        for (int i = 0; i < PlacementList.Count; i++)
        {
            if (showList[i] != null)
            {
                showList[i].transform.SetParent(PlacementList[i].transform);
                showList[i].transform.position = PlacementList[i].transform.position;
                showList[i].transform.localScale = PlacementList[i].transform.localScale;
                showList[i].transform.rotation = PlacementList[i].transform.rotation;
            }
        }
    }
    void SetMatsForShowList()
    {
        for (int i = 0; i < showList.Count; i++)
        {
            if (showList[i] != null)
            {   
                if (i == 1 || i == 3)
                {
                    showList[i].GetComponent<MeshRenderer>().material = matNotselected;
                }
                if (i == 0 || i == 4)
                {
                    showList[i].GetComponent<MeshRenderer>().material = matInvisible;
                }
                if (i == 2)
                {
                    showList[i].GetComponent<MeshRenderer>().material = matSelected;
                }
            }
        }
    }

    void SetupShips(int index)
    {
        GameObject temp;
        for (int i = 0; i < shipList.Count; i++)
        {
            if (i < PlacementList.Count)
            {
                temp = Instantiate(shipList[i], PlacementList[i].transform.position, PlacementList[i].transform.rotation, PlacementList[i].transform);
                showList.Add(temp);
            }
            else
            {
                temp = Instantiate(shipList[i], transform);
            }
            nonPrefabList.Add(temp);
            temp = null;
        }
    }

    public void AddShipToList(GameObject newShip)
    {
        shipList.Add(newShip);
        print("Ship added to shipList..: + " + newShip.transform.name);
    }

    public void RemoveShipFromList(GameObject shipToRemove)
    {
        foreach (GameObject go in shipList)
        {
            if (go.transform.name.ToLower() == shipToRemove.transform.name.ToLower())
            {
                print("Ship removed from shipList..: " + go.transform.name);
                shipList.Remove(go);
                break;
            }
        }
    }
}