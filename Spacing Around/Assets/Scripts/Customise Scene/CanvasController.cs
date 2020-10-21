using System.Collections;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    #region Fields
    //Refs
    [SerializeField]
    PresetEditorScript myPES;
    [SerializeField]
    ShipSelector mySS;
    
    [SerializeField]
    private Canvas ShipRoomSelector, PresetRoomEditor;
    public CanvasGroup ShipRoomGroup, PresetRoomGroup;

    [SerializeField]
    private bool changeScene;

    public bool ChangeScene
    {
        get => changeScene; set
        {
            changeScene = value;
            ChangeCanvas(value);
        }
    }

    public ShipSelector MySS { get => mySS; set => mySS = value; }

    #endregion

    private void Awake()
    {
        myPES = GetComponentInChildren<PresetEditorScript>();
        MySS = GetComponentInChildren<ShipSelector>();

        ChangeScene = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        PresetRoomEditor.enabled = false;
        ShipRoomSelector.enabled = true;
    }

    public void ChangeCanvas(bool input)
    {
        if (input)
        {
            PresetRoomEditor.enabled = !PresetRoomEditor.enabled;
            PresetRoomGroup.interactable = PresetRoomEditor.enabled;
            ShipRoomSelector.enabled = !ShipRoomSelector.enabled;
            ShipRoomGroup.enabled = ShipRoomSelector.enabled;
            if (PresetRoomEditor.enabled)
            {
                SetShip();
            }
            else
            {
                SaveShipAndReturn();
            }
        }
    }

    void SetShip()
    {
        MySS.SelectedShip.transform.SetParent(myPES.ShipHolder.transform);
        MySS.SelectedShip.transform.SetAsFirstSibling();
        MySS.SelectedShip.transform.position = myPES.ShipHolder.transform.position;
        MySS.SelectedShip.transform.rotation = myPES.ShipHolder.transform.rotation;

        myPES.OnOpenShipEditor();
    }

    void SaveShipAndReturn()
    {
        myPES.ShipHolder.transform.GetChild(0).GetComponent<Renderer>().enabled = true;
        myPES.ShipHolder.transform.GetChild(0).transform.SetParent(MySS.PlacementList[2].transform);
        MySS.PlacementList[2].transform.GetChild(0).transform.position = MySS.PlacementList[2].transform.position;
        MySS.PlacementList[2].transform.GetChild(0).transform.rotation = MySS.PlacementList[2].transform.rotation;
    }
}