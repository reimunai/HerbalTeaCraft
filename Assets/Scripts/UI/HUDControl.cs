using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class HUDControl : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text Score;
    public TMP_Text Simlarity;
    public TMP_Text Name;
    public TMP_Text NothingInPot;
    public HerbalTeaRecipe startRecipe;
    public HerbalInfoUI herbalInfo;
    public StepGradeUIControl gradeControl;
    private void Awake()
    {
        Score.enabled = false;
        Simlarity.enabled = false;
        Name.enabled = false;
        NothingInPot.enabled = false;
    }

    private void Start()
    {
        StartCoroutine(gradeControl.ShowStepGrageUIInfoMultiply(new int[] { 0, 1 }));

    }

    public void ShowStepGrade(int stepNumber)
    {
        gradeControl.ShowStepGradeUIInfo(stepNumber);
    }

    public void ShowScoreText(float score)
    {
        Score.enabled = true;
        Score.text = score.ToString(CultureInfo.InvariantCulture);
    }

    public void HideScoreText()
    {
        Score.enabled = false;
    }

    public void ShowSimlarityNNameText(float simlarity, string name)
    {
        Simlarity.enabled = true;
        Simlarity.text = simlarity.ToString(CultureInfo.InvariantCulture);
        Name.enabled = true;
        Name.text = name;
    }
    public void HideSimlarityNNameText()
    {
        Simlarity.enabled = false;
        Name.enabled = false;
    }

    public void ShowNothingIn()
    {
        NothingInPot.enabled = true;
    }
    public void HideNothinIn()
    {
        NothingInPot.enabled = false;
    }
    public void ShowHerbalInfo(string herbal, string info)
    {
        herbalInfo.SetAndShow(herbal, info);
    }

    public void HideHerbalInfo()
    {
        herbalInfo.TextDisable();
    }
    public void ClearAll()
    {
        HideHerbalInfo();
        HideNothinIn();
        HideScoreText();
        HideSimlarityNNameText();
    }
}
