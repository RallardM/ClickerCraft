
using System.Collections.Generic;
using UnityEngine;

public class IngredientPool : IngredientManager
{
    private static List<IngredientData> m_ingredientsTransitToCauldron = new List<IngredientData>();
    //[SerializeField] private IngredientData m_ingredientData;

    //public void CollectIngredient()
    //{
    //    EIngredient ingredientType = m_ingredientData.Ingredient;
    //    //GetInstance().AddIngredient(ingredientType);
    //}

    public static List<IngredientData> IngredientsTransitToCauldron { get { return m_ingredientsTransitToCauldron; } set { m_ingredientsTransitToCauldron = value; } }

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
        LastClickedIngredient = ingredient;
        m_ingredientsTransitToCauldron.Add(ingredient);
        //Debug.Log("Ingredient in cauldron after add :" + m_ingredientsTransitToCauldron.Count);
    }

    public static void RemoveIngredient(IngredientData ingredientData)
    {
        m_ingredientsTransitToCauldron.Remove(ingredientData);
    }

}
