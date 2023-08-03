// Source : https://www.youtube.com/watch?v=kWRyZ3hb1Vc
using UnityEngine;
using UnityEngine.EventSystems;

public class BasicIngredientInteraction : IngredientManager, IPointerClickHandler
{
    [SerializeField] IngredientData m_ingredientData;

    public IngredientData IngredientData { get { return m_ingredientData; } set { m_ingredientData = value; } }

    public void OnPointerClick(PointerEventData eventData)
    {
        IngredientPool.AddIngredient(IngredientData, UIManager.EUiSlotContainer.Cauldron);
    }
}
