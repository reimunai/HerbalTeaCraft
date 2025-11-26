using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScaleManager : XRSocketInteractor
{
    public List<Ingredient> defaultIngredients=new List<Ingredient>();
    public XRSocketInteractor Self;
    public Ingredient Ingredient;
    public float Weight = 0f;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        var obj = args.interactableObject.transform.gameObject;
        var totalHerb = obj.GetComponent<SocketManager>();
        foreach (var ingredient in defaultIngredients)
        {
            if (totalHerb.IngredientName == ingredient.ingredientName)
            {
                Ingredient = ingredient;
                Weight = totalHerb.totalWeight;
                totalHerb.ReleaseAllSocket();
                Self.interactionManager.SelectExit(Self, args.interactableObject);
                totalHerb.Transport();
                
            }
            else
            {
                totalHerb.ReleaseAllSocket();
                Self.interactionManager.SelectExit(Self, args.interactableObject);
                totalHerb.Transport();
                Debug.Log("Wrong herb");
            }
        }
    }

    public void ClearScale() 
    {
        Ingredient = null;
        Weight= 0;
    }
}
