
using UnityEngine;
using UnityEngine.EventSystems;

public class CauldronInteraction : IngredientManager, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnCauldronMix :");
        IngredientManager.MixIngredients();
    }
}
