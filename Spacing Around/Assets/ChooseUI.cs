using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChooseUI : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup optionsCanvas, askCanvas, backButton;
    [SerializeField]
    private Button noButton, yesButton;
    [SerializeField]
    TextMeshProUGUI questionTextObj;
    private bool setQuestion;

    //Reference
    public RotateObj myRotateobject;

    public bool SetQuestion
    {
        get => setQuestion; set => setQuestion = value;
    }

    private void OnEnable()
    {
        questionTextObj = GetComponentInChildren<TextMeshProUGUI>();
        CloseAskUI();
    }

    public void OpenAskUI(string questionText)
    {
        questionTextObj.text = questionText;
        askCanvas.alpha = 1;
        askCanvas.interactable = true;
        askCanvas.blocksRaycasts = true;

        optionsCanvas.alpha = 0.2f;
        optionsCanvas.interactable = false;
        optionsCanvas.blocksRaycasts = false;

        backButton.alpha = 0.2f;
        backButton.interactable = false;
        backButton.blocksRaycasts = false;

        StartCoroutine(WaitForResponse());
    }

    private void CloseAskUI()
    {
        optionsCanvas.alpha = 1;
        optionsCanvas.interactable = true;
        optionsCanvas.blocksRaycasts = true;

        backButton.alpha = 1f;
        backButton.interactable = true;
        backButton.blocksRaycasts = true;

        askCanvas.alpha = 0;
        askCanvas.interactable = false;
        askCanvas.blocksRaycasts = false;
    }

    public void YesNoButton(bool input)
    {
        SetQuestion = input;
        CloseAskUI();
    }

    IEnumerator WaitForResponse()
    {
        WaitForUIButtons waitForButton = new WaitForUIButtons(yesButton, noButton);
        myRotateobject.enabled = false; //Hard coded
        yield return waitForButton.Reset();
        if (waitForButton.PressedButton == yesButton)
        {
            // yes was pressed
            YesNoButton(true);
        }
        else
        {
            // no was pressed
            YesNoButton(false);
        }
        myRotateobject.enabled = true; //Hard coded
    }
}
