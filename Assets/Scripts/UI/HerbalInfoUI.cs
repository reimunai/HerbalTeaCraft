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
    
    [SerializeField]private Sprite deaultSprite;
    private Image _selfImage;
    
    private void Awake()
    {
        _selfImage = GetComponent<Image>();
        TextDisable();
    }

    public void  TextEnable()
    {
        _selfImage.enabled = true;
        herbalName.enabled = true;
        info.enabled = true;
        image.enabled = true;
    }
    public void TextDisable()
    {
        _selfImage.enabled = false;
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

    public void SetAndShow(Ingredient ingredient)
    {
        this.herbalName.text = ingredient.name;
        this.info.text = ingredient.description;
        if (ingredient.ingredientSprite != null)
        {
            this.image.sprite = ingredient.ingredientSprite;
        }
        else
        {
            this.image.sprite = deaultSprite;
        }
        
        TextEnable();
    }
}
