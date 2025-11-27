using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HerbalInfoUI : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI herbalName;
    public TextMeshProUGUI info;
    public Image image;

    private void Awake()
    {
        TextEnable();
    }

    public void TextEnable()
    {
        herbalName.enabled = !herbalName.enabled;
        info.enabled = !info.enabled;
        image.enabled = !image.enabled;
    }

    public void SetAndShow(string herbalNameString, string herbalInfo)
    {
        this.herbalName.text = herbalNameString;
        this.info.text = herbalInfo;
        TextEnable();
    }
}
