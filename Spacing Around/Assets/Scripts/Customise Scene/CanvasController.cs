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

    [SerializeField]
    private bool changeScene;

    public bool ChangeScene { get => changeScene; set => changeScene = value; }

    #endregion

    private void Awake()
    {
        myPES = GetComponentInChildren<PresetEditorScript>();
        mySS = GetComponentInChildren<ShipSelector>();

        ChangeScene = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        PresetRoomEditor.enabled = false;
        ShipRoomSelector.enabled = true;
    }

    public void ChangeCanvas()
    {
        if (!ChangeScene)
        {

        }
        else
        {
            PresetRoomEditor.enabled = !PresetRoomEditor.enabled;
            ShipRoomSelector.enabled = !ShipRoomSelector.enabled;
            if (PresetRoomEditor.enabled)
            {
                SetShip();
            }
        }
    }

    void SetShip()
    {
        myPES.ShipHolder = mySS.SelectedShip;
    }

    public void ChangeSceneSafety(bool value)
    {
        ChangeScene = value;
        ChangeCanvas();
    }
}