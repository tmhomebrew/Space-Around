using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PresetEditorScript : MonoBehaviour
{
    enum PresetChoise
    {
        Delete = 1,
        Reset = 2,
        Save = 3,
        Back = 4,
    }

    CanvasController myCanvasCont;
    ChooseUI myConfirm;
    [SerializeField]
    private GameObject shipHolder, changedShip, curShip;
    [SerializeField]
    private Button changeValue;
    //ChoiseUI
    [SerializeField]
    private Button noButton, yesButton;

    //Reference
    public RotateObj myRotateobject;

    public GameObject ShipHolder { get => shipHolder; set => shipHolder = value; }

    private void Awake()
    {
        myCanvasCont = GetComponentInParent<CanvasController>();
        myConfirm = GetComponentInChildren<ChooseUI>();
    }

    public void OnOpenShipEditor()
    {
        curShip = ShipHolder.transform.GetChild(0).gameObject; //<------
        if (changedShip == null)
        {
            changedShip = Instantiate(curShip, ShipHolder.transform.position, ShipHolder.transform.rotation, ShipHolder.transform);
        }
        else
        {
            changedShip = curShip;
        }
        curShip.GetComponent<Renderer>().enabled = false;
        changedShip.name = "TempShip";
        LoadValuesOfShip();
    }

    void UponChangesToShip(string text)
    {
        myConfirm.OpenAskUI(text);
    }

    IEnumerator WaitForResponse(PresetChoise pc)
    {
        WaitForUIButtons waitForButton = new WaitForUIButtons(yesButton, noButton);
        myRotateobject.enabled = false; //Hard coded
        yield return waitForButton.Reset();
        if (waitForButton.PressedButton == yesButton)
        {
            // yes was pressed
            switch (pc)
            {
                case PresetChoise.Delete:
                    if (myCanvasCont.MySS.ShipList.Count > 1)
                    {
                        //Delete current preset
                        myCanvasCont.MySS.RemoveShipFromList(changedShip);
                        Destroy(curShip);
                    }
                    else
                    {
                        print("Could not delete last ship..");
                    }
                    //Return to ShipSelector, ChangeScene..
                    Destroy(changedShip);
                    myCanvasCont.ChangeScene = true;
                    break;
                case PresetChoise.Reset:
                    changedShip.GetComponent<TestScript>().HasChanged = curShip.GetComponent<TestScript>().HasChanged;
                    LoadValuesOfShip();
                    break;
                case PresetChoise.Save:
                    curShip.GetComponent<TestScript>().HasChanged = changedShip.GetComponent<TestScript>().HasChanged;
                    LoadValuesOfShip();
                    break;
                case PresetChoise.Back:
                    curShip.GetComponent<Renderer>().enabled = true;
                    changedShip.GetComponent<TestScript>().HasChanged = curShip.GetComponent<TestScript>().HasChanged;
                    myCanvasCont.ChangeScene = true;
                    foreach (Transform child in ShipHolder.transform)
                    {
                        Destroy(child.gameObject);
                    }
                    break;
                default:
                    break;
            }
        }
        else
        {
            // no was pressed
            switch (pc)
            {
                case PresetChoise.Delete:
                    break;
                case PresetChoise.Reset:
                    LoadValuesOfShip();
                    break;
                case PresetChoise.Save:
                    LoadValuesOfShip();
                    break;
                case PresetChoise.Back:
                    //Returns to Preset Editor
                    myCanvasCont.ChangeScene = false;
                    break;
                default:
                    break;
            }
        }
        myConfirm.CloseAskUI();
        myRotateobject.enabled = true; //Hard coded
    }

    void LoadValuesOfShip()
    {
        //Temp-test.. Is on, is of..
        if (changedShip.GetComponent<TestScript>().HasChanged)
        {
            //Green
            changeValue.image.color = new Color(0, 255, 0);
        }
        else
        {
            //Red
            changeValue.image.color = new Color(255, 0, 0);
        }
    }

    //Save-button
    public void SavePreset()
    {
        UponChangesToShip("You have made changes to your current preset, do you want to overwrite last save.?");
        StartCoroutine(WaitForResponse(PresetChoise.Save));
    }

    //Cancel and Back-buttons
    public void Back()
    {
        //If currentShip and TempShip are note the same (Meaning changes have been made)
        if (!CompareShips())
        {
            UponChangesToShip("You have unsaved changes, do you want to skip them.?");
            StartCoroutine(WaitForResponse(PresetChoise.Back));
        }
        //If no changes to ships
        else
        {
            curShip.GetComponent<Renderer>().enabled = true;
            myCanvasCont.ChangeScene = true;
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
        StartCoroutine(WaitForResponse(PresetChoise.Reset));
    }

    //Delete-button
    public void DeletePreset()
    {
        UponChangesToShip("Do you want to delete your current preset.?");
        StartCoroutine(WaitForResponse(PresetChoise.Delete));
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
            return false;
        }

        return true;
    }
}