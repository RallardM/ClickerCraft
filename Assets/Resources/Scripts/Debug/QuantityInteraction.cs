using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuantityInteraction : MonoBehaviour, IPointerClickHandler
{
    BasicIngredientInteraction m_ingredientInteraction;
    IngredientData m_ingredientData;

    private void Awake()
    {
        m_ingredientInteraction = transform.parent.parent.GetComponentInParent<BasicIngredientInteraction>();
        m_ingredientData = m_ingredientInteraction.IngredientData;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (name == "UpArrow")
        {
            IncreaseBasicIngredientQuantity();
        }
        else if (name == "DownArrow")
        {
            DecreaseBasicIngredientQuantity();
        }
    }
    
    private void IncreaseBasicIngredientQuantity()
    {
        if(m_ingredientInteraction.CurrentQuantity == m_ingredientData.MaxQuantity)
        {
            return;
        }

        m_ingredientInteraction.CurrentQuantity++;
    }

    private void DecreaseBasicIngredientQuantity()
    {
        if (m_ingredientInteraction.CurrentQuantity == 1)
        {
            return;
        }

        m_ingredientInteraction.CurrentQuantity--;
    }
}
