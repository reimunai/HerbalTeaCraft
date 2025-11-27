using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StepGradeUIControl : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float aliveTimePerChar = 0.2f;
    public List<string> stepsGradeInfo = new List<string>();

    public void ShowStepGradeUIInfo(int step)
    {
        if (step < 0 || step >= stepsGradeInfo.Count)
        {
            return;
        }
        text.text = stepsGradeInfo[step];
        StartCoroutine(HideStepGradeUI(stepsGradeInfo[step].Length * aliveTimePerChar));
    }

    public IEnumerator ShowStepGrageUIInfoMultiply(int[] steps)
    {
        foreach (var s in steps)
        {
            if (s < 0 || s >= steps.Length)
            {
                continue;
            }

            text.text = stepsGradeInfo[s];
            yield return StartCoroutine(HideStepGradeUI(stepsGradeInfo[s].Length * aliveTimePerChar));
        }
    }
    public IEnumerator HideStepGradeUI(float t)
    {
        yield return new WaitForSeconds(t);
        text.text = "";
    }
}
