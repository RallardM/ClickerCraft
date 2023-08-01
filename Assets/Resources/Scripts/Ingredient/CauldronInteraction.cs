
using UnityEngine;
using UnityEngine.EventSystems;

public class CauldronInteraction : IngredientManager, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        // Update the cauldron slot ingredients in case they have changed
        UpdateCauldronSlotIngredients();

        if (!CheckCauldronIngredients())
        {
            Debug.Log("Ingredients are not valid");
            return;
        }

        EIngredient resultingIngredient = GetResultingIngredient();
        CraftNewIngredient(resultingIngredient);
    }
}
