// Source : https://www.youtube.com/watch?v=kWRyZ3hb1Vc
using UnityEngine;
using UnityEngine.EventSystems;

public class BasicIngredientInteraction : IngredientManager, IPointerClickHandler
{
    [SerializeField] IngredientData m_ingredientData;
    private uint m_currentQuantity = 1;

    public uint CurrentQuantity { get { return m_currentQuantity; } set { m_currentQuantity = value; } }
    public IngredientData IngredientData { get { return m_ingredientData; } set { m_ingredientData = value; } }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("Clicked: " + IngredientData.Name);
        UIManager.LastClickedIngredientQuantity = m_currentQuantity;
        IngredientPool.AddIngredientToTransitPool(IngredientData, UIManager.EUiSlotContainer.Cauldron);
    }
}