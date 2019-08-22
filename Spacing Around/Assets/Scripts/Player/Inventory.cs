using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject playerShipRef;
    public GameObject inventoryPanelRef;
    public List<GameObject> slotList;

    int inventorySize;

    public int InventorySize
    {
        get { return playerShipRef.GetComponent<ShipStats>().ShipCargoSpace; }
    }

    // Start is called before the first frame update
    void Start()
    {
        slotList = new List<GameObject>();
        InventorySlotsAvailable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InventorySlotsAvailable()
    {
        foreach (RectTransform slot in inventoryPanelRef.GetComponentsInChildren<RectTransform>())
        {
            if (slot.name == "SlotText" || slot.name == "InventoryPanel")
            {
                continue;
            }
            slotList.Add(slot.gameObject);
            //print("Slot added: " + slot.name);
        }

        //print("Slotlist Count: " + slotList.Count);
        //print("Inventory Size: " + InventorySize);
        for (int i = 0; i < slotList.Count; i++)
        {
            if (i < InventorySize)
            {
                slotList[i].GetComponentInChildren<Text>().enabled = true;
            }
            else
            {
                slotList[i].GetComponentInChildren<Text>().enabled = true;
                slotList[i].GetComponentInChildren<Text>().text = "Not Available";
            }
        }

    }
}
