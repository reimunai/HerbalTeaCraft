using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
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

[Serializable]
public class BrewingPot
{
    [Header("锅中混合物实时属性")]
    public float capacity;
    [Range(0, 1f)]public float waterLevel;
    [Range(0, 1f)] public float boilLevel;
    [Range(0, 200f)]public float temperature;
    public Dictionary<Ingredient, float> ingredientsAndWeighs = new Dictionary<Ingredient, float>();
    
    //1为完美，超过1为过火
    [Range(0, 2f)]public float qualityColor = 0;
    public bool isHeating = false;
    public bool isBoiling = false;
    
    //Range 0, 1.5
    [Header("消耗时间相关")]
    public float costTime = 30f;
    public float timer = 0f;
    
    [Header(("上限相关"))]
    public float maxCapacity = 100f;
    public float maxCostTime = 120f;
    
    [Header("各属性对消耗时间的影响")]
    public float waterFactorToCost = 10f;
    public float ingredientsVaryFactorToCost = 0.001f;
    public float weightFactorToCost = 0.001f;

    [Header("烹煮相关速率")] 
    public float boilingBigFireQualityK = 4.0f;
    public float boilingSmallFireQualityK = 1.0f;
    public float maxBoilIncrementK = 2.5f;

    [Header("相关事件")] public UnityEvent onBoiling;

    public void AddIngredient(Ingredient ingredient, float weight)
    {
        if (capacity > maxCapacity)
        {
            Debug.LogWarning("can't add more than max capacity");
            return;
        }

        if (ingredientsAndWeighs.ContainsKey(ingredient))
        {
            ingredientsAndWeighs[ingredient] += weight;
            costTime += ingredientsAndWeighs[ingredient] * weightFactorToCost;
        }
        else
        {
            ingredientsAndWeighs.Add(ingredient, weight);
            costTime += ingredientsVaryFactorToCost;
        }

        if (costTime >= maxCostTime)
        {
            costTime = maxCostTime;
        }
        capacity += weight;
    }

    public void AddWater(float water)
    {
        waterLevel += water;
        if (waterLevel >= 1.0f)
        {
            waterLevel = 1.0f;
        }
    }
    
    public void StartHeating()
    {
        if (waterLevel <= 0.05f || isHeating)
        {
            Debug.LogWarning("can't start heating cause low water level or already heating");
            return;
        }
        //小火
        this.temperature = 30f;
        isHeating = true;
        costTime += waterLevel * waterFactorToCost;
    }

    public void AdjustTemperature(float temperature)
    {
        this.temperature = temperature;
    }
    public void UpdateQualityColor(float deltaTime)
    {
        if(!isHeating)
            return;
        
        //quality boil 进度条上升斜率
        float qualityK = temperature * 0.85f / 200f;







        float boilK = temperature * maxBoilIncrementK / 200f;
        
        
        timer += deltaTime;
        boilLevel += boilK * deltaTime / costTime;
        
        //沸腾判断
        if (boilLevel >= 1.0f)
        {
            boilLevel = 1.0f;
            if (isBoiling == false)
            {
                onBoiling?.Invoke();
                Debug.Log("TMD 沸腾了！ 转小火！");
                isBoiling = true;
            }
        }

        if (isBoiling)
        {
            if (temperature > 170f)
            {
                qualityK = boilingBigFireQualityK;
            }
            else if (temperature < 100f && temperature > 60f)
            {
                qualityK = boilingSmallFireQualityK;
            }
        }
        
        qualityColor += qualityK * deltaTime / costTime;
    }
    
    public void ClearBrewingPots()
    {
        ingredientsAndWeighs.Clear();
        capacity = 0;
        waterLevel = 0;
        qualityColor = 0;
        costTime = 10f;
    }

    public float GetBestWaterLevel()
    {
        return capacity / maxCapacity;
    }
}
[Serializable]
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

[Serializable]
public class IngredientsAndWeighs
{
    public Ingredient ingredient;
    public float weight;
}

public class IngredientsAndOtherCls : MonoBehaviour
{
    public Ingredient ingredient;
    public HerbalTeaRecipe recipe;
    public BrewingPot pot;

    public Ingredient herball1;
    public Ingredient herball2;
    public Ingredient herball3;
    
    private void Awake()
    {
        pot.AddIngredient(herball1, 5);
        pot.AddIngredient(herball2, 5);
        pot.AddIngredient(herball3, 5);
        foreach (var pair in pot.ingredientsAndWeighs)
        {
            Debug.Log(pair.Key.name + " : " + pair.Value);
        }
        
        
    }

    private void Update()
    {
        pot.UpdateQualityColor(Time.deltaTime);
    }
}