using System;
using System.Collections.Generic;
using UnityEngine;
using static IngredientManager;

[CreateAssetMenu(fileName = "Ingredient", menuName = "ScriptableObjects/IngredientData")]

public class IngredientData : ScriptableObject
{
    public string Name;
    public Sprite Sprite;
    public uint ID;
    private static uint s_ID;
    public uint MaxQuantity;
    public EIngredient Ingredient;
    public bool isCraftable;
    public bool isStackable;

    private void Awake()
    {
        Name = Ingredient.ToString();
        ID = s_ID++;

        if (m_receipes == null)
        {
            LoadReceipes();
        }
    }
}