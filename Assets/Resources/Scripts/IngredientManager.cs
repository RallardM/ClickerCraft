
using System.Collections.Generic;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    [SerializeField] private GameObject m_ingredientPrefab;
    private static List<IngredientData> m_ingredientsInCauldron = new List<IngredientData>();
    private static List<Transform> m_cauldronSlots = new List<Transform>();

    private static IngredientManager _Instance;

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

    public enum EIngredient
    {
        Fire,      Air,        Earth,         Water,      Ether,
        Blaze,     Coal,       Vapor,         Fireburst,  Archea,
        Magma,     Rock,       Mud,           Dust,       Seed,
        Steam,     Pond,       Puddle,        Rain,       Algae,
        Firebolt,  Duststorm,  Thunderstorm,  Tornado,    Fungi,
        Pyroid,    Golem,      Undine,        Sylph,      Spirit,
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
            m_ingredientPrefab.tag = "CauldonIngredient";
            IngredientUI ingredientUI = Instantiate(m_ingredientPrefab, cauldronSlot).GetComponent<IngredientUI>();
            ingredientUI.SetIngredientData(ingredientData);
        }
    }

    public static void RemoveIngredient(IngredientData ingredientData)
    {
        m_ingredientsInCauldron.Remove(ingredientData);
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
        switch (ingredient.Ingredient)
        {
            case EIngredient.Fire:
                m_fireCauldronCount++;
                break;
            case EIngredient.Air:
                m_airCauldronCount++;
                break;
            case EIngredient.Earth:
                m_earthCauldronCount++;
                break;
            case EIngredient.Water:
                m_waterCauldronCount++;
                break;
            case EIngredient.Ether:
                m_etherCauldronCount++;
                break;
            default:
                Debug.LogError("Ingredient type not found");
                break;
        }
    }

    private static uint GetIngredientCount(IngredientData ingredient)
    {
        switch (ingredient.Ingredient)
        {
            case EIngredient.Fire:
                return m_fireCauldronCount;
            case EIngredient.Air:
                return m_airCauldronCount;
            case EIngredient.Earth:
                return m_earthCauldronCount;
            case EIngredient.Water:
                return m_waterCauldronCount;
            case EIngredient.Ether:
                return m_etherCauldronCount;
            default:
                Debug.LogError("Ingredient type not found");
                return 0;
        }
    }
}

