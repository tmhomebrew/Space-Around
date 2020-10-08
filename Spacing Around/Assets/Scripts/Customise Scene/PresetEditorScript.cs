using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PresetEditorScript : MonoBehaviour
{
    CanvasController myCanvasCont;
    ChooseUI myConfirm;
    [SerializeField]
    private GameObject shipHolder, changedShip;
    [SerializeField]
    private Button changeValue;

    public GameObject ShipHolder { get => shipHolder; set => shipHolder = value; }

    private void Awake()
    {
        myCanvasCont = GetComponentInParent<CanvasController>();
        myConfirm = GetComponentInChildren<ChooseUI>();
    }

    public void OnOpenShipEditor()
    {
        changedShip = Instantiate(ShipHolder.transform.GetChild(0).gameObject); //<------
    }

    void UponChangesToShip(string text)
    {
        myConfirm.OpenAskUI(text);
        
    }

    //Save-button
    public void SavePreset()
    {
        UponChangesToShip("You have made changes to your current preset, do you want to overwrite last save.?");

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
            UponChangesToShip("You have unsaved changes, do you want to skip them.?");
            if (myConfirm.SetQuestion)
            {
                changedShip.GetComponent<TestScript>().HasChanged = ShipHolder.GetComponent<TestScript>().HasChanged;
                myCanvasCont.ChangeScene = true;
            }
            else
            {
                //Returns to Preset Editor
                myCanvasCont.ChangeScene = false;
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
        UponChangesToShip("Reset your preset to last save.?");

        if (myConfirm.SetQuestion)
        {
            changedShip.GetComponent<TestScript>().HasChanged = ShipHolder.GetComponent<TestScript>().HasChanged;
        }
    }

    //Delete-button
    public void DeletePreset()
    {
        UponChangesToShip("Do you want to delete your current preset.?");
        if (myConfirm.SetQuestion)
        {
            //Delete current preset
            myCanvasCont.MySS.RemoveShipFromList(ShipHolder.transform.GetChild(0).gameObject);
            Destroy(ShipHolder.transform.GetChild(0).gameObject);
            //Return to ShipSelector, ChangeScene..
            myCanvasCont.ChangeScene = true;
        }
    }

    public void ChangeValue()
    {
        changedShip.GetComponent<TestScript>().HasChanged = !changedShip.GetComponent<TestScript>().HasChanged;
        if (changedShip.GetComponent<TestScript>().HasChanged)
        {
            changeValue.image.color = new Color(0, 255, 0);
        }
        else
        {
            changeValue.image.color = new Color(255, 0, 0);
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