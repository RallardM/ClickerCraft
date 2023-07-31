
using UnityEngine;
using UnityEngine.EventSystems;

public class CauldronIngredientInteraction : IngredientManager, IPointerClickHandler
{
    private IngredientData m_ingredientData;
    private uint m_currentQuantity = 1;
    public IngredientData IngredientData { get { return m_ingredientData; } set { m_ingredientData = value; } }
    public uint CurrentQuantity { get { return m_currentQuantity; } set { m_currentQuantity = value; } }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("Is an ingredient inside the cauldron");

        //Check if the clicked ingredient has stacked ingredients
        if (CurrentQuantity > 1)
        {
            //Remove one ingredient from the stack
            //Debug.Log("Remove one ingredient from the stack");
            CurrentQuantity--;
            return;
        }

        //Debug.Log("Remove the ingredient from the cauldron");
        IngredientManager.RemoveIngredient(IngredientData);
        Destroy(gameObject);
        m_cauldronPreviousSize--;
    }
}