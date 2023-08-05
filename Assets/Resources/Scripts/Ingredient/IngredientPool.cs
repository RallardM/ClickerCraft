using System.Collections.Generic;
using System.Linq;
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

    public static void AddIngredientToTransitPool(IngredientData ingredient, EUiSlotContainer uiSlotContainer)
    {
        if (ingredient == null)
        {
            Debug.LogError("Ingredient is null");
            return;
        }

        List <IngredientData> ingredientToTransitPool = GetTransitPool(uiSlotContainer);

        // Update the container slot ingredients in case they have changed
        UpdateContainerIngredients(CauldronIngredientTransforms, uiSlotContainer);

        // If the container contains the ingredient
        // verify if it's stackable
        // and if there is already a stack in the container
        bool isStackable = ingredient.isStackable;
        bool isThereASmallEnoughStack = IsThereASmallEnoughStack(ingredient, uiSlotContainer);

        if (isStackable && isThereASmallEnoughStack)
        {

            bool isSumSmallerThanMaxQuantity = GetStartedStack(ingredient, uiSlotContainer).GetComponent<IngredientInteraction>().CurrentQuantity + LastClickedIngredientQuantity <= ingredient.MaxQuantity;

            if (isSumSmallerThanMaxQuantity)
            {
                //Debug.Log("Ingredient is stackable and there is already a stack in the container");
                AddIngredientToStartedStack(ingredient, uiSlotContainer);
                return;
            }
        }

        if (ingredientToTransitPool.Count == GetContainer(uiSlotContainer).Count())
        {
            Debug.Log("The container is full");
            return;
        }

        // If the ingredient is not in the container
        // add it for the first time
        //Debug.Log("Ingredient added to transit list, list count before: " + ingredientToTransitPool.Count);
        LastClickedIngredient = ingredient;
        ingredientToTransitPool.Add(ingredient);
        //Debug.Log("Ingredient added to transit list, list count after: " + ingredientToTransitPool.Count);
    }

    public static void UpdateContainerIngredients(List<Transform> ingredientPool, EUiSlotContainer uiSlotContainer)
    {
        if (ingredientPool == null)
        {
            Debug.LogError("Ingredient pool is null");
            return;
        }

        if (ingredientPool.Count == 0)
        {
            //Debug.Log("Ingredient pool is empty");
            return;
        }

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

    public static void RemoveIngredientFromTransitPool(IngredientData ingredientData, EUiSlotContainer uiSlotContainer)
    {
        switch (uiSlotContainer)
        {
            case EUiSlotContainer.Solve:
                //Debug.Log("Remove ingredient from solve");
                IngredientsTransitToSolve.Remove(ingredientData);
                break;

            case EUiSlotContainer.Cauldron:
                //Debug.Log("Remove ingredient from cauldron");
                IngredientsTransitToCauldron.Remove(ingredientData);
                break;

            case EUiSlotContainer.Coagula:
                //Debug.Log("Remove ingredient from coagula");
                IngredientsTransitToCoagula.Remove(ingredientData);
                break;

            default:
                Debug.LogError("Invalid container");
                break;
        }
    }

    public static void RemoveIngredientFromTransformPool(Transform ingredientTransform, EUiSlotContainer uiSlotContainer)
    {
        switch (uiSlotContainer)
        {
            case EUiSlotContainer.Solve:
                SolveIngredientTransforms.Remove(ingredientTransform);
                break;

            case EUiSlotContainer.Cauldron:
                CauldronIngredientTransforms.Remove(ingredientTransform);
                break;

            case EUiSlotContainer.Coagula:
                CoagulaIngredientTransforms.Remove(ingredientTransform);
                break;

            default:
                Debug.LogError("No pool found for this slot : " + uiSlotContainer);
                break;
        }
    }

    private static bool IsThereASmallEnoughStack(IngredientData addedIngredient, EUiSlotContainer uiSlotContainer)
    {
        List<Transform> ingredientTransformPool = GetInGameIngredientsTransformPool(uiSlotContainer);
        foreach (Transform prefabTransform in ingredientTransformPool)
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
            }
        }

        return false;
    }

    private static Transform GetStartedStack(IngredientData addedIngredient, EUiSlotContainer uiSlotContainer)
    {
        List<Transform> ingredientTransformPool = GetInGameIngredientsTransformPool(uiSlotContainer);
        foreach (Transform prefabTransform in ingredientTransformPool)
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

            IngredientData ingredientInContainer = prefabTransform.GetComponent<IngredientInteraction>().IngredientData;

            // If the added ingredient is not in the the same as the ingredient in the cauldron
            if (ingredientInContainer.Ingredient != addedIngredient.Ingredient)
            {
                continue;
            }

            // If the ingredient is the same but has reached the max quantity
            if (prefabTransform.GetComponent<IngredientInteraction>().CurrentQuantity == ingredientInContainer.MaxQuantity)
            {
                continue;
            }

            // else return true if the ingredient is the same and has not reached the max quantity
            else
            {
                return prefabTransform;
            }
        }

        return null;
    }

    private static void AddIngredientToStartedStack(IngredientData addedIngredient, EUiSlotContainer uiSlotContainer)
    {
        foreach (Transform prefabTransform in GetInGameIngredientsTransformPool(uiSlotContainer))
        {
            // If the slot is empty
            if (prefabTransform == null)
            {
                continue;
            }

            IngredientData ingredientInContainer = prefabTransform.GetComponent<IngredientInteraction>().IngredientData;

            // If the added ingredient is not in the the same as the ingredient in the container
            if (ingredientInContainer.Ingredient != addedIngredient.Ingredient)
            {
                continue;
            }

            // If the ingredient is the same but as reached the max quantity
            if (prefabTransform.GetComponent<IngredientInteraction>().CurrentQuantity == ingredientInContainer.MaxQuantity)
            {
                continue;
            }

            // else increment and return if the ingredient is the same and has not reached the max quantity
            //else
            //{
            //    //Debug.Log("Incrementing ingredient");
            //    prefabTransform.GetComponent<IngredientInteraction>().CurrentQuantity++;
            //    return;
            //}

            if (prefabTransform.GetComponent<IngredientInteraction>().CurrentQuantity + LastClickedIngredientQuantity <= ingredientInContainer.MaxQuantity)
            {
                Debug.Log("Adding ingredient to the container slot");
                prefabTransform.GetComponent<IngredientInteraction>().CurrentQuantity += LastClickedIngredientQuantity;
                LastClickedIngredient = null;
                LastClickedIngredientQuantity = 0;
            }
        }
    }

    public static void UpdateContainerContent(EUiSlotContainer uiSlotContainer)
    {
        List<IngredientData> ingredientToTransitPool = GetTransitPool(uiSlotContainer);

        // Return if the list to transfrer to the container is empty
        if (ingredientToTransitPool.Count == 0)
        {
            return;
        }

        // If the container is not empty
        //Debug.Log("The container " + uiSlotContainer.ToString() + " is not empty");

        // Return if the quantity in the list of ingredient to transfer has not changed
        uint previousIngredientCount = GetContainerPreviousIngredientCount(uiSlotContainer);
        int transitPoolCount = ingredientToTransitPool.Count;
        if (previousIngredientCount == transitPoolCount)
        {
            return;
        }

        //Debug.Log("Container quantity has changed : " + uiSlotContainer.ToString());

        // If the quantity in the list of ingredient to transfer has changed
        // and has increased (meaning that an ingredient needs to be physically added)
        // Update previous ingredient count to the current count 
        // PreviousIngredientCount serves as a way to know if the list
        // of ingredient to transfer has changed since the last time it was updated here :
        SetContainerPreviousIngredientCount(uiSlotContainer, (uint)GetTransitPool(uiSlotContainer).Count);

        for (int i = 0; i < ingredientToTransitPool.Count; i++)
        {
            if (GetContainerSlotFromIndex(uiSlotContainer, i) == null)
            {
                Debug.LogError("Container slot is null : " + i);
                continue;
            }

            GameObject containerSlot = GetContainerSlotFromIndex(uiSlotContainer, i);

            // if the container slot is already occupied by a ingredient
            if (containerSlot.transform.childCount > 0)
            {
                Debug.Log("Container slot is already occupied by an ingredient");
                continue;
            }

            //Debug.Log("Instantiating ingredient in the container slot");

            // Create the ingredient in the container slot that is empty
            Transform ingredientPrefabTransform = Instantiate(GetIngredientPrefab(), containerSlot.transform).GetComponent<Transform>();
            SetInGameIngredientsTransformPool(ingredientPrefabTransform, uiSlotContainer);

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

            // Transfere the ingredient data from the clicked ingredient to the new ingredient created in the current container
            //Debug.Log("Transfering ingredient data of : " + LastClickedIngredient + " to : " + uiSlotContainer.ToString());
            ingredientInteraction.IngredientData = LastClickedIngredient;
            ingredientInteraction.CurrentQuantity = LastClickedIngredientQuantity;
            LastClickedIngredient = null;
            LastClickedIngredientQuantity = 0;
        }
    }

    public static List<IngredientData> GetTransitPool(EUiSlotContainer uiSlotContainer)
    {
        switch (uiSlotContainer)
        {
            case EUiSlotContainer.Solve:
                //Debug.Log("Returning IngredientsTransitToSolve");
                return IngredientsTransitToSolve;

            case EUiSlotContainer.Cauldron:
                //Debug.Log("Returning IngredientsTransitToCauldron");
                return IngredientsTransitToCauldron;

            case EUiSlotContainer.Coagula:
                //Debug.Log("Returning IngredientsTransitToCoagula");
                return IngredientsTransitToCoagula;

            default:
                Debug.LogError("No pool found for this slot");
                return null;
        }
    }

    private static void SetInGameIngredientsTransformPool(Transform ingredientTransform, EUiSlotContainer uiSlotContainer)
    {
        switch (uiSlotContainer)
        {
            case EUiSlotContainer.Solve:
                SolveIngredientTransforms.Add(ingredientTransform);
                break;

            case EUiSlotContainer.Cauldron:
                CauldronIngredientTransforms.Add(ingredientTransform);
                break;

            case EUiSlotContainer.Coagula:
                CoagulaIngredientTransforms.Add(ingredientTransform);
                break;

            default:
                Debug.LogError("No pool found for this slot");
                break;
        }
    }

    private static List<Transform> GetInGameIngredientsTransformPool(EUiSlotContainer uiSlotContainer)
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
                Debug.LogError("No pool found for this slot");
                return null;
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
