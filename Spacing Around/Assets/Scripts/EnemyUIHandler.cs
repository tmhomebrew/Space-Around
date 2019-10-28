using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIHandler : MonoBehaviour
{
    #region Fields
    public EnemyShipStats shipStatsRef;

    //Health
    public GameObject _healthBar;
    private float healthVarEnemyRef;
    public GameObject _greenFill;

    //Shield
    public GameObject _shieldBar;
    private float shieldVarEnemyRef;
    public GameObject _backgroundBlue;
    public GameObject _blueFill;
    bool shieldUp;

    #endregion
    #region Properties
    public float HealthVarEnemyRef
    {
        get => healthVarEnemyRef; set
        {
            healthVarEnemyRef = value;
            _healthBar.GetComponent<Slider>().value = healthVarEnemyRef;
            UpdateHealthBarText();
        }
    }
    public float ShieldVarEnemyRef
    {
        get => shieldVarEnemyRef; set
        {
            shieldVarEnemyRef = value;
            if (shieldVarEnemyRef > 0 && !shieldUp)
            {
                shieldUp = true;
            }
            if (shieldVarEnemyRef <= 0)
            {
                shieldVarEnemyRef = 0;
                shieldUp = false;
            }
            _shieldBar.GetComponent<Slider>().value = shieldVarEnemyRef;
            UpdateHealthBarText();
        }
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if (shipStatsRef == null)
            GetComponent<EnemyShipStats>();

        //Ship-UI
        shieldUp = false;
        _healthBar.GetComponent<Slider>().maxValue = shipStatsRef.EnemyHealthMax;
        _healthBar.GetComponent<Slider>().value = shipStatsRef.EnemyHealthCur;
        _shieldBar.GetComponent<Slider>().maxValue = shipStatsRef.EnemyShieldMax;
        _shieldBar.GetComponent<Slider>().value = shipStatsRef.EnemyShieldMax;

        //Update
        UpdateHealthBarText();
    }

    public void UpdateHealthBarText()
    {
        #region Health
        if (_healthBar.GetComponent<Slider>().value >= 0 && _greenFill.GetComponent<Image>().enabled == false)
        {
            _greenFill.GetComponent<Image>().enabled = true;
        }
        if (_healthBar.GetComponentInChildren<Slider>().value == 0)
        {
            _greenFill.GetComponent<Image>().enabled = false;
        }
        #endregion

        #region Shield
        if (_shieldBar.GetComponent<Slider>().value >= 0 && _blueFill.GetComponent<Image>().enabled == false)
        {
            _blueFill.GetComponent<Image>().enabled = true;
            _backgroundBlue.GetComponent<Image>().enabled = true;
        }
        if (_shieldBar.GetComponentInChildren<Slider>().value == 0)
        {
            _blueFill.GetComponent<Image>().enabled = false;
            _backgroundBlue.GetComponent<Image>().enabled = false;
        }
        #endregion
    }
}
