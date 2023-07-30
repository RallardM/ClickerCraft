using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CauldronIngredientInteraction : IngredientManager
{
    private IngredientData m_ingredientData;
    private uint m_currentQuantity = 1;
    public IngredientData IngredientData { get { return m_ingredientData; } set { m_ingredientData = value; } }
    public uint CurrentQuantity { get { return m_currentQuantity; } set { m_currentQuantity = value; } }
    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("Is an ingredient inside the cauldron");
        IngredientManager.RemoveIngredient(IngredientData);
        Destroy(gameObject);

    }
}