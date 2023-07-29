
using UnityEngine;
using UnityEngine.EventSystems;

public class CauldronInteraction : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnCauldronMix :");
        IngredientManager.MixIngredients();
    }
}
