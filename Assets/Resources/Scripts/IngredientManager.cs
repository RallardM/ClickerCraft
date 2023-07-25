using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    [SerializeField] static List<IngredientData> m_chauldron = new List<IngredientData>();
    private static IngredientManager _Instance;

    [SerializeField] private static GameObject m_fireIngredientPrefab;
    [SerializeField] private static GameObject m_airIngredientPrefab;
    [SerializeField] private static GameObject m_earthIngredientPrefab;
    [SerializeField] private static GameObject m_waterIngredientPrefab;
    [SerializeField] private static GameObject m_therIngredientPrefab;

    static List<Transform> m_chauldronSlots;
    private static Transform m_firstChauldronSlot;
    private static Transform m_secondChauldronSlot;
    private static Transform m_thirdChauldronSlot;
    private static Transform m_fourthChauldronSlot;

    //private static uint m_chauldronPreviousSize = 0;

    private static uint m_fireChauldronCount = 0;
    private static uint m_earthChauldronCount = 0;
    private static uint m_waterChauldronCount = 0;
    private static uint m_airChauldronCount = 0;
    private static uint m_etherChauldronCount = 0;

    private void Awake()
    {
        m_firstChauldronSlot = GameObject.FindGameObjectWithTag("FirstChauldronSlot").transform;
        m_secondChauldronSlot = GameObject.FindGameObjectWithTag("SecondChauldronSlot").transform;
        m_thirdChauldronSlot = GameObject.FindGameObjectWithTag("ThirdChauldronSlot").transform;
        m_fourthChauldronSlot = GameObject.FindGameObjectWithTag("FourthChauldronSlot").transform;

        m_chauldronSlots = new List<Transform>
        {
            m_firstChauldronSlot,
            m_secondChauldronSlot,
            m_thirdChauldronSlot,
            m_fourthChauldronSlot
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
        // Return if the chauldron is empty
        if (m_chauldron.Count == 0)
        {
            return;
        }

        // If the chauldron is not empty

        //uint totalNumberInChauldron = GetNumberOfStackedIngredients();
        //if (m_chauldronPreviousSize != totalNumberInChauldron)
        //{
        //    // Update the chauldron content
        //    //ResetChauldronContent();
        //    UpdateChauldronContent();
        //}

        foreach (IngredientData ingredientData in m_chauldron)
        { 
            foreach (Transform chauldronSlots in m_chauldronSlots)
            {
                GetIngredientPrefab(ingredientData);
                //chauldronSlots.Instantiate(m_chauldronSlots);
            }
        }
    }

    private uint GetNumberOfStackedIngredients()
    {
        return 0;
    }

    //private void ResetChauldronContent()
    //{

    //}

    private void UpdateChauldronContent()
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

        if (m_chauldron.Count == 4)
        {
            Debug.Log("Chauldron is full");
            return;
        }

        if (m_chauldron.Contains(ingredient))
        {
            // If the chauldron contains the ingredient
            // verify if it's stackable
            // and if it's max quantity is not reached
            uint ingredientCount = GetIngredientCount(ingredient);

            if (ingredient.isStackable && ingredientCount < ingredient.MaxQuantity)
            {
                m_chauldron.Add(ingredient);
                IncrementeIngredientCounter(ingredient);
                //m_chauldronPreviousSize++;
            }
            return;
        }

        // If the ingredient is not in the chauldron
        // add it for the first time
        m_chauldron.Add(ingredient);
        IncrementeIngredientCounter(ingredient);
        //m_chauldronPreviousSize++;
    }

    private static uint GetIngredientCount(IngredientData ingredient)
    {
        switch (ingredient.IngredientType)
        {
            case EBasicIngredient.Fire:
                return m_fireChauldronCount;
            case EBasicIngredient.Air:
                return m_airChauldronCount;
            case EBasicIngredient.Earth:
                return m_earthChauldronCount;
            case EBasicIngredient.Water:
                return m_waterChauldronCount;
            case EBasicIngredient.Ether:
                return m_etherChauldronCount;
            default:
                Debug.LogError("Ingredient type not found");
                return 0;
        }
    }

    private static void IncrementeIngredientCounter(IngredientData ingredient)
    {
        switch (ingredient.IngredientType)
        {
            case EBasicIngredient.Fire:
                m_fireChauldronCount++;
                break;
            case EBasicIngredient.Air:
                m_airChauldronCount++;
                break;
            case EBasicIngredient.Earth:
                m_earthChauldronCount++;
                break;
            case EBasicIngredient.Water:
                m_waterChauldronCount++;
                break;
            case EBasicIngredient.Ether:
                m_etherChauldronCount++;
                break;
            default:
                Debug.LogError("Ingredient type not found");
                break;
        }
    }

    private GameObject GetIngredientPrefab(IngredientData ingredientData)
    {
        switch (ingredientData.IngredientType)
        {
            case EBasicIngredient.Fire:
                return m_fireIngredientPrefab;
            case EBasicIngredient.Air:
                return m_airIngredientPrefab;
            case EBasicIngredient.Earth:
                return m_earthIngredientPrefab;
            case EBasicIngredient.Water:
                return m_waterIngredientPrefab;
            case EBasicIngredient.Ether:
                return m_therIngredientPrefab;
            default:
                Debug.LogError("Ingredient type not found");
                return null;
        }
    }
}

