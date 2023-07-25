using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    private static IngredientManager _Instance;

    private static uint m_fire = 0;

    public enum EBasicIngredient
    {
        Fire,
        Air,
        Earth,
        Water,
        Ether,
        Count
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
        Debug.Log("Add ingredient : " + ingredient.Name);

        m_fire++;
    }
}
