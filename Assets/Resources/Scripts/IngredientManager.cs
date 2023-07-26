
using System.Collections.Generic;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    static List<IngredientData> m_ingredientsInCauldron = new List<IngredientData>();
    static List<Transform> m_cauldronSlots = new List<Transform>();

    private static IngredientManager _Instance;

    [SerializeField] private GameObject m_ingredientPrefab;

    private static Transform m_firstCauldronSlot;
    private static Transform m_secondCauldronSlot;
    private static Transform m_thirdCauldronSlot;
    private static Transform m_fourthCauldronSlot;

    private static uint m_fireCauldronCount = 0;
    private static uint m_earthCauldronCount = 0;
    private static uint m_waterCauldronCount = 0;
    private static uint m_airCauldronCount = 0;
    private static uint m_etherCauldronCount = 0;

    private static uint m_cauldronPreviousSize = 0;

    private void Awake()
    {
        m_firstCauldronSlot = transform.Find("Canvas/CauldronSlots/TopInventorySlot");
        m_secondCauldronSlot = transform.Find("Canvas/CauldronSlots/LeftInventorySlot");
        m_thirdCauldronSlot = transform.Find("Canvas/CauldronSlots/RightInventorySlot");
        m_fourthCauldronSlot = transform.Find("Canvas/CauldronSlots/BottomInventorySlot");

        m_cauldronSlots = new List<Transform>
        {
            m_firstCauldronSlot,
            m_secondCauldronSlot,
            m_thirdCauldronSlot,
            m_fourthCauldronSlot
        };
    }

    public enum EBasicIngredient
    {
        Fire,
        Air,
        Earth,
        Water,
        Ether,
        Count
    }

    public void Update()
    {
        // Return if the cauldron is empty
        if (m_ingredientsInCauldron.Count == 0)
        {
            return;
        }

        // If the cauldron is not empty

        // Return if the cauldron size has not changed
        if (m_cauldronPreviousSize == m_ingredientsInCauldron.Count)
        {
            return;
        }

        m_cauldronPreviousSize = (uint)m_ingredientsInCauldron.Count;

        Debug.Log("Ingerdients in cauldron : " + m_ingredientsInCauldron.Count);

        for (int i = 0; i < m_ingredientsInCauldron.Count; i++)
        {
            Transform cauldronSlot = m_cauldronSlots[i];
            IngredientData ingredientData = m_ingredientsInCauldron[i];
            //GameObject ingredientPrefab = GetIngredientData(ingredientData);
            IngredientUI ingredientUI = Instantiate(m_ingredientPrefab, cauldronSlot).GetComponent<IngredientUI>();
            ingredientUI.SetIngredientData(ingredientData);
        }
    }

    private uint GetNumberOfStackedIngredients()
    {
        return 0;
    }

    //private void ResetCauldronContent()
    //{

    //}

    private void UpdateCauldronContent()
    {
        
    }

    public static IngredientManager GetInstance()
    {
        if (_Instance == null)
        {
            _Instance = new IngredientManager();
        }

        return _Instance;
    }

    public IngredientManager()
    {
        if (_Instance)
        {
            Destroy(this);
            return;
        }
    }

    public static void AddIngredient(IngredientData ingredient)
    {
        if (ingredient == null)
        {
            Debug.LogError("Ingredient is null");
            return;
        }

        if (m_ingredientsInCauldron.Count == 4)
        {
            Debug.Log("Cauldron is full");
            return;
        }

        Debug.Log("Cauldron is not full");

        if (m_ingredientsInCauldron.Contains(ingredient))
        {
            // If the cauldron contains the ingredient
            // verify if it's stackable
            // and if it's max quantity is not reached
            uint ingredientCount = GetIngredientCount(ingredient);

            if (ingredient.isStackable && ingredientCount < ingredient.MaxQuantity)
            {
                m_ingredientsInCauldron.Add(ingredient);
                IncrementeIngredientCounter(ingredient);
                Debug.Log("Ingredient added to the cauldron");
                //m_cauldronPreviousSize++;
            }
            return;
        }

        // If the ingredient is not in the cauldron
        // add it for the first time
        Debug.Log("Ingredient added to the cauldron");
        m_ingredientsInCauldron.Add(ingredient);
        IncrementeIngredientCounter(ingredient);
        //m_cauldronPreviousSize++;
    }

    private static void IncrementeIngredientCounter(IngredientData ingredient)
    {
        switch (ingredient.IngredientType)
        {
            case EBasicIngredient.Fire:
                m_fireCauldronCount++;
                break;
            case EBasicIngredient.Air:
                m_airCauldronCount++;
                break;
            case EBasicIngredient.Earth:
                m_earthCauldronCount++;
                break;
            case EBasicIngredient.Water:
                m_waterCauldronCount++;
                break;
            case EBasicIngredient.Ether:
                m_etherCauldronCount++;
                break;
            default:
                Debug.LogError("Ingredient type not found");
                break;
        }
    }

    private static uint GetIngredientCount(IngredientData ingredient)
    {
        switch (ingredient.IngredientType)
        {
            case EBasicIngredient.Fire:
                return m_fireCauldronCount;
            case EBasicIngredient.Air:
                return m_airCauldronCount;
            case EBasicIngredient.Earth:
                return m_earthCauldronCount;
            case EBasicIngredient.Water:
                return m_waterCauldronCount;
            case EBasicIngredient.Ether:
                return m_etherCauldronCount;
            default:
                Debug.LogError("Ingredient type not found");
                return 0;
        }
    }

    //private GameObject GetIngredientData(IngredientData ingredientData)
    //{
    //    switch (ingredientData.IngredientType)
    //    {
    //        case EBasicIngredient.Fire:
    //            return m_fireIngredientPrefab;
    //        case EBasicIngredient.Air:
    //            return m_airIngredientPrefab;
    //        case EBasicIngredient.Earth:
    //            return m_earthIngredientPrefab;
    //        case EBasicIngredient.Water:
    //            return m_waterIngredientPrefab;
    //        case EBasicIngredient.Ether:
    //            return m_etherIngredientPrefab;
    //        default:
    //            Debug.LogError("Ingredient type not found");
    //            return null;
    //    }
    //}
}

