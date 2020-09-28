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
    List<GameObject> showList;

    private int selectionIndex = 0;


    private void Awake()
    {
        showList = new List<GameObject>() { placePrevPrev, placePrev, selectedShip, placeNext, placeNextNext };
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform go in GetComponentsInChildren<Transform>())
        {
            if (go.name.Contains("Placement"))
            {
                shipList.Add(go.gameObject);
            }
        }

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

    void NextShip()
    {
        if (butRight.GetComponent<TextMeshPro>().text.ToLower() != "right")
        {
            //NewPreset()
        }
        else
        { 
            selectionIndex++;
            if (selectionIndex > shipList.Count)
            {
                selectionIndex = shipList.Count;
            }

            for (int i = 0; i <= 4; i++)
            {
                if (shipList[selectionIndex - (i - 2)] == null)
                {
                    showList[i] = null;
                }
                else
                {
                    showList[i] = shipList[selectionIndex - (i - 2)];
                }
            }
            
            //placePrevPrev = placePrev;
            //placePrevPrev.GetComponent<MeshRenderer>().material = matInvisible;
            //placePrev = selectedShip;
            //placePrev.GetComponent<MeshRenderer>().material = matNotselected;
            //selectedShip = placeNext; //Selected Ship
            //selectedShip.GetComponent<MeshRenderer>().material = matSelected;
            //placeNext = placeNextNext;
            //placeNext.GetComponent<MeshRenderer>().material = matNotselected;
            //placeNextNext.GetComponent<MeshRenderer>().material = matInvisible;
        }

        AvailableChoiseButtons();
    }

    void PrevShiP()
    {
        selectionIndex--;
        if (selectionIndex < 0)
        {
            selectionIndex = 0;
        }

        for (int i = 0; i <= 4; i++)
        {
            if (shipList[selectionIndex - (i - 2)] == null)
            {
                showList[i] = null;
            }
            else
            {
                showList[i] = shipList[selectionIndex - (i - 2)];
            }
        }

        //placePrevPrev = placePrev;
        //placePrevPrev.GetComponent<MeshRenderer>().material = matInvisible;
        //placePrev = selectedShip;
        //placePrev.GetComponent<MeshRenderer>().material = matNotselected;
        //selectedShip = placeNext; //Selected Ship
        //selectedShip.GetComponent<MeshRenderer>().material = matSelected;
        //placeNext = placeNextNext;
        //placeNext.GetComponent<MeshRenderer>().material = matNotselected;
        //placeNextNext.GetComponent<MeshRenderer>().material = matInvisible;

        AvailableChoiseButtons();
    }

    void SetMatsForShowList()
    {
        showList[0].GetComponentInChildren<MeshRenderer>().material = matInvisible;
        showList[1].GetComponentInChildren<MeshRenderer>().material = matNotselected;
        showList[2].GetComponentInChildren<MeshRenderer>().material = matSelected;
        showList[3].GetComponentInChildren<MeshRenderer>().material = matNotselected;
        showList[4].GetComponentInChildren<MeshRenderer>().material = matInvisible;
    }
    
    void AvailableChoiseButtons()
    {
        if (shipList.First() == showList[2])
        {
            butLeft.interactable = false;
        }
        else
        {
            butLeft.interactable = true;
        }
        if (shipList.Last() == showList[2])
        {
            butRight.GetComponent<TextMeshPro>().text = "New preset";
        }
        else
        {
            butRight.GetComponent<TextMeshPro>().text = "Right";
        }
        SetMatsForShowList();
    }
}