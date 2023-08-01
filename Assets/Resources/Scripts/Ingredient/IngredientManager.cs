
using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    private static List<IngredientData> m_ingredientsTransitToCauldron = new List<IngredientData>();
    //private static List<GameObject> m_cauldronSlots = new List<GameObject>();
    //private static List<GameObject> m_coagulaSlots = new List<GameObject>();
    private static List<Transform> m_cauldronSlotsIngredients = new List<Transform>();

    public static EIngredient[][] m_receipes;
    private static GameObject[] m_cauldronSlots;
    private static GameObject[] m_coagulaSlots;

    public enum EIngredient
    {
        Fire, Air, Earth, Water, Ether, // 5 elements
        Blaze, Coal, Vapor, Fireburst, Archea, // 5 elements
        Magma, MagmaExtended, Rock, Mud, Dust, Seed, // 6 elements
        Steam, SteamExtended, Pond, PondExtended, Puddle, Rain, Algae, // 7 elements
        Firebolt, FireboltExtended, Duststorm, DuststormExtended, Thunderstorm, ThunderstormExtended, Tornado, Fungi, FungiExtended, // 9 elements
        Pyroid, PyroidExtended, Golem, GolemExtended, Undine, UndineExtended, Sylph, SylphExtended, Spirit, // 9 elements
        Count // 1 element
    } // total 42 elements - 0 to 41 

    // Source : https://youtu.be/Zn4BDIXhy-M
    private static readonly EIngredient[] m_fireReceipe = new EIngredient[1] { EIngredient.Count }; // To add 0-4 elements for the SetReceipe() function
    private static readonly EIngredient[] m_airReceipe = new EIngredient[1] { EIngredient.Count }; // To add 0-4 elements for the SetReceipe() function
    private static readonly EIngredient[] m_earthReceipe = new EIngredient[1] { EIngredient.Count }; // To add 0-4 elements for the SetReceipe() function
    private static readonly EIngredient[] m_waterReceipe = new EIngredient[1] { EIngredient.Count }; // To add 0-4 elements for the SetReceipe() function
    private static readonly EIngredient[] m_etherReceipe = new EIngredient[1] { EIngredient.Count }; // To add 0-4 elements for the SetReceipe() function
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
    private static readonly EIngredient[] m_steamReceipe = new EIngredient[2] { EIngredient.Puddle, EIngredient.Fire };
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

    private GameObject m_firstCauldronSlot;
    private GameObject m_secondCauldronSlot;
    private GameObject m_thirdCauldronSlot;
    private GameObject m_fourthCauldronSlot;

    private GameObject m_cauldronIngredientPrefab;

    private GameObject m_firstCoagulaSlot;
    private GameObject m_secondCoagulaSlot;
    private GameObject m_thirdCoagulaSlot;
    private GameObject m_fourthCoagulaSlot;
    private GameObject m_fifthCoagulaSlot;
    private GameObject m_sixthCoagulaSlot;
    private GameObject m_seventhCoagulaSlot;
    private GameObject m_eigthCoagulaSlot;
    private GameObject m_ninethCoagulaSlot;
    private GameObject m_tenthCoagulaSlot;
    private GameObject m_eleventhCoagulaSlot;
    private GameObject m_twelfthCoagulaSlot;
    private GameObject m_thirteenthCoagulaSlot;
    private GameObject m_fourteenthCoagulaSlot;
    private GameObject m_fifteenthCoagulaSlot;
    private GameObject m_sixteenthCoagulaSlot;
    private GameObject m_seventeenthCoagulaSlot;

    private static IngredientData m_lastClickedIngredient;

    private string m_firstCauldronSlotPath = "Canvas/CauldronSlots/TopInventorySlot";
    private string m_secondCauldronSlotPath = "Canvas/CauldronSlots/LeftInventorySlot";
    private string m_thirdCauldronSlotPath = "Canvas/CauldronSlots/RightInventorySlot";
    private string m_fourthCauldronSlotPath = "Canvas/CauldronSlots/BottomInventorySlot";

    private string m_firstCoagulaSlotPath = "Canvas/RightSquareGrid/InventorySlot1";
    private string m_secondCoagulaSlotPath = "Canvas/RightSquareGrid/InventorySlot2";
    private string m_thirdCoagulaSlotPath = "Canvas/RightSquareGrid/InventorySlot3";
    private string m_fourthCoagulaSlotPath = "Canvas/RightSquareGrid/InventorySlot4";
    private string m_fifthCoagulaSlotPath = "Canvas/RightSquareGrid/InventorySlot5";
    private string m_sixthCoagulaSlotPath = "Canvas/RightSquareGrid/InventorySlot6";
    private string m_seventhCoagulaSlotPath = "Canvas/RightSquareGrid/InventorySlot7";
    private string m_eigthCoagulaSlotPath = "Canvas/RightSquareGrid/InventorySlot8";
    private string m_ninethCoagulaSlotPath = "Canvas/RightSquareGrid/InventorySlot9";
    private string m_tenthCoagulaSlotPath = "Canvas/RightHorizontalGrid/InventorySlot1";
    private string m_eleventhCoagulaSlotPath = "Canvas/RightHorizontalGrid/InventorySlot2";
    private string m_twelfthCoagulaSlotPath = "Canvas/RightHorizontalGrid/InventorySlot3";
    private string m_thirteenthCoagulaSlotPath = "Canvas/RightHorizontalGrid/InventorySlot4";
    private string m_fourteenthCoagulaSlotPath = "Canvas/RightHorizontalGrid/InventorySlot5";
    private string m_fifteenthCoagulaSlotPath = "Canvas/RightHorizontalGrid/InventorySlot6";
    private string m_sixteenthCoagulaSlotPath = "Canvas/RightHorizontalGrid/InventorySlot7";
    private string m_seventeenthCoagulaSlotPath = "Canvas/RightHorizontalGrid/InventorySlot8";

    private string m_cauldronIngredientPath = "Prefabs/CauldronIngredient";

    protected static uint m_cauldronPreviousSize = 0;

    private IngredientData LastClickedIngredient { get => m_lastClickedIngredient; set => m_lastClickedIngredient = value; }

    private void Awake()
    {
        m_firstCauldronSlot = GameObject.Find(m_firstCauldronSlotPath);
        m_secondCauldronSlot = GameObject.Find(m_secondCauldronSlotPath);
        m_thirdCauldronSlot = GameObject.Find(m_thirdCauldronSlotPath);
        m_fourthCauldronSlot = GameObject.Find(m_fourthCauldronSlotPath);

        m_firstCoagulaSlot = GameObject.Find(m_firstCoagulaSlotPath);
        m_secondCoagulaSlot = GameObject.Find(m_secondCoagulaSlotPath);
        m_thirdCoagulaSlot = GameObject.Find(m_thirdCoagulaSlotPath);
        m_fourthCoagulaSlot = GameObject.Find(m_fourthCoagulaSlotPath);
        m_fifthCoagulaSlot = GameObject.Find(m_fifthCoagulaSlotPath);
        m_sixthCoagulaSlot = GameObject.Find(m_sixthCoagulaSlotPath);
        m_seventhCoagulaSlot = GameObject.Find(m_seventhCoagulaSlotPath);
        m_eigthCoagulaSlot = GameObject.Find(m_eigthCoagulaSlotPath);
        m_ninethCoagulaSlot = GameObject.Find(m_ninethCoagulaSlotPath);
        m_tenthCoagulaSlot = GameObject.Find(m_tenthCoagulaSlotPath);
        m_eleventhCoagulaSlot = GameObject.Find(m_eleventhCoagulaSlotPath);
        m_twelfthCoagulaSlot = GameObject.Find(m_twelfthCoagulaSlotPath);
        m_thirteenthCoagulaSlot = GameObject.Find(m_thirteenthCoagulaSlotPath);
        m_fourteenthCoagulaSlot = GameObject.Find(m_fourteenthCoagulaSlotPath);
        m_fifteenthCoagulaSlot = GameObject.Find(m_fifteenthCoagulaSlotPath);
        m_sixteenthCoagulaSlot = GameObject.Find(m_sixteenthCoagulaSlotPath);
        m_seventeenthCoagulaSlot = GameObject.Find(m_seventeenthCoagulaSlotPath);

        if (m_firstCauldronSlot == null || m_secondCauldronSlot == null ||
            m_thirdCauldronSlot == null || m_fourthCauldronSlot == null)
        {
            Debug.LogError("One or more Cauldron Slots not found.");
        }

        m_cauldronSlots = new GameObject[4]
        {
            m_firstCauldronSlot,
            m_secondCauldronSlot,
            m_thirdCauldronSlot,
            m_fourthCauldronSlot
        };

        m_coagulaSlots = new GameObject[17]
        {
            m_firstCoagulaSlot, m_secondCoagulaSlot, m_thirdCoagulaSlot,
             m_fourthCoagulaSlot, m_fifthCoagulaSlot, m_sixthCoagulaSlot,
             m_seventhCoagulaSlot, m_eigthCoagulaSlot, m_ninethCoagulaSlot,
             m_tenthCoagulaSlot, m_eleventhCoagulaSlot, m_twelfthCoagulaSlot, m_thirteenthCoagulaSlot,
             m_fourteenthCoagulaSlot, m_fifteenthCoagulaSlot, m_sixteenthCoagulaSlot, m_seventeenthCoagulaSlot
        };

        m_cauldronIngredientPrefab = Resources.Load(m_cauldronIngredientPath) as GameObject;

        if (m_cauldronIngredientPrefab == null)
        {
            Debug.LogError("Cauldron Ingredient Prefab not found.");
        }

        m_receipes = new EIngredient[(int)EIngredient.Count + 1][]
        {
                m_fireReceipe, m_airReceipe, m_earthReceipe, m_waterReceipe, m_etherReceipe, // To add 0-4 elements for the SetReceipe() function // 5 elements
                m_blazeReceipe, m_coalReceipe, m_vaporReceipe, m_fireburstReceipe, m_archeaReceipe, // 5 elements
                m_magmaReceipe, m_magmaReceipeExtended, m_rockReceipe, m_mudReceipe, m_DustReceipe, m_seedReceipe, // 6 elements
                m_steamReceipe, m_steamReceipeExtended, m_pondReceipe, m_pondReceipeExtended, m_puddleReceipe, m_rainReceipe, m_algeaReceipe, // 7 elements
                m_fireboltReceipe, m_fireboltReceipeExtended, m_duststormReceipe, m_duststormReceipeExtended, m_thunderstormReceipe, m_thunderstormReceipeExtended, m_tornadoReceipe, m_fungiReceipe, m_fungiReceipeExtended, // 9 elements
                m_pyroidReceipe, m_pyroidReceipeExtended, m_golemReceipe, m_golemReceipeExtended, m_undineReceipe, m_undineReceipeExtended, m_sylphReceipe, m_sylphReceipeExtended, m_spiritReceipe, // 9 elements
                m_nonCraftable // 1 element 
        };// Total 42 elements
    }

    public void Update()
    {
        // Return if the cauldron is empty
        if (m_ingredientsTransitToCauldron.Count == 0)
        {
            return;
        }

        // If the cauldron is not empty

        // Return if the cauldron size has not changed
        if (m_cauldronPreviousSize == m_ingredientsTransitToCauldron.Count)
        {
            //Debug.Log("Cauldron size has not changed");
            return;
        }

        m_cauldronPreviousSize = (uint)m_ingredientsTransitToCauldron.Count;

        //Debug.Log("Ingerdients in cauldron : " + m_cauldronPreviousSize);

        // If the cauldron size has changed
        UpdateCauldronContent();
    }

    private void UpdateCauldronContent()
    {
        for (int i = 0; i < m_ingredientsTransitToCauldron.Count; i++)
        {
            if (m_cauldronSlots[i] == null)
            {
                Debug.LogError("Cauldron slot is null : " + i);
                continue;
            }

            GameObject cauldronSlot = m_cauldronSlots[i];

            if (cauldronSlot.transform.childCount > 0)
            {
                //Debug.Log("Cauldron slot is not empty");
                continue;
            }

            //Debug.Log("Creating ingredient in cauldron");
            Transform ingredientPrefabTransform = Instantiate(m_cauldronIngredientPrefab, cauldronSlot.transform).GetComponent<Transform>();

            // Transfere the ingredient data from the clicked ingredient to the new ingredient in the cauldron

            if (ingredientPrefabTransform == null)
            {
                Debug.LogError("Ingredient prefab transform is null");
                continue;
            }

            CauldronIngredientInteraction ingredientInteraction = ingredientPrefabTransform.GetComponent<CauldronIngredientInteraction>();

            if (ingredientInteraction == null)
            {
                Debug.LogError("Ingredient interaction is null");
                continue;
            }
            //Transform LastClickedIngredientTransform = LastClickedIngredient.GetComponent<Transform>();
            ingredientInteraction.IngredientData = LastClickedIngredient;
            LastClickedIngredient = null;
        }
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

    if (m_ingredientsTransitToCauldron.Contains(ingredient))
    {
        // Update the cauldron slot ingredients in case they have changed
        UpdateCauldronSlotIngredients();

        // If the cauldron contains the ingredient
        // verify if it's stackable
        // and if there is already a stack in the cauldron

        if (ingredient.isStackable && IsThereStartedStack(ingredient))
        {
            //Debug.Log("Ingredient is stackable and there is already a stack in the cauldron");
            AddIngredientToStartedStack(ingredient);
        }
        return;
    }

    if (m_ingredientsTransitToCauldron.Count == 4)
    {
        Debug.Log("Cauldron is full");
        return;
    }


    // If the ingredient is not in the cauldron
    // add it for the first time

    //Debug.Log("Ingredient in cauldron before add :" + m_ingredientsTransitToCauldron.Count);
    m_lastClickedIngredient = ingredient;
    m_ingredientsTransitToCauldron.Add(ingredient);
    //Debug.Log("Ingredient in cauldron after add :" + m_ingredientsTransitToCauldron.Count);
}

