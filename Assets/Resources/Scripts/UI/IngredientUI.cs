using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientUI : MonoBehaviour
{
    IngredientData m_ingredientData;

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

    public Image Image { get { return m_image; } set { m_image = value; } }


    void Awake()
    {
        //m_ingredientData = GetComponent<ItemInteraction>().GetIngredientData();
        //Sprite spriteFromImage = GetComponent<Image>().sprite;
        //m_ingredientData.Sprite = spriteFromImage;

        m_image = GetComponent<Image>();
        //m_quantityTextTransform = transform.Find("QuantityText");
        m_quantityText = transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        m_currentQuantity = GetComponent<IngredientInteraction>().CurrentQuantity;

        //m_title.text = m_ingredientData.Name;
        //m_description.text = m_ingredientData.Description;
        //m_image.sprite = m_ingredientData.Sprite;
        //m_maxQuantity.text = m_ingredientData.MaxQuantity.ToString();
        //m_ingredientType.text = m_ingredientData.IngredientType.ToString();
    }

    private void Update()
    {
        if (m_currentQuantity != GetComponent<IngredientInteraction>().CurrentQuantity)
        {
            m_currentQuantity = GetComponent<IngredientInteraction>().CurrentQuantity;
            m_quantityText.text = m_currentQuantity.ToString();
        }
        
        if (m_currentQuantity > SINGLE_INGREDIENT)
        {
            m_quantityText.gameObject.SetActive(true);
        }
        else
        {
            m_quantityText.gameObject.SetActive(false);
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
