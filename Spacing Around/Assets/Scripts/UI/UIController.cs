using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject cargoUI_GO;
    public GameObject statusBarUI_GO;
    private bool showUI;
    
    public List<object> tempList;
    // Start is called before the first frame update
    void Start()
    {
        tempList = new List<object>();
        showUI = true;
    }

    // Update is called once per frame
    void Update()
    {
        Controller();
    }

    void Controller()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            /* Note <---------------------------------------------------------------------
             * Disabler ikke de første childs i rækken, men kun grand-childs.. hmmm...
             */
            if (!showUI)
            {
                #region Cargo-UI Load
                foreach (Image go in cargoUI_GO.GetComponentsInChildren<Image>())
                {
                    go.enabled = true;
                }
                foreach (RawImage go in cargoUI_GO.GetComponentsInChildren<RawImage>())
                {
                    go.enabled = true;
                }
                foreach (Text go in cargoUI_GO.GetComponentsInChildren<Text>())
                {
                    go.enabled = true;
                }
                #endregion
                showUI = true;
            }
            else
            {
                #region Cargo-UI Unload
                foreach (RawImage go in cargoUI_GO.GetComponentsInChildren<RawImage>())
                {
                    go.enabled = false;
                }
                foreach (Image go in cargoUI_GO.GetComponentsInChildren<Image>())
                {
                    go.enabled = false;
                }
                foreach (Text go in cargoUI_GO.GetComponentsInChildren<Text>())
                {
                    go.enabled = false;
                }
                #endregion
                showUI = false;
            }
        }
    }
}
