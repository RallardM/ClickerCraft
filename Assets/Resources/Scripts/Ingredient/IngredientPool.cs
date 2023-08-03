
using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static UIManager;

public class IngredientPool : IngredientManager
{
    private static List<IngredientData> m_ingredientsTransitToSolve = new List<IngredientData>();
    private static List<IngredientData> m_ingredientsTransitToCauldron = new List<IngredientData>();
    private static List<IngredientData> m_ingredientsTransitToCoagula = new List<IngredientData>();

    private static List<Transform> m_solveIngredientTransforms = new List<Transform>();
    private static List<Transform> m_cauldronIngredientTransforms = new List<Transform>();
    private static List<Transform> m_coagulaIngredientTransforms = new List<Transform>();

    public static List<IngredientData> IngredientsTransitToSolve { get { return m_ingredientsTransitToSolve; } set { m_ingredientsTransitToSolve = value; } }
    public static List<IngredientData> IngredientsTransitToCauldron { get { return m_ingredientsTransitToCauldron; } set { m_ingredientsTransitToCauldron = value; } }
    public static List<IngredientData> IngredientsTransitToCoagula { get { return m_ingredientsTransitToCoagula; } set { m_ingredientsTransitToCoagula = value; } }

    public static List<Transform> SolveIngredientTransforms { get { return m_solveIngredientTransforms; } set { m_solveIngredientTransforms = value; } }
    public static List<Transform> CauldronIngredientTransforms { get { return m_cauldronIngredientTransforms; } set { m_cauldronIngredientTransforms = value; } }
    public static List<Transform> CoagulaIngredientTransforms { get { return m_coagulaIngredientTransforms; } set { m_coagulaIngredientTransforms = value; } }

    public void Update()
    {
        UpdateContainerContent(EUiSlotContainer.Solve);
        UpdateContainerContent(EUiSlotContainer.Cauldron);
        UpdateContainerContent(EUiSlotContainer.Coagula);
    }

    public static void AddIngredient(IngredientData ingredient, EUiSlotContainer uiSlotContainer)
    {
        if (ingredient == null)
        {
            Debug.LogError("Ingredient is null");
            return;
        }

        if (GetTransitPool(uiSlotContainer).Contains(ingredient))
        {
            // Update the cauldron slot ingredients in case they have changed
            UpdateSlotIngredients(CauldronIngredientTransforms, uiSlotContainer);

            // If the cauldron contains the ingredient
            // verify if it's stackable
            // and if there is already a stack in the cauldron

            if (ingredient.isStackable && IsThereStartedStack(ingredient, uiSlotContainer))
            {
                //Debug.Log("Ingredient is stackable and there is already a stack in the cauldron");
                AddIngredientToStartedStack(ingredient, uiSlotContainer);
            }
            return;
        }

        if (GetTransitPool(uiSlotContainer).Count == 4)
        {
            Debug.Log("Cauldron is full");
            return;
        }

        // If the ingredient is not in the cauldron
        // add it for the first time

        //Debug.Log("Ingredient in cauldron before add :" + IngredientsTransitToCauldron.Count);
        LastClickedIngredient = ingredient;
        GetTransitPool(uiSlotContainer).Add(ingredient);
        //Debug.Log("Ingredient in cauldron after add :" + IngredientsTransitToCauldron.Count);
    }

    public static void UpdateSlotIngredients(List<Transform> ingredientPool, EUiSlotContainer uiSlotContainer)
    {
        ingredientPool.Clear();

        foreach (GameObject prefabTransform in GetIngredientsParentSlotsPool(uiSlotContainer))
        {
            if (prefabTransform.transform.childCount == 0)
            {
                continue;
            }

            Transform ingredient = prefabTransform.transform.GetChild(0);
            if (ingredient != null)
            {
                ingredientPool.Add(ingredient.GetComponent<Transform>());
            }
        }
    }

    public static void RemoveIngredient(IngredientData ingredientData, EUiSlotContainer uiSlotContainer)
    {
        switch (uiSlotContainer)
        {
            case EUiSlotContainer.Solve:
                IngredientsTransitToSolve.Remove(ingredientData);
                break;

            case EUiSlotContainer.Cauldron:
                IngredientsTransitToCauldron.Remove(ingredientData);
                break;

            case EUiSlotContainer.Coagula:
                IngredientsTransitToCoagula.Remove(ingredientData);
                break;

            default:
                break;
        }
    }

