using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    #region Fields
    public GameObject cargoUI_GO;
    public GameObject statusBarUI_GO;
    [SerializeField]
    private bool showUI;

    #endregion
    //Refs
    Inventory myInven;

    
    // Start is called before the first frame update
    void Start()
    {
        myInven = transform.root.GetComponentInChildren<Inventory>();
        showUI = true;
        ShowUI(showUI);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Controller();
    }

    void Controller()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            myInven.InventorySlotsAvailable();
            ShowUI(showUI);
        }
    }

    /// <summary>
    /// Show UI for Players CargoPanel.
    /// </summary>
    /// <param name="isOn">true/false for CargoUI to be visible</param>
    void ShowUI(bool isOn)
    {
        if (isOn == false)
        {
            #region Cargo-UI Load
            foreach (Text go in cargoUI_GO.GetComponentsInChildren<Text>())
            {
                go.enabled = true;
            }
            foreach (RawImage go in cargoUI_GO.GetComponentsInChildren<RawImage>())
            {
                go.enabled = true;
            }
            foreach (Image go in cargoUI_GO.GetComponentsInChildren<Image>())
            {
                go.enabled = true;
            }
            #endregion
        }
        else
        {
            #region Cargo-UI Unload
            foreach (Text go in cargoUI_GO.GetComponentsInChildren<Text>())
            {
                go.enabled = false;
            }
            foreach (RawImage go in cargoUI_GO.GetComponentsInChildren<RawImage>())
            {
                go.enabled = false;
            }
            foreach (Image go in cargoUI_GO.GetComponentsInChildren<Image>())
            {
                go.enabled = false;
            }
            #endregion
        }
        showUI = !showUI;
    }
}
