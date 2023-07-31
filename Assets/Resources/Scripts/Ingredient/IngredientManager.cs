
using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    private static List<IngredientData> m_ingredientsTransitToCauldron = new List<IngredientData>();
    private static List<GameObject> m_cauldronSlots = new List<GameObject>();
    private static List<Transform> m_cauldronSlotsIngredients = new List<Transform>();

    public static readonly Array[] m_receipes = new EIngredient[][]
    {
        m_fireReceipe, m_airReceipe, m_earthReceipe, m_waterReceipe, m_etherReceipe, // To add 0-4 elements for the SetReceipe() function
        m_blazeReceipe, m_coalReceipe, m_vaporReceipe, m_fireburstReceipe, m_archeaReceipe,
        m_magmaReceipe, m_magmaReceipeExtended, m_rockReceipe, m_mudReceipe, m_DustReceipe, m_seedReceipe,
        m_steamReceipe, m_steamReceipeExtended, m_pondReceipe, m_pondReceipeExtended, m_puddleReceipe, m_rainReceipe, m_algeaReceipe,
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
    private static readonly EIngredient[] m_fireReceipe = new EIngredient[1] { EIngredient.Fire }; // To add 0-4 elements for the SetReceipe() function
    private static readonly EIngredient[] m_airReceipe = new EIngredient[1] { EIngredient.Air }; // To add 0-4 elements for the SetReceipe() function
    private static readonly EIngredient[] m_earthReceipe = new EIngredient[1] { EIngredient.Earth }; // To add 0-4 elements for the SetReceipe() function
    private static readonly EIngredient[] m_waterReceipe = new EIngredient[1] { EIngredient.Water }; // To add 0-4 elements for the SetReceipe() function
    private static readonly EIngredient[] m_etherReceipe = new EIngredient[1] { EIngredient.Ether }; // To add 0-4 elements for the SetReceipe() function
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

    private static IngredientData m_lastClickedIngredient;

    private string m_firstCauldronSlotPath = "Canvas/CauldronSlots/TopInventorySlot";
    private string m_secondCauldronSlotPath = "Canvas/CauldronSlots/LeftInventorySlot";
    private string m_thirdCauldronSlotPath = "Canvas/CauldronSlots/RightInventorySlot";
    private string m_fourthCauldronSlotPath = "Canvas/CauldronSlots/BottomInventorySlot";

    private string m_cauldronIngredientPath = "Prefabs/CauldronIngredient";

    protected static uint m_cauldronPreviousSize = 0;

    private IngredientData LastClickedIngredient { get => m_lastClickedIngredient; set => m_lastClickedIngredient = value; }

    private void Awake()
    {
        m_firstCauldronSlot = GameObject.Find(m_firstCauldronSlotPath);
        m_secondCauldronSlot = GameObject.Find(m_secondCauldronSlotPath);
        m_thirdCauldronSlot = GameObject.Find(m_thirdCauldronSlotPath);
        m_fourthCauldronSlot = GameObject.Find(m_fourthCauldronSlotPath);

        if (m_firstCauldronSlot == null || m_secondCauldronSlot == null ||
            m_thirdCauldronSlot == null || m_fourthCauldronSlot == null)
        {
            Debug.LogError("One or more Cauldron Slots not found.");
        }

        m_cauldronSlots = new List<GameObject>
        {
            m_firstCauldronSlot,
            m_secondCauldronSlot,
            m_thirdCauldronSlot,
            m_fourthCauldronSlot
        };

        m_cauldronIngredientPrefab = Resources.Load(m_cauldronIngredientPath) as GameObject;

        if (m_cauldronIngredientPrefab == null)
        {
            Debug.LogError("Cauldron Ingredient Prefab not found.");
        }
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

        for (int i = 0; i < m_ingredientsTransitToCauldron.Count; i++)
        {
            GameObject cauldronSlot = m_cauldronSlots[i];

            if (cauldronSlot == null)
            {
                Debug.LogError("Cauldron slot is null");
                continue;
            }

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

    public static void MixIngredients()
    {

        if(CheckCauldronIngredients())
        {
            Debug.Log("Ingredients are valid");
        }
        else
        {
            Debug.Log("Ingredients are not valid");
        }

        //uint ingredientCount = GetIngredientsInCauldronCount();

        //for (int i = 0; i < ingredientCount; i++)
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

    //private static uint GetIngredientsInCauldronCount()
    //{
    //    uint total = 0;

    //    UpdateCauldronSlotIngredients();
    //    foreach (Transform prefabTransform in m_cauldronSlotsIngredients)
    //    {
    //        //IngredientData ingredientInCauldron = prefabTransform.GetComponent<IngredientInteraction>().GetIngredientData();

    //        // If the slot is empty
    //        //if (ingredientInCauldron == null)
    //        if (prefabTransform == null)
    //        {
    //            continue;
    //        }

    //        total += prefabTransform.GetComponent<IngredientInteraction>().CurrentQuantity;
    //    }

    //    return total;
    //}

    private static bool CheckCauldronIngredients()
    {
        foreach (EIngredient[] recipe in m_receipes)
        {
            if (IsCauldronMatchingRecipe(recipe))
            {
                return true;
            }
        }
        return false;
    }

    private static bool IsCauldronMatchingRecipe(EIngredient[] recipe)
    {
        // Create a dictionary to store the ingredient counts in the cauldron
        Dictionary<EIngredient, uint> cauldronIngredientCounts = new Dictionary<EIngredient, uint>();

        // Update the cauldron slot ingredients in case they have changed
        UpdateCauldronSlotIngredients();

        // Populate the dictionary with the ingredient counts from the cauldronIngredients list
        foreach (Transform ingredientTransorm in m_cauldronSlotsIngredients)
        {
            // If not in the dictionary, put it to 0
            if (!cauldronIngredientCounts.ContainsKey(ingredientTransorm.GetComponent<IngredientData>().Ingredient))
            {
                cauldronIngredientCounts[ingredientTransorm.GetComponent<IngredientData>().Ingredient] = 0;
            }

            // Add the quantity of the ingredient to the dictionary
            cauldronIngredientCounts[ingredientTransorm.GetComponent<IngredientData>().Ingredient] += ingredientTransorm.GetComponent<CauldronIngredientInteraction>().CurrentQuantity;
        }

        // Iterate through the recipe ingredients and try to find a match in the cauldronIngredientCounts dictionary
        foreach (EIngredient ingredient in recipe)
        {
            if (!cauldronIngredientCounts.ContainsKey(ingredient) || cauldronIngredientCounts[ingredient] <= 0)
            {
                return false;
            }
            cauldronIngredientCounts[ingredient]--;
        }

        return true;
    }

    //private bool IsCauldronMatchingRecipe(EIngredient[] recipe)
    //{
    //    if (GetIngredientsInCauldronCount() != recipe.Length)
    //    {
    //        return false;
    //    }

    //    // Source : https://discussions.unity.com/t/copying-one-list-to-another-list/99515
    //    // Create a copy of the cauldron ingredients list
    //    UpdateCauldronSlotIngredients();
    //    List<Transform> cauldronCopy = new List<Transform>(m_cauldronSlotsIngredients);

    //    // Iterate through the recipe ingredients and try to find a match in the cauldron copy
    //    foreach (EIngredient ingredient in recipe)
    //    {
    //        bool found = false;
    //        for (int i = 0; i < GetIngredientsInCauldronCount(); i++)
    //        {
    //            if (cauldronCopy[i].GetComponent<IngredientData>().Ingredient == ingredient)
    //            {
    //                // Mark the ingredient as found and remove it from the cauldron copy
    //                found = true;
    //                cauldronCopy.RemoveAt(i);
    //                break;
    //            }
    //        }

    //        if (!found)
    //        {
    //            return false;
    //        }
    //    }

    //    return true;
    //}

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

    private static void UpdateCauldronSlotIngredients()
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