using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipSelector : MonoBehaviour
{
    [SerializeField]
    GameObject preSelectedShipGO, newShip;
    [SerializeField]
    GameObject placePrevPrev, placePrev, selectedShip, placeNext, placeNextNext;
    [SerializeField]
    Button butLeft, butRight;
    [SerializeField]
    Material matInvisible, matNotselected, matSelected;
    [SerializeField]
    List<GameObject> shipList = new List<GameObject>();
    [SerializeField]
    List<GameObject> showList;
    [SerializeField]
    private int selectionIndex = 2;

    private void Awake()
    {
        showList = new List<GameObject>()/* { placePrevPrev, placePrev, selectedShip, placeNext, placeNextNext }*/;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform go in GetComponentsInChildren<Transform>())
        {
            if (go.name.Contains("Placement"))
            {
                shipList.Add(go.GetChild(0).gameObject);
                showList.Add(go.GetChild(0).gameObject);
            }
        }
        AvailableChoiseButtons();

        if (preSelectedShipGO == null)
        {
            selectedShip = shipList[selectionIndex];
            preSelectedShipGO = selectedShip;
        }
        else
        {
            selectedShip = preSelectedShipGO;
        }
        SetMatsForShowList();
    }

    public void RightButton()
    {
        Selecter(-1);
    }

    public void LeftButton()
    {
        Selecter(+1);
    }

    void Selecter(int i)
    {
        selectionIndex += i;
        if (selectionIndex <= 2 && selectionIndex >= 0)
        {
            showList[4] = shipList[selectionIndex + 2];
        }
        else
        {
            showList[4] = null;
        }
        if (selectionIndex <= 3 && selectionIndex >= 0)
        {
            showList[3] = shipList[selectionIndex + 1];
        }
        else
        {
            showList[3] = null;
        }
        if (selectionIndex > 1 && selectionIndex <= shipList.Count)
        {
            showList[0] = shipList[selectionIndex - 2];
        }
        else
        {
            showList[0] = null;
        }
        if (selectionIndex > 0 && selectionIndex <= shipList.Count)
        {
            showList[1] = shipList[selectionIndex - 1];
        }
        else
        {
            showList[1] = null;
        }

        showList[2] = shipList[selectionIndex];
        AvailableChoiseButtons();
    }

    void AvailableChoiseButtons()
    {
        if (shipList.First() == showList[2])
        {
            butRight.interactable = false;
        }
        else
        {
            butRight.interactable = true;
        }
        if (shipList.Last() == showList[2])
        {
            butLeft.interactable = false;
        }
        else
        {
            butLeft.interactable = true;
        }
        SetMatsForShowList();
    }

    void SetMatsForShowList()
    {
        //Skal ændres så det kun er midlertidigt
        for (int i = 0; i < showList.Count; i++)
        {
            if (showList[i] != null)
            {
                if (i == 1 || i == 3)
                {
                    showList[i].GetComponentInChildren<MeshRenderer>().material = matNotselected;
                }
                if (i == 0 || i == 4)
                {
                    showList[i].GetComponentInChildren<MeshRenderer>().material = matInvisible;
                }
                if (i == 2)
                {
                    showList[i].GetComponentInChildren<MeshRenderer>().material = matSelected;
                }
            }
        }
    }
}