public static void RemoveIngredient(IngredientData ingredientData)
{
    m_ingredientsTransitToCauldron.Remove(ingredientData);
}

protected static void CraftNewIngredient(EIngredient resultingIngredient)
{
    // TODO : Add the new ingredient to the inventory
}

protected static bool CheckCauldronIngredients()
{
    foreach (EIngredient[] receipe in m_receipes)
    {
        if (receipe == null)
        {
            Debug.LogError("Recipe is null");
            continue;
        }

        if (IsCauldronMatchingRecipe(receipe))
        {
            return true;
        }
    }
    return false;
}

protected static EIngredient GetResultingIngredient()
{
    uint index = 0;
    foreach (EIngredient[] receipe in m_receipes)
    {
        index++;
        if (receipe == null)
        {
            Debug.LogError("Recipe is null");
            continue;
        }

        if (IsCauldronMatchingRecipe(receipe))
        {
            Debug.Log("Recipe found");
            return GetResultingIngredient(index);
        }
    }
    return EIngredient.Count;
}

private static bool IsCauldronMatchingRecipe(EIngredient[] receipe)
{
    // Create a dictionary to store the ingredient counts in the cauldron
    Dictionary<EIngredient, uint> cauldronIngredientCountsDictionary = new Dictionary<EIngredient, uint>();

    // Populate the dictionary with the ingredient counts from the cauldronIngredients list
    foreach (Transform ingredientTransorm in m_cauldronSlotsIngredients)
    {
        if (ingredientTransorm.GetComponent<CauldronIngredientInteraction>().IngredientData == null)
        {
            Debug.Log("Ingredient data is null");
        }

        IngredientData ingredientInCauldron = ingredientTransorm.GetComponent<CauldronIngredientInteraction>().IngredientData;

        // If not in the dictionary, put it to 0
        if (!cauldronIngredientCountsDictionary.ContainsKey(ingredientInCauldron.Ingredient))
        {
            cauldronIngredientCountsDictionary[ingredientInCauldron.Ingredient] = 0;
        }

        // Add the quantity of the ingredient to the dictionary
        cauldronIngredientCountsDictionary[ingredientInCauldron.Ingredient] += ingredientTransorm.GetComponent<CauldronIngredientInteraction>().CurrentQuantity;
    }

    // Iterate through the recipe ingredients and try to find a match in the cauldronIngredientCounts dictionary
    foreach (EIngredient ingredient in receipe)
    {
        // If the ingredient is not in the dictionary or the quantity is 0 or the ingredient is Count (see basic ingredients receipe array's commentary -> m_fireReceipe)
        if (!cauldronIngredientCountsDictionary.ContainsKey(ingredient) || cauldronIngredientCountsDictionary[ingredient] <= 0 || ingredient == EIngredient.Count)
        {
            return false;
        }

        cauldronIngredientCountsDictionary[ingredient]--;
    }

    return true;
}

