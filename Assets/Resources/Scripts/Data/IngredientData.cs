using System.Collections.Generic;
using UnityEngine;
using static IngredientManager;

[CreateAssetMenu(fileName = "Ingredient", menuName = "ScriptableObjects/IngredientData")]

public class IngredientData : ScriptableObject
{
    public string Name;
    //public string Description;
    public Sprite Sprite;
    public int MaxQuantity;
    public EBasicIngredient IngredientType;
    public List<IngredientData> Receipe; // TODO: add needed ingredient for receipe in list 
}