using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingInteraction : IngredientManager, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        // Update the UI cauldron slots in case they have changed
        IngredientPool.UpdateContainerIngredients(IngredientPool.CauldronIngredientTransforms, UIManager.EUiSlotContainer.Cauldron);
        IngredientPool.ClearCauldronDictionary();
        IngredientPool.PrepareCauldronDictionary();

        int ingredientCount = IngredientPool.GetCauldronDictionaryTotalValue();
        if (ingredientCount <= 1)
        {
            return;
        }

        if (!CheckCauldronIngredients())
        {
            Debug.Log("Ingredients are not valid");
            return;
        }

        EIngredient resultingIngredient = GetResultingIngredient();
        
        UIManager.LastClickedIngredientQuantity = 1;
        IngredientPool.AddIngredientToTransitPool(GetIngredientData(resultingIngredient), UIManager.EUiSlotContainer.Coagula);
        
        //IngredientPool.ClearCauldronDictionary();
        IngredientPool.ClearContainerTransformPool(UIManager.EUiSlotContainer.Cauldron);
        IngredientPool.ClearContainerTransitPool(UIManager.EUiSlotContainer.Cauldron);
        SetContainerPreviousIngredientCount(UIManager.EUiSlotContainer.Cauldron, 0);
        IngredientPool.DestroyAllGameObjectsFromContainer(UIManager.EUiSlotContainer.Cauldron);
    }
}