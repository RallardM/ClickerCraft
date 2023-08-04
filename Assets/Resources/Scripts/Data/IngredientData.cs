
using System;
using System.Collections.Generic;
using UnityEngine;
using static IngredientManager;

[CreateAssetMenu(fileName = "Ingredient", menuName = "ScriptableObjects/IngredientData")]

public class IngredientData : ScriptableObject
{
    public string Name;
    public Sprite Sprite;
    public int MaxQuantity;
    public EIngredient Ingredient;
    public List<Array> Receipes = new List<Array>();
    public bool isCraftable;
    public bool isStackable;

    private void Awake()
    {
        Name = Ingredient.ToString();

        if (m_receipes == null)
        {
            LoadReceipes();
        }

        SetReceipe();
    }

    private void SetReceipe()
    {
        switch (Ingredient)
        {
            case EIngredient.Blaze:
                Receipes.Add(m_receipes[(int)EIngredient.Blaze]);
                break;

            case EIngredient.Coal:
                Receipes.Add(m_receipes[(int)EIngredient.Coal]);
                break;

            case EIngredient.Vapor:
                Receipes.Add(m_receipes[(int)EIngredient.Vapor]);
                break;

            case EIngredient.Fireburst:
                Receipes.Add(m_receipes[(int)EIngredient.Fireburst]);
                break;

            case EIngredient.Archea:
                Receipes.Add(m_receipes[(int)EIngredient.Archea]);
                break;

            case EIngredient.Magma:
                Receipes.Add(m_receipes[(int)EIngredient.Magma]);
                Receipes.Add(m_receipes[(int)EIngredient.MagmaExtended]);
                break;

            case EIngredient.Rock:
                Receipes.Add(m_receipes[(int)EIngredient.Rock]);
                break;

            case EIngredient.Mud:
                Receipes.Add(m_receipes[(int)EIngredient.Mud]);
                break;

            case EIngredient.Dust:
                Receipes.Add(m_receipes[(int)EIngredient.Dust]);
                break;

            case EIngredient.Seed:
                Receipes.Add(m_receipes[(int)EIngredient.Seed]);
                break;

            case EIngredient.Steam:
                Receipes.Add(m_receipes[(int)EIngredient.Steam]);
                Receipes.Add(m_receipes[(int)EIngredient.SteamExtended]);
                break;

            case EIngredient.Pond:
                Receipes.Add(m_receipes[(int)EIngredient.Pond]);
                Receipes.Add(m_receipes[(int)EIngredient.PondExtended]);
                break;

            case EIngredient.Puddle:
                Receipes.Add(m_receipes[(int)EIngredient.Puddle]);
                break;

            case EIngredient.Rain:
                Receipes.Add(m_receipes[(int)EIngredient.Rain]);
                break;

            case EIngredient.Algae:
                Receipes.Add(m_receipes[(int)EIngredient.Algae]);
                break;

            case EIngredient.Firebolt:
                Receipes.Add(m_receipes[(int)EIngredient.Firebolt]);
                Receipes.Add(m_receipes[(int)EIngredient.FireboltExtended]);
                break;

            case EIngredient.Duststorm:
                Receipes.Add(m_receipes[(int)EIngredient.Duststorm]);
                Receipes.Add(m_receipes[(int)EIngredient.DuststormExtended]);
                break;

            case EIngredient.Thunderstorm:
                Receipes.Add(m_receipes[(int)EIngredient.Thunderstorm]);
                Receipes.Add(m_receipes[(int)EIngredient.ThunderstormExtended]);
                break;

            case EIngredient.Tornado:
                Receipes.Add(m_receipes[(int)EIngredient.Tornado]);
                break;

            case EIngredient.Fungi:
                Receipes.Add(m_receipes[(int)EIngredient.Fungi]);
                Receipes.Add(m_receipes[(int)EIngredient.FungiExtended]);
                break;

            case EIngredient.Pyroid:
                Receipes.Add(m_receipes[(int)EIngredient.Pyroid]);
                Receipes.Add(m_receipes[(int)EIngredient.PyroidExtended]);
                break;

            case EIngredient.Golem:
                Receipes.Add(m_receipes[(int)EIngredient.Golem]);
                Receipes.Add(m_receipes[(int)EIngredient.GolemExtended]);
                break;

            case EIngredient.Undine:
                Receipes.Add(m_receipes[(int)EIngredient.Undine]);
                Receipes.Add(m_receipes[(int)EIngredient.UndineExtended]);
                break;

            case EIngredient.Sylph:
                Receipes.Add(m_receipes[(int)EIngredient.Sylph]);
                Receipes.Add(m_receipes[(int)EIngredient.SylphExtended]);
                break;

            case EIngredient.Spirit:
                Receipes.Add(m_receipes[(int)EIngredient.Spirit]);
                break;

            // Base Ingredients
            case EIngredient.Fire:
            case EIngredient.Water:
            case EIngredient.Earth:
            case EIngredient.Air:
            case EIngredient.Ether:
            default:
                Receipes.Add(m_receipes[(int)EIngredient.Count]);
                break;
        }
    }
}