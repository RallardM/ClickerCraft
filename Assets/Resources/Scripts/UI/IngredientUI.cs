using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientUI : UIManager
{
    IngredientInteraction m_ingredientInteraction;

    private Image m_image;
    private TextMeshProUGUI m_quantityText;
    private uint m_currentQuantity;
    private const uint SINGLE_INGREDIENT = 1;

    void Awake()
    {
        m_ingredientInteraction = GetComponent<IngredientInteraction>();
        m_image = GetComponent<Image>();
        m_quantityText = transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();

        if (GetComponent<IngredientInteraction>() != null)
        {
            m_currentQuantity = GetComponent<IngredientInteraction>().CurrentQuantity;
        }
        else
        {
            m_currentQuantity = SINGLE_INGREDIENT;
        }
    }

    private void Update()
    {
        // Return if the ingredient is not in the cauldron (it is therfore a static basic ingredient)
        if (GetComponent<IngredientInteraction>() == null)
        {
            return;
        }

        UpdateSprite();
        UpdateQuantityText();
        UpdateStackText();
    }

    private void UpdateSprite()
    {
        // If the image has no sprite but the , load the corresponding sprite
        if (m_ingredientInteraction != null && m_image.sprite == null)
        {
            m_image.sprite = m_ingredientInteraction.IngredientData.Sprite;

            // Set the ingredient prefab transform to visible
            // so it does not appear as a white square
            // while it receives its respective sprite 
            GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
    }

    private void UpdateStackText()
    {
        // If the quantity of the ingredient is more than 1 (Is a stack), show the quantity text
        if (m_currentQuantity > SINGLE_INGREDIENT)
        {
            m_quantityText.gameObject.SetActive(true);
        }
        else
        {
            m_quantityText.gameObject.SetActive(false);
        }
    }

    private void UpdateQuantityText()
    {
        // If the quantity of the ingredient has changed, update the quantity text
        if (m_currentQuantity != GetComponent<IngredientInteraction>().CurrentQuantity)
        {
            m_currentQuantity = GetComponent<IngredientInteraction>().CurrentQuantity;
            m_quantityText.text = m_currentQuantity.ToString();
        }
    }
}