private static EIngredient GetResultingIngredient(uint indexOfReceipe)
{
    switch (indexOfReceipe)
    {
        case (int)EIngredient.Blaze:
            return EIngredient.Blaze;

        case (int)EIngredient.Coal:
            return EIngredient.Coal;

        case (int)EIngredient.Vapor:
            return EIngredient.Vapor;

        case (int)EIngredient.Fireburst:
            return EIngredient.Fireburst;

        case (int)EIngredient.Archea:
            return EIngredient.Archea;

        case (int)EIngredient.Magma:
        case (int)EIngredient.MagmaExtended:
            return EIngredient.Magma;

        case (int)EIngredient.Rock:
            return EIngredient.Rock;

        case (int)EIngredient.Mud:
            return EIngredient.Mud;

        case (int)EIngredient.Dust:
            return EIngredient.Dust;

        case (int)EIngredient.Seed:
            return EIngredient.Seed;

        case (int)EIngredient.Steam:
        case (int)EIngredient.SteamExtended:
            return EIngredient.Steam;

        case (int)EIngredient.Pond:
        case (int)EIngredient.PondExtended:
            return EIngredient.Pond;

        case (int)EIngredient.Puddle:
            return EIngredient.Puddle;

        case (int)EIngredient.Rain:
            return EIngredient.Rain;

        case (int)EIngredient.Algae:
            return EIngredient.Algae;

        case (int)EIngredient.Firebolt:
        case (int)EIngredient.FireboltExtended:
            return EIngredient.Firebolt;

        case (int)EIngredient.Duststorm:
        case (int)EIngredient.DuststormExtended:
            return EIngredient.Duststorm;

        case (int)EIngredient.Thunderstorm:
        case (int)EIngredient.ThunderstormExtended:
            return EIngredient.Thunderstorm;

        case (int)EIngredient.Tornado:
            return EIngredient.Tornado;

        case (int)EIngredient.Fungi:
        case (int)EIngredient.FungiExtended:
            return EIngredient.Fungi;

        case (int)EIngredient.Pyroid:
        case (int)EIngredient.PyroidExtended:
            return EIngredient.Pyroid;

        case (int)EIngredient.Golem:
        case (int)EIngredient.GolemExtended:
            return EIngredient.Golem;

        case (int)EIngredient.Undine:
        case (int)EIngredient.UndineExtended:
            return EIngredient.Undine;

        case (int)EIngredient.Sylph:
        case (int)EIngredient.SylphExtended:
            return EIngredient.Sylph;

        case (int)EIngredient.Spirit:
            return EIngredient.Spirit;

        default:
            Debug.LogError("No ingredient found for receipe index : " + indexOfReceipe);
            return EIngredient.Count;

    }
}

