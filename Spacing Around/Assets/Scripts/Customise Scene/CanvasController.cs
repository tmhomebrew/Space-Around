using System.Collections;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField]
    private Canvas ShipSelector, PresetEditor;

    // Start is called before the first frame update
    void Start()
    {
        PresetEditor.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}