    protected static bool IsThereStartedStack(IngredientData addedIngredient, EUiSlotContainer uiSlotContainer)
    {
        foreach (Transform prefabTransform in GetInGameIngredientsTransformPool(uiSlotContainer))
        {
            // If the slot is empty
            if (prefabTransform == null)
            {
                continue;
            }

            if (prefabTransform.GetComponent<IngredientInteraction>().IngredientData == null)
            {
                Debug.Log("Ingredient data is null");
            }

            IngredientData ingredientInCauldron = prefabTransform.GetComponent<IngredientInteraction>().IngredientData;

            // If the added ingredient is not in the the same as the ingredient in the cauldron
            if (ingredientInCauldron.Ingredient != addedIngredient.Ingredient)
            {
                continue;
            }

            // If the ingredient is the same but has reached the max quantity
            if (prefabTransform.GetComponent<IngredientInteraction>().CurrentQuantity == ingredientInCauldron.MaxQuantity)
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

    //protected static bool IsThereStartedStack(IngredientData addedIngredient)
    //{
    //    foreach (Transform prefabTransform in CauldronIngredientTransforms)
    //    {
    //        // If the slot is empty
    //        if (prefabTransform == null)
    //        {
    //            continue;
    //        }

    //        if (prefabTransform.GetComponent<CauldronIngredientInteraction>().IngredientData == null)
    //        {
    //            Debug.Log("Ingredient data is null");
    //        }

    //        IngredientData ingredientInCauldron = prefabTransform.GetComponent<CauldronIngredientInteraction>().IngredientData;

    //        // If the added ingredient is not in the the same as the ingredient in the cauldron
    //        if (ingredientInCauldron.Ingredient != addedIngredient.Ingredient)
    //        {
    //            continue;
    //        }

    //        // If the ingredient is the same but has reached the max quantity
    //        if (prefabTransform.GetComponent<CauldronIngredientInteraction>().CurrentQuantity == ingredientInCauldron.MaxQuantity)
    //        {
    //            continue;
    //        }

    //        // else return true if the ingredient is the same and has not reached the max quantity
    //        else
    //        {
    //            return true;
    //            //prefabTransform.GetComponent<IngredientInteraction>().m_quantity;
    //        }
    //    }
    //    return false;
    //}


    private static void AddIngredientToStartedStack(IngredientData addedIngredient, EUiSlotContainer uiSlotContainer)
    {
        foreach (Transform prefabTransform in GetInGameIngredientsTransformPool(uiSlotContainer))
        {
            // If the slot is empty
            if (prefabTransform == null)
            {
                continue;
            }

            IngredientData ingredientInCauldron = prefabTransform.GetComponent<IngredientInteraction>().IngredientData;

            // If the added ingredient is not in the the same as the ingredient in the cauldron
            if (ingredientInCauldron.Ingredient != addedIngredient.Ingredient)
            {
                continue;
            }

            // If the ingredient is the same but as reached the max quantity
            if (prefabTransform.GetComponent<IngredientInteraction>().CurrentQuantity == ingredientInCauldron.MaxQuantity)
            {
                continue;
            }

            // else increment and return if the ingredient is the same and has not reached the max quantity
            else
            {
                //Debug.Log("Incrementing ingredient");
                prefabTransform.GetComponent<IngredientInteraction>().CurrentQuantity++;
                return;
            }
        }
    }

    private void UpdateContainerContent(EUiSlotContainer uiSlotContainer)
    {
        // Return if the list to transfrer to the container is empty
        if (GetTransitPool(uiSlotContainer).Count == 0)
        {
            return;
        }

        // If the container is not empty

        // Return if the quantity in the list of ingredient to transfer has not changed
        if (GetContainerPreviousIngredientCount(uiSlotContainer) == GetTransitPool(uiSlotContainer).Count)
        {
            return;
        }

        // If the quantity in the list of ingredient to transfer has changed
        // Update previous ingredient count to the current count 
        // PreviousIngredientCount serves as a way to know if the list of ingredient to transfer has changed
        // since the last time it was updated here :
        SetContainerPreviousIngredientCount(uiSlotContainer, (uint)IngredientPool.IngredientsTransitToCauldron.Count);

        for (int i = 0; i < GetTransitPool(uiSlotContainer).Count; i++)
        {
            if (GetContainerSlotFromIndex(uiSlotContainer, i) == null) 
            {
                Debug.LogError("Container slot is null : " + i); 
                continue;
            }

            GameObject containerSlot = GetContainerSlotFromIndex(uiSlotContainer, i); 

            if (containerSlot.transform.childCount > 0)
            {
                //Debug.Log("Container slot is not empty");
                continue;
            }

            //Debug.Log("Creating ingredient in cauldron"); 
            Transform ingredientPrefabTransform = Instantiate(GetIngredientPrefab(), containerSlot.transform).GetComponent<Transform>();

            // Transfere the ingredient data from the clicked ingredient to the new ingredient in the container

            if (ingredientPrefabTransform == null)
            {
                Debug.LogError("Ingredient prefab transform is null");
                continue;
            }

            IngredientInteraction ingredientInteraction = ingredientPrefabTransform.GetComponent<IngredientInteraction>();

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

    private static List<IngredientData> GetTransitPool(EUiSlotContainer uiSlotContainer)
    {
        switch (uiSlotContainer)
        {
            case EUiSlotContainer.Solve:
                return IngredientsTransitToSolve;

            case EUiSlotContainer.Cauldron:
                return IngredientsTransitToCauldron;

            case EUiSlotContainer.Coagula:
                return IngredientsTransitToCoagula;

            default:
                Debug.LogError("No pool found for this slot");
                return null;
        }
    }

    private static IEnumerable<Transform> GetInGameIngredientsTransformPool(EUiSlotContainer uiSlotContainer)
    {
        switch (uiSlotContainer)
        {
            case EUiSlotContainer.Solve:
                return SolveIngredientTransforms;

            case EUiSlotContainer.Cauldron:
                return CauldronIngredientTransforms;

            case EUiSlotContainer.Coagula:
                return CoagulaIngredientTransforms;

            default:
                throw new ArgumentOutOfRangeException(nameof(uiSlotContainer), uiSlotContainer, null);
        }
    }

    private static IEnumerable<GameObject> GetIngredientsParentSlotsPool(EUiSlotContainer uiSlotContainer)
    {
        switch (uiSlotContainer)
        {
            case EUiSlotContainer.Solve:
                return SolveSlots;

            case EUiSlotContainer.Cauldron:
                return CauldronSlots;

            case EUiSlotContainer.Coagula:
                return CoagulaSlots;

            default:
                return null;
        }
    }
}
