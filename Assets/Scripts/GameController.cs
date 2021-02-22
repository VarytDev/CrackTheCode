using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using static GameLogic;

public class GameController : MonoBehaviour
{
    private string code;
    private string rightCode;

    public GameObject codeDisplay, customEndPanel, campEndPanel;

    private GameObject endPanel;
    private GameObject[] slots;
    private int currentSlot, codeLength;
    private bool trackHistory, campaign;

    private int correct, perfect;
    public TextMeshProUGUI currentText, fullText, attemptsDisplay;
    private int attempts;

    #region Initialize and Update

    void Start()
    {
        //define settings
        attempts = GameSettings.Instance.gameplaySettings.attempts;
        codeLength = (int)GameSettings.Instance.gameplaySettings.codeLength;
        trackHistory = GameSettings.Instance.gameplaySettings.trackHistory;
        campaign = GameSettings.Instance.gameplaySettings.campaign;

        slots = new GameObject[codeLength];

        //code generation
        if(GameSettings.Instance.gameplaySettings.seedGeneration) rightCode = CodeGenerator.Generate(GameSettings.Instance.gameplaySettings.seed);
        else rightCode = CodeGenerator.Generate();

        string overrideCode = GameSettings.Instance.gameplaySettings.overrideCode;
        if(overrideCode != "") rightCode = overrideCode;

        //initialization        
        InitDisplay();
        UpdateAttempts(attempts);

        //scene mode branch
        if(campaign)
        {
            endPanel = campEndPanel;

            string overrideEntry = GameSettings.Instance.gameplaySettings.overrideEntry;
            fullText.text = overrideEntry;

            TestValue(GameSettings.Instance.gameplaySettings.startingCodes);
        } 
        else
        {
            endPanel = customEndPanel;
        } 
        customEndPanel.SetActive(false);
        campEndPanel.SetActive(false);
    }

    void InitDisplay()
    {
        int length = Enum.GetValues(typeof(GameplaySettings.CodeLength)).Length + 2;

        for(int i = 0; i < length; i++)
        {
            GameObject slot = codeDisplay.transform.GetChild(i).gameObject;

            if(i < codeLength) slots[i] = slot;
            else slot.SetActive(false);
        }
    }

    #endregion

    #region Buttons

    public void AddNumber(int amount)
    {
        if(currentSlot >= codeLength) return;

        string number = amount.ToString();

        slots[currentSlot].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = number;
        currentSlot++;
    }

    public void RemoveNumber()
    {
        if(currentSlot <= 0) return;

        currentSlot--;
        slots[currentSlot].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
    }

    public void Enter()
    {
        if(attempts <= 0) return;

        attempts--;

        if(attempts <= 0) Lost();

        Check();
    }

    public void Surrender()
    {
        UpdateAttempts(0);
        Lost();
    }

    #endregion

    #region Functions

    void GenerateHints()
    {
        //code that generates random anwswers with at least 1 correctt and no more than 2
    }

    void Check()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            code += slots[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        }

        if(code == rightCode) Win();
            
        Answer result = GameLogic.CompareStrings(code, rightCode, campaign);
        correct += result.correct;
        perfect += result.perfect;

        PrintEntry();
        Clear();
        
        UpdateAttempts(attempts);
    }

    void TestValue(string[] codes)
    {
        trackHistory = true;
        for(int i = 0; i < codes.Length; i++)
        {
            code = codes[i];
            Check();
        }
        trackHistory = false;
        code = "";
        Check();
        trackHistory = GameSettings.Instance.gameplaySettings.trackHistory;
    }

    void UpdateAttempts(int newAttempts)
    {
        attempts = newAttempts;
        attemptsDisplay.text = "Attempts: " + attempts;
    }

    void PrintEntry()
    {
        string entry = "Code: " + code + "\n \n" + perfect + String.Numbers(perfect) + "correct and placed well." + 
        "\n \n" + correct + String.Numbers(correct) + "correct and placed wrong.";

        currentText.text = entry;
        if(trackHistory)
        {
            fullText.text += entry + String.Space();
            fullText.transform.parent.GetComponent<UnityEngine.UI.ScrollRect>().verticalNormalizedPosition = 0;
        } 
    }

    void Clear()
    {
        foreach(GameObject slot in slots)
        {
            slot.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        }

        currentSlot = 0;

        correct = 0;
        perfect = 0;

        code = "";
    }

    void Win()
    {
        if(campaign)
        {
            //AddStatus(true);
        }

        print("You won, code: " + code);
        endPanel.SetActive(true);
        endPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "You won, code: " + code;
        UpdateAttempts(0);
    }

    void Lost()
    {
        string displayText;

        if(campaign)
        {
            //AddStatus(false);
            displayText = "You lost!";
        } 
        else displayText = "You lost, correct code was: " + rightCode;
        
        endPanel.SetActive(true);
        endPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = displayText;
    }

    void AddStatus(bool levelStatus)
    {
        LevelStatus status = GameSettings.LoadLevelStatus();
        int level = GameSettings.Instance.gameplaySettings.level;
        status.currentLevel = level + 1;
        status.levelProgression.Add(levelStatus);
        GameSettings.Instance.levelStatus = status;
        GameSettings.SaveLevelStatus();
    }

    #endregion
}
