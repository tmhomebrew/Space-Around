using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    #region Fields
    public GameObject playerShipRef;
    public GameObject inventoryPanelRef;
    public List<GameObject> slotList;

    //ref
    public UIHandler myUIHandler;

    int inventorySize;
    int goldSize, kills, deaths;

    #endregion
    #region Properties
    public int InventorySize
    {
        get { return playerShipRef.GetComponent<ShipStats>().ShipCargoSpace; }
    }

    public int GoldSize
    {
        get => goldSize; set
        {
            goldSize = value;
            if (goldSize <= 0)
            {
                goldSize = 0;
            }
            myUIHandler.UpdateInformationPanel();
        }
    }
    public int Kills
    {
        get => kills; set
        {
            kills = value;
            myUIHandler.UpdateInformationPanel();
        }
    }
    public int Deaths
    {
        get => deaths; set
        {
            deaths = value;
            myUIHandler.UpdateInformationPanel();
        }
    }
    #endregion

    private void Awake()
    {
        myUIHandler = transform.root.GetComponentInChildren<UIHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        slotList = new List<GameObject>();
        InventorySlotsAvailable();
        GoldSize = 0;
        Kills = 0;
        Deaths = 0;
    }

    public void InventorySlotsAvailable()
    {
        foreach (RectTransform slot in inventoryPanelRef.GetComponentsInChildren<RectTransform>())
        {
            if (slot.name == "SlotText" || slot.name == "InventoryPanel")
            {
                continue;
            }
            if (!slotList.Contains(slot.gameObject))
            {
                slotList.Add(slot.gameObject);
            }
        }

        if (slotList.Count > 0)
        {
            for (int i = 0; i < slotList.Count; i++)
            {
                if (i < InventorySize)
                {
                    if (!slotList[i].activeSelf)
                    {
                        slotList[i].SetActive(true);
                    }
                    slotList[i].GetComponentInChildren<Text>().enabled = false;
                    slotList[i].GetComponentInChildren<Text>().text = (i + 1).ToString(); //Show name of object on this InventorySlot
                }
                else
                {
                    if (slotList[i].activeSelf)
                    {
                        slotList[i].GetComponentInChildren<Text>().enabled = false;
                        slotList[i].GetComponentInChildren<Text>().text = "Not Available";
                        slotList[i].SetActive(false);
                    }
                }
            }
        }
        else
        {

        }
    }
}
