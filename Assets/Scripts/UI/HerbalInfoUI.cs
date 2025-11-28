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
        TextDisable();
    }

    public void  TextEnable()
    {
        herbalName.enabled = true;
        info.enabled = true;
        image.enabled = true;
    }
    public void TextDisable()
    {
        herbalName.enabled=false;
        info.enabled = false;
        image.enabled = false;
    }

    public void SetAndShow(string herbalNameString, string herbalInfo)
    {
        this.herbalName.text = herbalNameString;
        this.info.text = herbalInfo;
        TextEnable();
    }
}
