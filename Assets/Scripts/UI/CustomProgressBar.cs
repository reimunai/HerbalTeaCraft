using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomProgressBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float minValue = 0f;
    [SerializeField] private float maxValue = 100f;
    [SerializeField]private RectTransform rectTransform;
    [SerializeField]private RectTransform progressBar;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    public void SetProgress(float progress)
    {
        var height = rectTransform.sizeDelta.y * (progress - minValue) / (maxValue - minValue);
        progressBar.sizeDelta = new Vector2(progressBar.sizeDelta.x, height);
    }
}
