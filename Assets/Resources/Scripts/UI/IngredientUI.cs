using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientUI : MonoBehaviour
{
    IngredientData m_ingredientData;

    private Image m_image;
    private TextMeshProUGUI m_quantity;

    //[SerializeField] TextMeshProUGUI m_title;
    //[SerializeField] TextMeshProUGUI m_description;
    //[SerializeField] TextMeshProUGUI m_manaCost;
    //[SerializeField] TextMeshProUGUI m_maxQuantity;
    //[SerializeField] TextMeshProUGUI m_ingredientType;

    void Awake()
    {
        //m_ingredientData = GetComponent<ItemInteraction>().GetIngredientData();
        //Sprite spriteFromImage = GetComponent<Image>().sprite;
        //m_ingredientData.Sprite = spriteFromImage;

        m_image = GetComponent<Image>();

        //m_title.text = m_ingredientData.Name;
        //m_description.text = m_ingredientData.Description;
        //m_image.sprite = m_ingredientData.Sprite;
        //m_maxQuantity.text = m_ingredientData.MaxQuantity.ToString();
        //m_ingredientType.text = m_ingredientData.IngredientType.ToString();
    }

    public void SetIngredientData(IngredientData ingredientData)
    {
        if (ingredientData == null)
        {
            Debug.LogError("IngredientData is null");
            return;
        }

        m_ingredientData = ingredientData;
        //m_title.text = m_ingredientData.Name;
        //m_description.text = m_ingredientData.Description;
        m_image.sprite = ingredientData.Sprite;
        //m_maxQuantity.text = m_ingredientData.MaxQuantity.ToString();
        //m_ingredientType.text = m_ingredientData.IngredientType.ToString();
    }
}
