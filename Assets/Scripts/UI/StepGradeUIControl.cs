using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StepGradeUIControl : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float aliveTime;
    public List<string> stepsGradeInfo = new List<string>();

    private void Start()
    {
        ShowStepGradeUIInfo(0);
        
    }

    public void ShowStepGradeUIInfo(int step)
    {
        if (step < 0 || step >= stepsGradeInfo.Count)
        {
            return;
        }
        text.text = stepsGradeInfo[step];
        StartCoroutine(HideStepGradeUI());
    }

    IEnumerator HideStepGradeUI()
    {
        yield return new WaitForSeconds(aliveTime);
        text.text = "";
    }
}
