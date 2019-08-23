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
                foreach (Image go in cargoUI_GO.GetComponentsInChildren<Image>())
                {
                    tempList.Add(go);
                    go.enabled = true;
                }
                foreach (RawImage go in cargoUI_GO.GetComponentsInChildren<RawImage>())
                {
                    tempList.Add(go);
                    go.enabled = true;
                }
                showUI = true;
                foreach (object o in tempList)
                {
                    print(o);
                }
                //cargoUI_GO.SetActive(true);
            }
            else
            {
                foreach (RawImage go in cargoUI_GO.GetComponentsInChildren<RawImage>())
                {
                    go.enabled = false;
                }
                showUI = false;
                //cargoUI_GO.SetActive(false);
            }
        }
    }
}
