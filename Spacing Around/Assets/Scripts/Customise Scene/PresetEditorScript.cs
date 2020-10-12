using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PresetEditorScript : MonoBehaviour
{
    CanvasController myCanvasCont;
    ChooseUI myConfirm;
    [SerializeField]
    private GameObject shipHolder, changedShip, curShip;
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
        if (changedShip == null)
        {
            curShip = ShipHolder.transform.GetChild(0).gameObject; //<------
            changedShip = Instantiate(curShip, ShipHolder.transform);
        }
        LoadValuesOfShip();
    }

    void UponChangesToShip(string text)
    {
        myConfirm.OpenAskUI(text);

    }

    void LoadValuesOfShip()
    {
        //Temp-test.. Is on, is of..
        if (changedShip.GetComponent<TestScript>().HasChanged)
        {
            changeValue.image.color = new Color(0, 255, 0);
        }
        else
        {
            changeValue.image.color = new Color(255, 0, 0);
        }
    }

    //Save-button
    public void SavePreset()
    {
        UponChangesToShip("You have made changes to your current preset, do you want to overwrite last save.?");

        if (myConfirm.SetQuestion)
        {
            //Should set every value (changedShip/TempShip to Shipholder)..
            curShip.GetComponent<TestScript>().HasChanged = changedShip.GetComponent<TestScript>().HasChanged;

            //
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
                changedShip.GetComponent<TestScript>().HasChanged = curShip.GetComponent<TestScript>().HasChanged;
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
        if (changedShip != null && myCanvasCont.ChangeScene)
        {
            foreach (Transform child in ShipHolder.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }

    //Redo-button
    public void ResetPreset()
    {
        UponChangesToShip("Reset your preset to last save.?");

        if (myConfirm.SetQuestion)
        {
            changedShip.GetComponent<TestScript>().HasChanged = curShip.GetComponent<TestScript>().HasChanged;
        }
    }

    //Delete-button
    public void DeletePreset()
    {
        UponChangesToShip("Do you want to delete your current preset.?");
        if (myConfirm.SetQuestion)
        {
            //Delete current preset
            myCanvasCont.MySS.RemoveShipFromList(curShip);
            Destroy(curShip);
            Destroy(changedShip);
            //Return to ShipSelector, ChangeScene..
            myCanvasCont.ChangeScene = true;
        }
    }

    public void ChangeValue()
    {
        if (!changedShip.GetComponent<TestScript>().HasChanged)
        {
            changeValue.image.color = new Color(0, 255, 0);
        }
        else
        {
            changeValue.image.color = new Color(255, 0, 0);
        }
        changedShip.GetComponent<TestScript>().HasChanged = !changedShip.GetComponent<TestScript>().HasChanged;
    }

    public bool CompareShips()
    {
        if (curShip.GetComponent<TestScript>().HasChanged != changedShip.GetComponent<TestScript>().HasChanged)
        {
            print("Its not the same!!");
            return false;
        }

        print("Its the same!!");
        return true;
    }
}