
using UnityEngine;
using static IngredientManager;

public class IngredientPool : MonoBehaviour
{
    [SerializeField] private IngredientData m_ingredientData;

    public void CollectIngredient()
    {
        EIngredient ingredientType = m_ingredientData.Ingredient;
        //GetInstance().AddIngredient(ingredientType);
    }
}