using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScaleManager : MonoBehaviour
{
    public PotManager potManager;
    public List<Ingredient> defaultIngredients=new List<Ingredient>();
    public XRSocketInteractor Self;
    public Ingredient Ingredient;
    public float Weight = 0f;

    private void Start()
    {
        Self.selectEntered.AddListener(OnSelected);
    }

    private void OnSelected(SelectEnterEventArgs arg0)
    {
        var obj = arg0.interactableObject.transform.gameObject;
        var totalHerb = obj.GetComponent<SocketManager>();

        if (totalHerb == null) 
        {
            return;
        }

        if (defaultIngredients == null || defaultIngredients.Count == 0)
        {
            return;
        }

        foreach (var ingredient in defaultIngredients)
        {
            
            if (ingredient == null) continue;

            if (ingredient.ingredientName != totalHerb.IngredientName)
            {
                continue;
            }
            Ingredient = ingredient;
            Weight = totalHerb.totalWeight;
            Self.allowSelect = false;
            Self.interactionManager.SelectExit(Self, arg0.interactableObject);
            totalHerb.ResetAll();
            StartCoroutine(Reable(Self));
            
            potManager.pot.AddIngredient(GetIngredient(), GetWeight());
            
            return;
            
        }

        if (Self != null && Self.interactionManager != null)
        {
            Self.allowSelect = false;
            totalHerb.ResetAll();
            Self.interactionManager.SelectExit(Self, arg0.interactableObject);
            
            StartCoroutine(Reable(Self));
        }
        
        ClearScale();
        Debug.Log("Wrong herb");
    }

    private IEnumerator Reable(XRSocketInteractor socket)
    {
        yield return new WaitForSeconds(2);
        socket.allowSelect = true;
    }
    public Ingredient GetIngredient()
    {
        return Ingredient;
    }
    public float GetWeight()
    {
        return Weight; 
    }
    public void ClearScale() 
    {
        Ingredient = null;
        Weight= 0;
    }
}
