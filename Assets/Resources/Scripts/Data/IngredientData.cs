using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static IngredientManager;

[CreateAssetMenu(fileName = "Ingredient", menuName = "ScriptableObjects/IngredientData")]

public class IngredientData : ScriptableObject
{
    private string Name;
    //public string Description;
    public Sprite Sprite;
    public int MaxQuantity;
    public EIngredient Ingredient;
    public List<EIngredient> Receipe = new List<EIngredient>();
    public bool isCraftable;
    public bool isStackable;

    private void Awake()
    {
        Name = Ingredient.ToString();
    }
}

