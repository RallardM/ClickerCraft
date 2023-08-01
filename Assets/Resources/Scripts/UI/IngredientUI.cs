using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientUI : UIManager
{
    CauldronIngredientInteraction m_ingredientInteraction;

    private Image m_image;
    //private Transform m_quantityTextTransform;
    private TextMeshProUGUI m_quantityText;
    private uint m_currentQuantity;
    private const uint SINGLE_INGREDIENT = 1;

    //[SerializeField] TextMeshProUGUI m_title;
    //[SerializeField] TextMeshProUGUI m_description;
    //[SerializeField] TextMeshProUGUI m_manaCost;
    //[SerializeField] TextMeshProUGUI m_maxQuantity;
    //[SerializeField] TextMeshProUGUI m_ingredientType;

    //public Image Image { get { return m_image; } set { m_image = value; } }


    void Awake()
    {
        //m_ingredientData = GetComponent<ItemInteraction>().GetIngredientData();
        //Sprite spriteFromImage = GetComponent<Image>().sprite;
        //m_ingredientData.Sprite = spriteFromImage;
        m_ingredientInteraction = GetComponent<CauldronIngredientInteraction>();
        m_image = GetComponent<Image>();
        //m_quantityTextTransform = transform.Find("QuantityText");
        m_quantityText = transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();

        if (GetComponent<CauldronIngredientInteraction>() != null)
        {
            m_currentQuantity = GetComponent<CauldronIngredientInteraction>().CurrentQuantity;
        }
        else
        {
            m_currentQuantity = SINGLE_INGREDIENT;
        }
        
        //m_title.text = m_ingredientData.Name;
        //m_description.text = m_ingredientData.Description;
        //m_image.sprite = m_ingredientData.Sprite;
        //m_maxQuantity.text = m_ingredientData.MaxQuantity.ToString();
        //m_ingredientType.text = m_ingredientData.IngredientType.ToString();
    }

    private void Update()
    {
        // Return if the ingredient is not in the cauldron (it is therfore a static basic ingredient)
        if (GetComponent<CauldronIngredientInteraction>() == null)
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
        if (m_currentQuantity != GetComponent<CauldronIngredientInteraction>().CurrentQuantity)
        {
            m_currentQuantity = GetComponent<CauldronIngredientInteraction>().CurrentQuantity;
            m_quantityText.text = m_currentQuantity.ToString();
        }
    }

    //    public void SetIngredientData(IngredientData ingredientData)
    //    {
    //        if (ingredientData == null)
    //        {
    //            Debug.LogError("IngredientData is null");
    //            return;
    //        }

    //        m_ingredientData = ingredientData;
    //        //m_title.text = m_ingredientData.Name;
    //        //m_description.text = m_ingredientData.Description;
    //        m_image.sprite = ingredientData.Sprite;
    //        //m_maxQuantity.text = m_ingredientData.MaxQuantity.ToString();
    //        //m_ingredientType.text = m_ingredientData.IngredientType.ToString();
    //    }
}
