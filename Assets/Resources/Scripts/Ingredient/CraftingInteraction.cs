
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingInteraction : IngredientManager, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        // Update the cauldron slot ingredients in case they have changed
        IngredientPool.UpdateContainerIngredients(IngredientPool.CauldronIngredientTransforms, UIManager.EUiSlotContainer.Cauldron);

        if (!CheckCauldronIngredients())
        {
            Debug.Log("Ingredients are not valid");
            return;
        }

        EIngredient resultingIngredient = GetResultingIngredient();

        UIManager.LastClickedIngredientQuantity = 1;
        IngredientPool.AddIngredientToTransitPool(GetIngredientData(resultingIngredient), UIManager.EUiSlotContainer.Coagula);
    }
}
