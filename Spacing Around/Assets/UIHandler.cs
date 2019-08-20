using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    //Health
    public GameObject _healthBar;
    public GameObject _healthText;
    public GameObject _greenFill;
    string healthInText;
    //Shield
    public GameObject _shieldBar;
    public GameObject _shieldText;
    public GameObject _blueFill;
    string shieldInText;
    bool shieldUp;


    // Start is called before the first frame update
    void Start()
    {
        shieldUp = false;

        UpdateHealthBarText();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (_healthBar.GetComponentInChildren<Slider>().value == _healthBar.GetComponentInChildren<Slider>().maxValue)
            {
                if (!shieldUp)
                {
                    shieldUp = true;
                }
                _shieldBar.GetComponent<Slider>().value++;
            }
            _healthBar.GetComponentInChildren<Slider>().value++;
            UpdateHealthBarText();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (_healthBar.GetComponentInChildren<Slider>().value > 0 && !shieldUp)
            {
                _healthBar.GetComponentInChildren<Slider>().value--;
            }
            if (_shieldBar.GetComponent<Slider>().value > 0)
            {
                _shieldBar.GetComponent<Slider>().value--;
                if (_shieldBar.GetComponent<Slider>().value == 0)
                {
                    shieldUp = false;
                }
            }
            UpdateHealthBarText();
        }
    }

    public void UpdateHealthBarText()
    {
        //Health
        if (_healthBar.GetComponentInChildren<Slider>().value > 0 && _greenFill.GetComponent<Image>().enabled == false)
        {
            _greenFill.GetComponent<Image>().enabled = true;
        }
        if(_healthBar.GetComponentInChildren<Slider>().value == 0)
        {
            _greenFill.GetComponent<Image>().enabled = false;
        }
        if (_healthBar.GetComponentInChildren<Slider>().value == _healthBar.GetComponentInChildren<Slider>().maxValue)
        {
            //shieldUp = true;
        }
        healthInText = _healthBar.GetComponentInChildren<Slider>().value.ToString();
        _healthText.GetComponent<Text>().text = "Health: " + healthInText + "/" + _healthBar.GetComponentInChildren<Slider>().maxValue;

        //Shield
        if (_shieldBar.GetComponentInChildren<Slider>().value > 0 && _blueFill.GetComponent<Image>().enabled == false)
        {
            _blueFill.GetComponent<Image>().enabled = true;
            //shieldUp = true;
        }
        if (_shieldBar.GetComponentInChildren<Slider>().value == 0)
        {
            _blueFill.GetComponent<Image>().enabled = false;
            //shieldUp = false;
        }
        shieldInText = _shieldBar.GetComponentInChildren<Slider>().value.ToString();
        _shieldText.GetComponent<Text>().text = "Shield: " + shieldInText + "/" + _shieldBar.GetComponentInChildren<Slider>().maxValue;

    }
}
