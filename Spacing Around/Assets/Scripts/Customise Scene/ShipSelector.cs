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
    //[SerializeField]
    //GameObject placePrevPrev, placePrev, , placeNext, placeNextNext;
    [SerializeField]
    Button butLeft, butRight;
    [SerializeField]
    Material matInvisible, matNotselected, matSelected;
    [SerializeField]
    List<GameObject> shipList = new List<GameObject>();
    public List<GameObject> placementList;
    [SerializeField]
    List<GameObject> nonPrefabList, showList;
    [SerializeField]
    private int selectionIndex = 0;

    private void Awake()
    {
        placementList = new List<GameObject>();
        nonPrefabList = new List<GameObject>();
        showList = new List<GameObject>();
        foreach (Transform go in GetComponentsInChildren<Transform>())
        {
            if (go.name.Contains("Placement"))
            {
                placementList.Add(go.gameObject);
                //showList.Add(go.GetChild(go.childCount - 1).gameObject);
                //if (go.childCount > 0)
                //{
                //    shipList.Add(go.GetChild(go.childCount - 1).gameObject);
                //}
            }
        }
        SetupShips(selectionIndex);
    }

    // Start is called before the first frame update
    void Start()
    {
        AvailableChoiseButtons();
        if (preSelectedShipGO == null)
        {
            selectedShip = nonPrefabList[selectionIndex];
            preSelectedShipGO = selectedShip;
        }
        else
        {
            selectedShip = preSelectedShipGO;
        }
    }

    #region Right And Left Button System
    public void RightButton()
    {
        Selecter(1);
    }

    public void LeftButton()
    {
        Selecter(-1);
    }

    void Selecter(int i)
    {
        selectionIndex += i;
        if (selectionIndex <= 2 && selectionIndex >= 0)
        {
            showList[4] = nonPrefabList[selectionIndex + 2];
        }
        else
        {
            showList[4] = null;
        }
        if (selectionIndex <= 3 && selectionIndex >= 0)
        {
            showList[3] = nonPrefabList[selectionIndex + 1];
        }
        else
        {
            showList[3] = null;
        }
        if (selectionIndex > 1 && selectionIndex <= shipList.Count)
        {
            showList[0] = nonPrefabList[selectionIndex - 2];
        }
        else
        {
            showList[0] = null;
        }
        if (selectionIndex > 0 && selectionIndex <= shipList.Count)
        {
            showList[1] = nonPrefabList[selectionIndex - 1];
        }
        else
        {
            showList[1] = null;
        }

        showList[2] = nonPrefabList[selectionIndex];
        selectedShip = nonPrefabList[selectionIndex];
        AvailableChoiseButtons();
    }
    #endregion

    void AvailableChoiseButtons()
    {
        if (nonPrefabList.Last() == placementList[2])
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
        if (nonPrefabList.First() == placementList[2])
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
        for (int i = 0; i < placementList.Count; i++)
        {
            placementList[i].transform.DetachChildren();
            if (showList[i] != null) //<--- Her er fejlen med listerne, måske.??
            {
                showList[i].transform.SetParent(placementList[i].transform);
                showList[i].transform.position = placementList[i].transform.position;
                showList[i].transform.localScale = placementList[i].transform.localScale;
                showList[i].transform.rotation = placementList[i].transform.rotation;
            }
        }
    }

    void SetupShips(int index)
    {
        for (int i = index; i < placementList.Count; i++)
        {
            GameObject temp = Instantiate(shipList[i], placementList[i].transform.position, placementList[i].transform.rotation, placementList[i].transform);
            nonPrefabList.Add(temp);
            //shipTemp.transform.SetParent(placementList[i].transform);
            //shipTemp.transform.position = placementList[i].transform.position;
            //shipTemp.transform.localScale = placementList[i].transform.localScale;
            //shipTemp.transform.rotation = placementList[i].transform.rotation;
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
}