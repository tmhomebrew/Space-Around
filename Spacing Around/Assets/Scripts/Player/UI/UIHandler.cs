using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public ShipStats shipStatsRef;
    //Health
    public GameObject _healthBar;
    private float healthVarPlayerRef;
    public GameObject _healthText;
    public GameObject _greenFill;
    string healthInText;

    //Shield
    public GameObject _shieldBar;
    private float shieldVarPlayerRef;
    public GameObject _shieldText;
    public GameObject _blueFill;
    string shieldInText;
    bool shieldUp;

    //Information Panel
    public GameObject _goldGO, _killsGO, _deathsGO;
    private int goldRef, killRef, deathRef;

    public float HealthVarPlayerRef
    {
        get
        {
            return healthVarPlayerRef;
        }

        set
        {
            healthVarPlayerRef = value;
            _healthBar.GetComponent<Slider>().value = healthVarPlayerRef;
            UpdateHealthBarText();
        }
    }
    public float ShieldVarPlayerRef
    {
        get
        {
            return shieldVarPlayerRef;
        }

        set
        {
            shieldVarPlayerRef = value;
            if (shieldVarPlayerRef > 0 && !shieldUp)
            {
                shieldUp = true;
            }
            if (shieldVarPlayerRef == 0)
            {
                shieldUp = false;
            }
            _shieldBar.GetComponent<Slider>().value = shieldVarPlayerRef;
            UpdateHealthBarText();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        //Ship-UI
        shieldUp = false;
        _healthBar.GetComponent<Slider>().maxValue = shipStatsRef.ShipMainBodyHealthMax;
        _healthBar.GetComponent<Slider>().value = shipStatsRef.ShipMainBodyHealthCur;
        _shieldBar.GetComponent<Slider>().maxValue = shipStatsRef.ShipShieldMax;
        _shieldBar.GetComponent<Slider>().value = shipStatsRef.ShipShieldCur;

        //Cargo-UI
        goldRef = shipStatsRef.ShipInventory.GoldSize;
        killRef = shipStatsRef.ShipInventory.Kills;
        deathRef = shipStatsRef.ShipInventory.Deaths;

        //Update
        UpdateHealthBarText();
        UpdateInformationPanel();
    }

    public void UpdateHealthBarText()
    {
        #region Health
        if (_healthBar.GetComponent<Slider>().value >= 0 && _greenFill.GetComponent<Image>().enabled == false)
        {
            _greenFill.GetComponent<Image>().enabled = true;
        }
        if(_healthBar.GetComponentInChildren<Slider>().value == 0)
        {
            _greenFill.GetComponent<Image>().enabled = false;
        }
        healthInText = _healthBar.GetComponent<Slider>().value.ToString();
        _healthText.GetComponent<Text>().text = "Health: " + healthInText + "/" + _healthBar.GetComponent<Slider>().maxValue;
        #endregion

        #region Shield
        if (_shieldBar.GetComponent<Slider>().value >= 0 && _blueFill.GetComponent<Image>().enabled == false)
        {
            _blueFill.GetComponent<Image>().enabled = true;
        }
        if (_shieldBar.GetComponentInChildren<Slider>().value == 0)
        {
            _blueFill.GetComponent<Image>().enabled = false;
        }
        shieldInText = _shieldBar.GetComponent<Slider>().value.ToString();
        _shieldText.GetComponent<Text>().text = "Shield: " + shieldInText + "/" + _shieldBar.GetComponentInChildren<Slider>().maxValue;
        #endregion
    }

    public void UpdateInformationPanel()
    {
        //_goldGO.GetComponent<Text>().text = "Gold: " + goldRef;
        //_killsGO.GetComponent<Text>().text = "Kills: " + killRef;
        //_deathsGO.GetComponent<Text>().text = "Deaths: " + deathRef;

        _goldGO.GetComponent<Text>().text = "Gold: " + shipStatsRef.ShipInventory.GoldSize;
        _killsGO.GetComponent<Text>().text = "Kills: " + shipStatsRef.ShipInventory.Kills;
        _deathsGO.GetComponent<Text>().text = "Deaths: " + shipStatsRef.ShipInventory.Deaths;
    }
}
