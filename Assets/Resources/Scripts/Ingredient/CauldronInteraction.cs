
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CauldronInteraction : IngredientManager, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        // Update the cauldron slot ingredients in case they have changed
        IngredientPool.UpdateSlotIngredients(IngredientPool.CauldronIngredientTransforms, UIManager.EUiSlotContainer.Cauldron);

        if (!CheckCauldronIngredients())
        {
            Debug.Log("Ingredients are not valid");
            return;
        }

        EIngredient resultingIngredient = GetResultingIngredient();
        //IngredientPool.CraftNewIngredient(resultingIngredient);
        IngredientPool.AddIngredient(GetIngredientData(resultingIngredient), UIManager.EUiSlotContainer.Coagula);
    }
}
