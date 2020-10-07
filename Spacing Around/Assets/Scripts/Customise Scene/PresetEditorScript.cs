using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresetEditorScript : MonoBehaviour
{
    CanvasController myCanvasCont;
    ChooseUI myConfirm;
    [SerializeField]
    private GameObject shipHolder, changedShip;

    public GameObject ShipHolder { get => shipHolder; set => shipHolder = value; }

    private void Awake()
    {
        myCanvasCont = GetComponentInParent<CanvasController>();
        myConfirm = GetComponentInChildren<ChooseUI>();
    }

    public void OnOpenShipEditor()
    {
        changedShip = ShipHolder.transform.GetChild(0).gameObject;
    }

    void UponChangesToShip()
    {
        myConfirm.OpenAskUI();
        
    }

    //Save-button
    public void SavePreset()
    {
        UponChangesToShip();

        if (myConfirm.SetQuestion)
        {
            ShipHolder.GetComponent<TestScript>().HasChanged = changedShip.GetComponent<TestScript>().HasChanged;
        }
    }

    //Cancel and Back-buttons
    public void Back()
    {
        if (!CompareShips())
        {
            UponChangesToShip();
            if (!myConfirm.SetQuestion)
            {
                myCanvasCont.ChangeScene = false;
            }
            else
            {
                changedShip.GetComponent<TestScript>().HasChanged = ShipHolder.GetComponent<TestScript>().HasChanged;
                myCanvasCont.ChangeScene = true;
            }
        }
        else
        {
            myCanvasCont.ChangeScene = true;
        }
    }

    //Redo-button
    public void ResetPreset()
    {
        UponChangesToShip();

        if (myConfirm.SetQuestion)
        {
            changedShip.GetComponent<TestScript>().HasChanged = ShipHolder.GetComponent<TestScript>().HasChanged;
        }
    }

    //Delete-button
    public void DeletePreset()
    {
        UponChangesToShip();
        if (myConfirm.SetQuestion)
        {
            //Delete current preset
            myCanvasCont.MySS.RemoveShipFromList(ShipHolder.transform.GetChild(0).gameObject);
            Destroy(ShipHolder.transform.GetChild(0).gameObject);
            //Return to ShipSelector, ChangeScene..
            myCanvasCont.ChangeScene = true;
        }
    }

    public bool CompareShips()
    {
        if (ShipHolder.transform.GetChild(0).GetComponent<TestScript>().HasChanged != changedShip.GetComponent<TestScript>().HasChanged)
        {
            print("Its not the same!!");
            return false;
        }

        print("Its the same!!");
        return true;
    }
}