private static bool IsThereStartedStack(IngredientData addedIngredient)
{
    foreach (Transform prefabTransform in m_cauldronSlotsIngredients)
    {
        // If the slot is empty
        if (prefabTransform == null)
        {
            continue;
        }

        if (prefabTransform.GetComponent<CauldronIngredientInteraction>().IngredientData == null)
        {
            Debug.Log("Ingredient data is null");
        }

        IngredientData ingredientInCauldron = prefabTransform.GetComponent<CauldronIngredientInteraction>().IngredientData;

        // If the added ingredient is not in the the same as the ingredient in the cauldron
        if (ingredientInCauldron.Ingredient != addedIngredient.Ingredient)
        {
            continue;
        }

        // If the ingredient is the same but has reached the max quantity
        if (prefabTransform.GetComponent<CauldronIngredientInteraction>().CurrentQuantity == ingredientInCauldron.MaxQuantity)
        {
            continue;
        }

        // else return true if the ingredient is the same and has not reached the max quantity
        else
        {
            return true;
            //prefabTransform.GetComponent<IngredientInteraction>().m_quantity;
        }
    }
    return false;
}

private static void AddIngredientToStartedStack(IngredientData addedIngredient)
{
    foreach (Transform prefabTransform in m_cauldronSlotsIngredients)
    {
        // If the slot is empty
        if (prefabTransform == null)
        {
            continue;
        }

        IngredientData ingredientInCauldron = prefabTransform.GetComponent<CauldronIngredientInteraction>().IngredientData;

        // If the added ingredient is not in the the same as the ingredient in the cauldron
        if (ingredientInCauldron.Ingredient != addedIngredient.Ingredient)
        {
            continue;
        }

        // If the ingredient is the same but as reached the max quantity
        if (prefabTransform.GetComponent<CauldronIngredientInteraction>().CurrentQuantity == ingredientInCauldron.MaxQuantity)
        {
            continue;
        }

        // else increment and return if the ingredient is the same and has not reached the max quantity
        else
        {
            //Debug.Log("Incrementing ingredient");
            prefabTransform.GetComponent<CauldronIngredientInteraction>().CurrentQuantity++;
            return;
        }
    }
}

protected static void UpdateCauldronSlotIngredients()
{
    m_cauldronSlotsIngredients.Clear();

    foreach (GameObject prefabTransform in m_cauldronSlots)
    {
        if (prefabTransform.transform.childCount == 0)
        {
            continue;
        }

        Transform ingredient = prefabTransform.transform.GetChild(0);
        if (ingredient != null)
        {
            m_cauldronSlotsIngredients.Add(ingredient.GetComponent<Transform>());
        }
    }
}
}