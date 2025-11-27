using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class HUDControl : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text text;
    public HerbalTeaRecipe startRecipe;
    public HerbalInfoUI herbalInfo;
    public StepGradeUIControl gradeControl;
    private void Awake()
    {
        text.enabled = false;
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
        text.enabled = true;
        text.text = score.ToString(CultureInfo.InvariantCulture);
    }

    public void HideScoreText()
    {
        text.enabled = false;
    }

    public void ShowHerbalInfo(string herbal, string info)
    {
        herbalInfo.SetAndShow(herbal, info);
    }

    public void HideHerbalInfo()
    {
        herbalInfo.TextEnable();
    }
}
