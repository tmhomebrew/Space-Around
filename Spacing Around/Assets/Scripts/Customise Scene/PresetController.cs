using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PresetController : MonoBehaviour
{
    [SerializeField]
    GameObject selectedShip, refShipSelector;
    List<Button> selectButtons = new List<Button>();
    List<GameObject> presetObjs = new List<GameObject>();

    private void Awake()
    {
        //foreach (Button b in refShipSelector.GetComponentsInChildren<Button>())
        //{
        //    selectButtons.Add(b);
        //}

        //presetObjs.Add(selectedShip);
    }

    void PresetsAvailable()
    {
        //foreach (GameObject go in presetObjs)
        //{
        //    if (go == null)
        //    {

        //    }
        //}
    }

    void PresetsText()
    {

    }
}