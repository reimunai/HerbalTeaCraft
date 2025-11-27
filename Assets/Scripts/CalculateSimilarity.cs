using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CalculateSimilarity : MonoBehaviour
{
    public List<HerbalTeaRecipe> allHerbalTeaRecipe = new List<HerbalTeaRecipe>();
    public HUDControl HUDControl;


    private List<float> Similaritys = new List<float>();
    private List<Dictionary<Ingredient, float>> standards = new List<Dictionary<Ingredient, float>>();

    private void Start()
    {
        InitStandard();
    }

    private void InitStandard()
    {
        foreach (var HerbalTeaRecipe in allHerbalTeaRecipe)
        {
            if(HerbalTeaRecipe != null)
            Debug.Log("添加配方" + HerbalTeaRecipe.name);
            AddToDictonaryList(HerbalTeaRecipe);
        }
    }

    private void AddToDictonaryList(HerbalTeaRecipe HerbalTeaRecipe)
    {
        if (HerbalTeaRecipe != null)
        {
            Dictionary<Ingredient, float> temp = new Dictionary<Ingredient, float>();
            foreach (var ingredientNweight in HerbalTeaRecipe.ingredientsAndWeighs)
            {
                if (ingredientNweight.ingredient == null || ingredientNweight.weight <= 0f)
                    continue;
                temp.Add(ingredientNweight.ingredient, ingredientNweight.weight);
                Debug.Log("配方里有"+ingredientNweight.ingredient.ingredientName+"重量为"+ ingredientNweight.weight);
            }
            standards.Add(temp);
            Debug.Log("Standards里有 Keys: " + string.Join(", ", temp.Keys) + " 和 Values: " + string.Join(", ", temp.Values));
        } 
    }



    //评分系统
    public float CalculateStandardBasedSimilarity(Dictionary<Ingredient, float> target, Dictionary<Ingredient, float> standard)
    {
        if (standard.Count == 0)
        {
            Debug.Log("没有配方");
            return 0f; 
        }

        float totalSimilarity = 0f;
        int comparedCount = 0;

        // 只比较标准字典中存在的材料
        foreach (var standardItem in standard)
        {
            Ingredient ingredient = standardItem.Key;
            float standardWeight = standardItem.Value;

            if (target.ContainsKey(ingredient))
            {
                Debug.Log("Target里有" + ingredient.name);
                float targetWeight = target[ingredient];
                float ratio = standardWeight > 0 ? targetWeight / standardWeight : 0f;

                // 计算单个材料的相似度（比例越接近1，相似度越高）
                float similarity = 1f - Mathf.Min(1f, Mathf.Abs(1f - ratio));
                totalSimilarity += similarity;
            }
            else
            {
                // 标准材料在目标中不存在，相似度为0
                Debug.Log("Target里没有" + ingredient.name);
                totalSimilarity += 0f;
            }

            comparedCount++;
        }

        // 惩罚额外材料
        float extraMaterialPenalty = CalculateExtraMaterialPenalty(target, standard);

        float baseSimilarity = comparedCount > 0 ? totalSimilarity / comparedCount : 0f;
        float finalSimilarity = baseSimilarity * extraMaterialPenalty;

        return finalSimilarity;
    }

    private float CalculateExtraMaterialPenalty(Dictionary<Ingredient, float> target, Dictionary<Ingredient, float> standard)
    {
        // 计算额外材料的数量
        int extraMaterials = target.Keys.Except(standard.Keys).Count();

        if (extraMaterials == 0) return 1f; // 没有额外材料，不惩罚

        // 惩罚系数：额外材料越多，惩罚越大
        float penalty = Mathf.Pow(0.8f, extraMaterials); // 每个额外材料乘以0.8
        return Mathf.Max(0.1f, penalty); // 最低保持10%的相似度
    }


    public void CalculateWithAll(Dictionary<Ingredient, float> target)
    {
        foreach (var standard in standards)
        {
            if (standard != null)
            { var Similarity = CalculateStandardBasedSimilarity(target, standard);
                Similaritys.Add(Similarity);
                Debug.Log(Similarity);
            }
        }
        if (Similaritys.Count > 0)
        {
            Debug.Log(allHerbalTeaRecipe[FindMaxIndex(Similaritys)].herbalTeaName);
            Debug.Log(Similaritys[FindMaxIndex(Similaritys)]);
            HUDControl.ShowScoreText(Similaritys[FindMaxIndex(Similaritys)]);
            
        }
    }
    private int FindMaxIndex<T>(List<T> list) where T : IComparable<T>
    {
        if (list == null || list.Count == 0)
            return -1;

        int maxIndex = 0;
        T maxValue = list[0];

        for (int i = 1; i < list.Count; i++)
        {
            if (list[i].CompareTo(maxValue) > 0)
            {
                maxValue = list[i];
                maxIndex = i;
            }
        }

        return maxIndex;
    }
}
