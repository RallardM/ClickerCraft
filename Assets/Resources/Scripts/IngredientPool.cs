
using UnityEngine;
using static IngredientManager;

public class IngredientPool : MonoBehaviour
{
    [SerializeField] private IngredientData m_ingredientData;

    public void CollectIngredient()
    {
        EBasicIngredient ingredientType = m_ingredientData.IngredientType;
        //GetInstance().AddIngredient(ingredientType);
    }
}
