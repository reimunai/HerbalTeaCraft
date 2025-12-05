using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CalculateSimilarity : MonoBehaviour
{
    public List<HerbalTeaRecipe> allHerbalTeaRecipe = new List<HerbalTeaRecipe>();
    public HUDControl HUDControl;
    public bool isDone=false;

    private List<float> Similaritys = new List<float>();
    private List<Dictionary<Ingredient, float>> standards = new List<Dictionary<Ingredient, float>>();

    private void Start()
    {
        InitStandard();
        HUDControl = FindFirstObjectByType<HUDControl>();
    }

    private void InitStandard()
    {
        foreach (var HerbalTeaRecipe in allHerbalTeaRecipe)
        {
            if (HerbalTeaRecipe != null)
            {
                Debug.Log("�����䷽" + HerbalTeaRecipe.herbalTeaName);
                AddToDictonaryList(HerbalTeaRecipe);
            }
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
                Debug.Log("�䷽���� "+ingredientNweight.ingredient.ingredientName+" ����Ϊ "+ ingredientNweight.weight);
            }
            standards.Add(temp);
            Debug.Log("Standards���� Keys: " + string.Join(", ", temp.Keys) + " �� Values: " + string.Join(", ", temp.Values));
        } 
    }



    //����ϵͳ
    public float CalculateStandardBasedSimilarity(Dictionary<Ingredient, float> target, Dictionary<Ingredient, float> standard)
    {
        if (standard.Count == 0)
        {
            Debug.Log("û���䷽");
            return 0f; 
        }

        float totalSimilarity = 0f;
        int comparedCount = 0;

        // ֻ�Ƚϱ�׼�ֵ��д��ڵĲ���
        foreach (var standardItem in standard)
        {
            Ingredient ingredient = standardItem.Key;
            float standardWeight = standardItem.Value;

            if (target.ContainsKey(ingredient))
            {
                Debug.Log("Target����" + ingredient.name);
                float targetWeight = target[ingredient];
                float ratio = standardWeight > 0 ? targetWeight / standardWeight : 0f;

                // ���㵥�����ϵ����ƶȣ�����Խ�ӽ�1�����ƶ�Խ�ߣ�
                float similarity = 1f - Mathf.Min(1f, Mathf.Abs(1f - ratio));
                totalSimilarity += similarity;
            }
            else
            {
                // ��׼������Ŀ���в����ڣ����ƶ�Ϊ0
                Debug.Log("Target��û��" + ingredient.name);
                totalSimilarity += 0f;
            }

            comparedCount++;
        }

        // �ͷ��������
        float extraMaterialPenalty = CalculateExtraMaterialPenalty(target, standard);

        float baseSimilarity = comparedCount > 0 ? totalSimilarity / comparedCount : 0f;
        float finalSimilarity = baseSimilarity * extraMaterialPenalty;

        return finalSimilarity;
    }

    private float CalculateExtraMaterialPenalty(Dictionary<Ingredient, float> target, Dictionary<Ingredient, float> standard)
    {
        // ���������ϵ�����
        int extraMaterials = target.Keys.Except(standard.Keys).Count();

        if (extraMaterials == 0) return 1f; // û�ж�����ϣ����ͷ�

        // �ͷ�ϵ�����������Խ�࣬�ͷ�Խ��
        float penalty = Mathf.Pow(0.8f, extraMaterials); // ÿ��������ϳ���0.8
        return Mathf.Max(0.1f, penalty); // ��ͱ���10%�����ƶ�
    }


    public void CalculateWithAll(Dictionary<Ingredient, float> target)
    {
        //if (isDone)
        {
            if (target.Count == 0)
            {
                HUDControl.ShowNothingIn();
                return;
            }
            foreach (var standard in standards)
            {
                if (standard != null)
                {
                    var Similarity = CalculateStandardBasedSimilarity(target, standard);
                    Similaritys.Add(Similarity);
                    Debug.Log(Similarity);
                }
            }
            if (Similaritys.Count > 0)
            {
                Debug.Log(allHerbalTeaRecipe[FindMaxIndex(Similaritys)].herbalTeaName);
                Debug.Log(Similaritys[FindMaxIndex(Similaritys)]);
                HUDControl.ShowSimlarityNNameText(Similaritys[FindMaxIndex(Similaritys)], allHerbalTeaRecipe[FindMaxIndex(Similaritys)].herbalTeaName);

            }
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
