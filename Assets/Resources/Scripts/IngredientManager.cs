
using System;
using System.Collections.Generic;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    [SerializeField] private GameObject m_ingredientPrefab;
    private static List<IngredientData> m_ingredientsInCauldron = new List<IngredientData>();
    private static List<Transform> m_cauldronSlots = new List<Transform>();

    public static readonly Array[] m_receipes = new EIngredient[][]
    {
        m_blazeReceipe, m_coalReceipe, m_vaporReceipe, m_fireburstReceipe, m_archeaReceipe,
        m_magmaReceipe, m_magmaReceipeExtended, m_rockReceipe, m_mudReceipe, m_DustReceipe, m_seedReceipe,
        m_steamRecipe, m_steamReceipeExtended, m_pondReceipe, m_pondReceipeExtended, m_puddleReceipe, m_rainReceipe, m_algeaReceipe,
        m_fireboltReceipe, m_fireboltReceipeExtended, m_duststormReceipe, m_duststormReceipeExtended, m_thunderstormReceipe, m_thunderstormReceipeExtended, m_tornadoReceipe, m_fungiReceipe, m_fungiReceipeExtended,
        m_pyroidReceipe, m_pyroidReceipeExtended, m_golemReceipe, m_golemReceipeExtended, m_undineReceipe, m_undineReceipeExtended, m_sylphReceipe, m_sylphReceipeExtended, m_spiritReceipe,
        m_nonCraftable
    };

    public enum EIngredient
    {
        Fire, Air, Earth, Water, Ether,
        Blaze, Coal, Vapor, Fireburst, Archea,
        Magma, MagmaExtended, Rock, Mud, Dust, Seed,
        Steam, SteamExtended, Pond, PondExtended, Puddle, Rain, Algae,
        Firebolt, FireboltExtended, Duststorm, DuststormExtended, Thunderstorm, ThunderstormExtended, Tornado, Fungi, FungiExtended,
        Pyroid, PyroidExtended, Golem, GolemExtended, Undine, UndineExtended, Sylph, SylphExtended, Spirit,
        Count
    }

    // Source : https://youtu.be/Zn4BDIXhy-M
    private static readonly EIngredient[] m_blazeReceipe = new EIngredient[2] { EIngredient.Fire, EIngredient.Fire };
    private static readonly EIngredient[] m_coalReceipe = new EIngredient[2] { EIngredient.Fire, EIngredient.Earth };
    private static readonly EIngredient[] m_vaporReceipe = new EIngredient[2] { EIngredient.Fire, EIngredient.Water };
    private static readonly EIngredient[] m_fireburstReceipe = new EIngredient[2] { EIngredient.Fire, EIngredient.Air };
    private static readonly EIngredient[] m_archeaReceipe = new EIngredient[2] { EIngredient.Fire, EIngredient.Ether };
    private static readonly EIngredient[] m_magmaReceipe = new EIngredient[2] { EIngredient.Rock, EIngredient.Fire };
    private static readonly EIngredient[] m_magmaReceipeExtended = new EIngredient[3] { EIngredient.Earth, EIngredient.Earth, EIngredient.Fire };
    private static readonly EIngredient[] m_rockReceipe = new EIngredient[2] { EIngredient.Earth, EIngredient.Earth };
    private static readonly EIngredient[] m_mudReceipe = new EIngredient[2] { EIngredient.Earth, EIngredient.Water };
    private static readonly EIngredient[] m_DustReceipe = new EIngredient[2] { EIngredient.Earth, EIngredient.Air };
    private static readonly EIngredient[] m_seedReceipe = new EIngredient[2] { EIngredient.Earth, EIngredient.Ether };
    private static readonly EIngredient[] m_steamRecipe = new EIngredient[2] { EIngredient.Puddle, EIngredient.Fire };
    private static readonly EIngredient[] m_steamReceipeExtended = new EIngredient[3] { EIngredient.Water, EIngredient.Water, EIngredient.Fire };
    private static readonly EIngredient[] m_pondReceipe = new EIngredient[2] { EIngredient.Puddle, EIngredient.Earth };
    private static readonly EIngredient[] m_pondReceipeExtended = new EIngredient[3] { EIngredient.Water, EIngredient.Water, EIngredient.Earth };
    private static readonly EIngredient[] m_puddleReceipe = new EIngredient[2] { EIngredient.Water, EIngredient.Water };
    private static readonly EIngredient[] m_rainReceipe = new EIngredient[2] { EIngredient.Water, EIngredient.Air };
    private static readonly EIngredient[] m_algeaReceipe = new EIngredient[2] { EIngredient.Water, EIngredient.Ether };
    private static readonly EIngredient[] m_fireboltReceipe = new EIngredient[2] { EIngredient.Tornado, EIngredient.Fire };
    private static readonly EIngredient[] m_fireboltReceipeExtended = new EIngredient[3] { EIngredient.Air, EIngredient.Air, EIngredient.Fire };
    private static readonly EIngredient[] m_duststormReceipe = new EIngredient[2] { EIngredient.Tornado, EIngredient.Earth };
    private static readonly EIngredient[] m_duststormReceipeExtended = new EIngredient[3] { EIngredient.Air, EIngredient.Air, EIngredient.Earth };
    private static readonly EIngredient[] m_thunderstormReceipe = new EIngredient[2] { EIngredient.Tornado, EIngredient.Water };
    private static readonly EIngredient[] m_thunderstormReceipeExtended = new EIngredient[3] { EIngredient.Air, EIngredient.Air, EIngredient.Water };
    private static readonly EIngredient[] m_tornadoReceipe = new EIngredient[2] { EIngredient.Air, EIngredient.Air };
    private static readonly EIngredient[] m_fungiReceipe = new EIngredient[2] { EIngredient.Tornado, EIngredient.Ether };
    private static readonly EIngredient[] m_fungiReceipeExtended = new EIngredient[3] { EIngredient.Air, EIngredient.Air, EIngredient.Ether };
    private static readonly EIngredient[] m_pyroidReceipe = new EIngredient[2] { EIngredient.Fire, EIngredient.Spirit };
    private static readonly EIngredient[] m_pyroidReceipeExtended = new EIngredient[3] { EIngredient.Ether, EIngredient.Ether, EIngredient.Fire };
    private static readonly EIngredient[] m_golemReceipe = new EIngredient[2] { EIngredient.Earth, EIngredient.Spirit };
    private static readonly EIngredient[] m_golemReceipeExtended = new EIngredient[3] { EIngredient.Ether, EIngredient.Ether, EIngredient.Earth };
    private static readonly EIngredient[] m_undineReceipe = new EIngredient[2] { EIngredient.Water, EIngredient.Spirit };
    private static readonly EIngredient[] m_undineReceipeExtended = new EIngredient[3] { EIngredient.Ether, EIngredient.Ether, EIngredient.Water };
    private static readonly EIngredient[] m_sylphReceipe = new EIngredient[2] { EIngredient.Air, EIngredient.Spirit };
    private static readonly EIngredient[] m_sylphReceipeExtended = new EIngredient[3] { EIngredient.Ether, EIngredient.Ether, EIngredient.Air };
    private static readonly EIngredient[] m_spiritReceipe = new EIngredient[2] { EIngredient.Ether, EIngredient.Ether };
    private static readonly EIngredient[] m_nonCraftable = new EIngredient[1] { EIngredient.Count };




    private static IngredientManager _Instance;

    private static Transform m_firstCauldronSlot;
    private static Transform m_secondCauldronSlot;
    private static Transform m_thirdCauldronSlot;
    private static Transform m_fourthCauldronSlot;

    //private static uint m_fireCauldronCount = 0;
    //private static uint m_earthCauldronCount = 0;
    //private static uint m_waterCauldronCount = 0;
    //private static uint m_airCauldronCount = 0;
    //private static uint m_etherCauldronCount = 0;
    //private static uint m_blazeCauldronCount = 0;
    //private static uint m_coalCauldronCount = 0;
    //private static uint m_vaporCauldronCount = 0;
    //private static uint m_fireburstCauldronCount = 0;
    //private static uint m_archeaCauldronCount = 0;
    //private static uint m_magmaCauldronCount = 0;
    //private static uint m_rockCauldronCount = 0;
    //private static uint m_mudCauldronCount = 0;
    //private static uint m_dustCauldronCount = 0;
    //private static uint m_seedCauldronCount = 0;
    //private static uint m_steamCauldronCount = 0;
    //private static uint m_pondCauldronCount = 0;
    //private static uint m_puddleCauldronCount = 0;
    //private static uint m_rainCauldronCount = 0;
    //private static uint m_algeaCauldronCount = 0;
    //private static uint m_fireboltCauldronCount = 0;
    //private static uint m_duststormCauldronCount = 0;
    //private static uint m_thunderstormCauldronCount = 0;
    //private static uint m_tornadoCauldronCount = 0;
    //private static uint m_fungiCauldronCount = 0;
    //private static uint m_pyroidCauldronCount = 0;
    //private static uint m_golemCauldronCount = 0;
    //private static uint m_undineCauldronCount = 0;
    //private static uint m_sylphCauldronCount = 0;
    //private static uint m_spiritCauldronCount = 0;

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

        //Debug.Log("Ingerdients in cauldron : " + m_ingredientsInCauldron.Count);

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

        //Debug.Log("Cauldron is not full");

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

        //Debug.Log("Ingredient added to the cauldron");
        m_ingredientsInCauldron.Add(ingredient);
        IncrementeIngredientCounter(ingredient);
        //m_cauldronPreviousSize++;
    }

    public static void MixIngredients()
    {
        //for (int i = 0; i < m_ingredientsInCauldron.Count; i++)
        //{
        //    EIngredient ingredient = m_ingredientsInCauldron[i].Ingredient;
        //    foreach (Array array in m_receipes)
        //    {
        //        foreach (EIngredient receipeIngredient in array)
        //        {
        //            if (receipeIngredient == ingredient)
        //            {
        //                Debug.Log("One ingredient found in receipe");
        //            }
        //        }
        //    }
        //}

    }

    private static void IncrementeIngredientCounter(IngredientData ingredient)
    {
        //switch (ingredient.Ingredient)
        //{
        //    case EIngredient.Fire:
        //        m_fireCauldronCount++;
        //        break;

        //    case EIngredient.Air:
        //        m_airCauldronCount++;
        //        break;

        //    case EIngredient.Earth:
        //        m_earthCauldronCount++;
        //        break;

        //    case EIngredient.Water:
        //        m_waterCauldronCount++;
        //        break;

        //    case EIngredient.Ether:
        //        m_etherCauldronCount++;
        //        break;

        //    case EIngredient.Blaze:
        //        m_blazeCauldronCount++;
        //        break;

        //    case EIngredient.Coal:
        //        m_coalCauldronCount++;
        //        break;

        //    case EIngredient.Vapor:
        //        m_vaporCauldronCount++;
        //        break;

        //    case EIngredient.Fireburst:
        //        m_fireburstCauldronCount++;
        //        break;

        //    case EIngredient.Archea:
        //        m_archeaCauldronCount++;
        //        break;

        //    case EIngredient.Magma:
        //        m_magmaCauldronCount++;
        //        break;

        //    case EIngredient.Rock:
        //        m_rockCauldronCount++;
        //        break;

        //    case EIngredient.Mud:
        //        m_mudCauldronCount++;
        //        break;

        //    case EIngredient.Dust:
        //        m_dustCauldronCount++;
        //        break;

        //    case EIngredient.Seed:
        //        m_seedCauldronCount++;
        //        break;

        //    case EIngredient.Steam:
        //        m_steamCauldronCount++;
        //        break;

        //    case EIngredient.Pond:
        //        m_pondCauldronCount++;
        //        break;

        //    case EIngredient.Puddle:
        //        m_puddleCauldronCount++;
        //        break;

        //    case EIngredient.Rain:
        //        m_rainCauldronCount++;
        //        break;

        //    case EIngredient.Algae:
        //        m_algeaCauldronCount++;
        //        break;

        //    case EIngredient.Firebolt:
        //        m_fireboltCauldronCount++;
        //        break;

        //    case EIngredient.Duststorm:
        //        m_duststormCauldronCount++;
        //        break;

        //    case EIngredient.Thunderstorm:
        //        m_thunderstormCauldronCount++;
        //        break;

        //    case EIngredient.Tornado:
        //        m_tornadoCauldronCount++;
        //        break;

        //    case EIngredient.Fungi:
        //        m_fungiCauldronCount++;
        //        break;

        //    case EIngredient.Pyroid:
        //        m_pyroidCauldronCount++;
        //        break;

        //    case EIngredient.Golem:
        //        m_golemCauldronCount++;
        //        break;

        //    case EIngredient.Undine:
        //        m_undineCauldronCount++;
        //        break;

        //    case EIngredient.Sylph:
        //        m_sylphCauldronCount++;
        //        break;

        //    case EIngredient.Spirit:
        //        m_spiritCauldronCount++;
        //        break;

        //    case EIngredient.MagmaExtended:
        //    case EIngredient.SteamExtended:
        //    case EIngredient.PondExtended:
        //    case EIngredient.FireboltExtended:
        //    case EIngredient.DuststormExtended:
        //    case EIngredient.ThunderstormExtended:
        //    case EIngredient.PyroidExtended:
        //    case EIngredient.GolemExtended:
        //    case EIngredient.UndineExtended:
        //    case EIngredient.SylphExtended:
        //    default:
        //        Debug.LogError("Ingredient type incorrect or not found.");
        //        break;  
        //}
    }

    private static uint GetIngredientCount(IngredientData ingredient)
    {
        //switch (ingredient.Ingredient)
        //{
        //    case EIngredient.Fire:
        //        return m_fireCauldronCount;

        //    case EIngredient.Air:
        //        return m_airCauldronCount;

        //    case EIngredient.Earth:
        //        return m_earthCauldronCount;

        //    case EIngredient.Water:
        //        return m_waterCauldronCount;

        //    case EIngredient.Ether:
        //        return m_etherCauldronCount;


        //    default:
        //        Debug.LogError("Ingredient type not found");
        //        return 0;
        //}
        return 0;
    }
}

