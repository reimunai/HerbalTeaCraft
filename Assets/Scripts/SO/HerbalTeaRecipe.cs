using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HerbalTeaSO/HerbalTeaRecipe", fileName = "HerbalTeaRecipe")]
public class HerbalTeaRecipe : ScriptableObject
{
    public string herbalTeaName;
    public List<IngredientsAndWeighs> ingredientsAndWeighs;
    public float qualityColor = 0;
    
    
    //成品评分
    public float Evaluate(BrewingPot pot)
    {
        return 100f;
    }
}
