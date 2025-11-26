using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//配料
[CreateAssetMenu(menuName = "HerbalTeaSO/Ingredient", fileName = "Ingredient")]
public class Ingredient : ScriptableObject
{
    public string ingredientName;
    public string[] tags;

    public Ingredient(string name)
    {
        ingredientName = name;
    }
}
