using System.Linq;
using UnityEngine;
using static UIManager;

public class IngredientManager : MonoBehaviour
{
    public static EIngredient[][] m_receipes;

    public enum EIngredient
    {
        Fire, Air, Earth, Water, Ether, // 5 elements
        Blaze, Coal, Vapor, Fireburst, Archea, // 5 elements
        Magma, MagmaExtended, Rock, Mud, Dust, Seed, // 6 elements
        Steam, SteamExtended, Pond, PondExtended, Puddle, Rain, Algae, // 7 elements
        Firebolt, FireboltExtended, Duststorm, DuststormExtended, Thunderstorm, ThunderstormExtended, Tornado, Fungi, FungiExtended, // 9 elements
        Pyroid, PyroidExtended, Golem, GolemExtended, Undine, UndineExtended, Sylph, SylphExtended, Spirit, // 9 elements
        Count // 1 element
    } // total 42 elements - 0 to 41 

    // Source : https://youtu.be/Zn4BDIXhy-M
    private static readonly EIngredient[] m_fireReceipe = new EIngredient[1] { EIngredient.Count }; // To add 0-4 elements for the SetReceipe() function
    private static readonly EIngredient[] m_airReceipe = new EIngredient[1] { EIngredient.Count }; // To add 0-4 elements for the SetReceipe() function
    private static readonly EIngredient[] m_earthReceipe = new EIngredient[1] { EIngredient.Count }; // To add 0-4 elements for the SetReceipe() function
    private static readonly EIngredient[] m_waterReceipe = new EIngredient[1] { EIngredient.Count }; // To add 0-4 elements for the SetReceipe() function
    private static readonly EIngredient[] m_etherReceipe = new EIngredient[1] { EIngredient.Count }; // To add 0-4 elements for the SetReceipe() function
    private static readonly EIngredient[] m_blazeReceipe = new EIngredient[2] { EIngredient.Fire, EIngredient.Fire };
    private static readonly EIngredient[] m_coalReceipe = new EIngredient[2] { EIngredient.Fire, EIngredient.Earth };
    private static readonly EIngredient[] m_vaporReceipe = new EIngredient[2] { EIngredient.Fire, EIngredient.Water };
    private static readonly EIngredient[] m_fireburstReceipe = new EIngredient[2] { EIngredient.Fire, EIngredient.Air };
    private static readonly EIngredient[] m_archeaReceipe = new EIngredient[2] { EIngredient.Fire, EIngredient.Ether };
    private static readonly EIngredient[] m_magmaReceipe = new EIngredient[2] { EIngredient.Rock, EIngredient.Fire };
    private static readonly EIngredient[] m_magmaReceipeExtended = new EIngredient[3] { EIngredient.Earth, EIngredient.Earth, EIngredient.Fire };
    private static readonly EIngredient[] m_rockReceipe = new EIngredient[2] { EIngredient.Earth, EIngredient.Earth };
    private static readonly EIngredient[] m_mudReceipe = new EIngredient[2] { EIngredient.Earth, EIngredient.Water };
    private static readonly EIngredient[] m_DustReceipe = new EIngredient[2] { EIngredient.Earth, EIngredient.Air };
    private static readonly EIngredient[] m_seedReceipe = new EIngredient[2] { EIngredient.Earth, EIngredient.Ether };
    private static readonly EIngredient[] m_steamReceipe = new EIngredient[2] { EIngredient.Puddle, EIngredient.Fire };
    private static readonly EIngredient[] m_steamReceipeExtended = new EIngredient[3] { EIngredient.Water, EIngredient.Water, EIngredient.Fire };
    private static readonly EIngredient[] m_pondReceipe = new EIngredient[2] { EIngredient.Puddle, EIngredient.Earth };
    private static readonly EIngredient[] m_pondReceipeExtended = new EIngredient[3] { EIngredient.Water, EIngredient.Water, EIngredient.Earth };
    private static readonly EIngredient[] m_puddleReceipe = new EIngredient[2] { EIngredient.Water, EIngredient.Water };
    private static readonly EIngredient[] m_rainReceipe = new EIngredient[2] { EIngredient.Water, EIngredient.Air };
    private static readonly EIngredient[] m_algeaReceipe = new EIngredient[2] { EIngredient.Water, EIngredient.Ether };
    private static readonly EIngredient[] m_fireboltReceipe = new EIngredient[2] { EIngredient.Tornado, EIngredient.Fire };
    private static readonly EIngredient[] m_fireboltReceipeExtended = new EIngredient[3] { EIngredient.Air, EIngredient.Air, EIngredient.Fire };
    private static readonly EIngredient[] m_duststormReceipe = new EIngredient[2] { EIngredient.Tornado, EIngredient.Earth };
    private static readonly EIngredient[] m_duststormReceipeExtended = new EIngredient[3] { EIngredient.Air, EIngredient.Air, EIngredient.Earth };
    private static readonly EIngredient[] m_thunderstormReceipe = new EIngredient[2] { EIngredient.Tornado, EIngredient.Water };
    private static readonly EIngredient[] m_thunderstormReceipeExtended = new EIngredient[3] { EIngredient.Air, EIngredient.Air, EIngredient.Water };
    private static readonly EIngredient[] m_tornadoReceipe = new EIngredient[2] { EIngredient.Air, EIngredient.Air };
    private static readonly EIngredient[] m_fungiReceipe = new EIngredient[2] { EIngredient.Tornado, EIngredient.Ether };
    private static readonly EIngredient[] m_fungiReceipeExtended = new EIngredient[3] { EIngredient.Air, EIngredient.Air, EIngredient.Ether };
    private static readonly EIngredient[] m_pyroidReceipe = new EIngredient[2] { EIngredient.Fire, EIngredient.Spirit };
    private static readonly EIngredient[] m_pyroidReceipeExtended = new EIngredient[3] { EIngredient.Ether, EIngredient.Ether, EIngredient.Fire };
    private static readonly EIngredient[] m_golemReceipe = new EIngredient[2] { EIngredient.Earth, EIngredient.Spirit };
    private static readonly EIngredient[] m_golemReceipeExtended = new EIngredient[3] { EIngredient.Ether, EIngredient.Ether, EIngredient.Earth };
    private static readonly EIngredient[] m_undineReceipe = new EIngredient[2] { EIngredient.Water, EIngredient.Spirit };
    private static readonly EIngredient[] m_undineReceipeExtended = new EIngredient[3] { EIngredient.Ether, EIngredient.Ether, EIngredient.Water };
    private static readonly EIngredient[] m_sylphReceipe = new EIngredient[2] { EIngredient.Air, EIngredient.Spirit };
    private static readonly EIngredient[] m_sylphReceipeExtended = new EIngredient[3] { EIngredient.Ether, EIngredient.Ether, EIngredient.Air };
    private static readonly EIngredient[] m_spiritReceipe = new EIngredient[2] { EIngredient.Ether, EIngredient.Ether };
    private static readonly EIngredient[] m_nonCraftable = new EIngredient[1] { EIngredient.Count };

    private static IngredientManager _Instance;

    private static GameObject m_ingredientPrefab;

    private static IngredientData m_fireIngredientData;
    private static IngredientData m_earthIngredientData;
    private static IngredientData m_waterIngredientData;
    private static IngredientData m_airIngredientData;
    private static IngredientData m_etherIngredientData;
    private static IngredientData m_blazeIngredientData;
    private static IngredientData m_coalIngredientData;
    private static IngredientData m_vaporIngredientData;
    private static IngredientData m_fireburstIngredientData;
    private static IngredientData m_archeaIngredientData;
    private static IngredientData m_magmaIngredientData;
    private static IngredientData m_rockIngredientData;
    private static IngredientData m_mudIngredientData;
    private static IngredientData m_dustIngredientData;
    private static IngredientData m_seedIngredientData;
    private static IngredientData m_steamIngredientData;
    private static IngredientData m_pondIngredientData;
    private static IngredientData m_puddleIngredientData;
    private static IngredientData m_rainIngredientData;
    private static IngredientData m_algeaIngredientData;
    private static IngredientData m_fireboltIngredientData;
    private static IngredientData m_duststormIngredientData;
    private static IngredientData m_thunderstormIngredientData;
    private static IngredientData m_tornadoIngredientData;
    private static IngredientData m_fungiIngredientData;
    private static IngredientData m_pyroidIngredientData;
    private static IngredientData m_golemIngredientData;
    private static IngredientData m_undineIngredientData;
    private static IngredientData m_sylphIngredientData;
    private static IngredientData m_spiritIngredientData;

    private string m_ingredientPrefabPath = "Prefabs/Ingredient";

    private string m_fireIngredientDataPath = "Data/Ingredients/Fire";
    private string m_earthIngredientDataPath = "Data/Ingredients/Earth";
    private string m_waterIngredientDataPath = "Data/Ingredients/Water";
    private string m_airIngredientDataPath = "Data/Ingredients/Air";
    private string m_etherIngredientDataPath = "Data/Ingredients/Ether";
    private string m_blazeIngredientDataPath = "Data/Ingredients/Blaze";
    private string m_coalIngredientDataPath = "Data/Ingredients/Coal";
    private string m_vaporIngredientDataPath = "Data/Ingredients/Vapor";
    private string m_fireburstIngredientDataPath = "Data/Ingredients/Fireburst";
    private string m_archeaIngredientDataPath = "Data/Ingredients/Archea";
    private string m_magmaIngredientDataPath = "Data/Ingredients/Magma";
    private string m_rockIngredientDataPath = "Data/Ingredients/Rock";
    private string m_mudIngredientDataPath = "Data/Ingredients/Mud";
    private string m_dustIngredientDataPath = "Data/Ingredients/Dust";
    private string m_seedIngredientDataPath = "Data/Ingredients/Seed";
    private string m_steamIngredientDataPath = "Data/Ingredients/Steam";
    private string m_pondIngredientDataPath = "Data/Ingredients/Pond";
    private string m_puddleIngredientDataPath = "Data/Ingredients/Puddle";
    private string m_rainIngredientDataPath = "Data/Ingredients/Rain";
    private string m_algeaIngredientDataPath = "Data/Ingredients/Algea";
    private string m_fireboltIngredientDataPath = "Data/Ingredients/Firebolt";
    private string m_duststormIngredientDataPath = "Data/Ingredients/Duststorm";
    private string m_thunderstormIngredientDataPath = "Data/Ingredients/Thunderstorm";
    private string m_tornadoIngredientDataPath = "Data/Ingredients/Tornado";
    private string m_fungiIngredientDataPath = "Data/Ingredients/Fungi";
    private string m_pyroidIngredientDataPath = "Data/Ingredients/Pyroid";
    private string m_golemIngredientDataPath = "Data/Ingredients/Golem";
    private string m_undineIngredientDataPath = "Data/Ingredients/Undine";
    private string m_sylphIngredientDataPath = "Data/Ingredients/Sylph";
    private string m_spiritIngredientDataPath = "Data/Ingredients/Spirit";

    private static uint m_solveContainerPreviousIngredientCount = 0;
    private static uint m_cauldronContainerPreviousIngredientCount = 0;
    private static uint m_coagulaContainerPreviousIngredientCount = 0;

    public static GameObject GetIngredientPrefab() { return m_ingredientPrefab; }

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

    protected static uint GetContainerPreviousIngredientCount(EUiSlotContainer uiSlotContainer)
    {
        switch (uiSlotContainer)
        {
            case EUiSlotContainer.Solve:
                return m_solveContainerPreviousIngredientCount;

            case EUiSlotContainer.Cauldron:
                return m_cauldronContainerPreviousIngredientCount;

            case EUiSlotContainer.Coagula:
                return m_coagulaContainerPreviousIngredientCount;

            default:
                Debug.LogError("uiSlotContainer is null");
                return 0;
        }
    }

    private void Awake()
    {
        m_ingredientPrefab = Resources.Load(m_ingredientPrefabPath) as GameObject;

        m_fireIngredientData = Resources.Load(m_fireIngredientDataPath) as IngredientData;
        m_earthIngredientData = Resources.Load(m_earthIngredientDataPath) as IngredientData;
        m_waterIngredientData = Resources.Load(m_waterIngredientDataPath) as IngredientData;
        m_airIngredientData = Resources.Load(m_airIngredientDataPath) as IngredientData;
        m_etherIngredientData = Resources.Load(m_etherIngredientDataPath) as IngredientData;
        m_blazeIngredientData = Resources.Load(m_blazeIngredientDataPath) as IngredientData;
        m_coalIngredientData = Resources.Load(m_coalIngredientDataPath) as IngredientData;
        m_vaporIngredientData = Resources.Load(m_vaporIngredientDataPath) as IngredientData;
        m_fireburstIngredientData = Resources.Load(m_fireburstIngredientDataPath) as IngredientData;
        m_archeaIngredientData = Resources.Load(m_archeaIngredientDataPath) as IngredientData;
        m_magmaIngredientData = Resources.Load(m_magmaIngredientDataPath) as IngredientData;
        m_rockIngredientData = Resources.Load(m_rockIngredientDataPath) as IngredientData;
        m_mudIngredientData = Resources.Load(m_mudIngredientDataPath) as IngredientData;
        m_dustIngredientData = Resources.Load(m_dustIngredientDataPath) as IngredientData;
        m_seedIngredientData = Resources.Load(m_seedIngredientDataPath) as IngredientData;
        m_steamIngredientData = Resources.Load(m_steamIngredientDataPath) as IngredientData;
        m_pondIngredientData = Resources.Load(m_pondIngredientDataPath) as IngredientData;
        m_puddleIngredientData = Resources.Load(m_puddleIngredientDataPath) as IngredientData;
        m_rainIngredientData = Resources.Load(m_rainIngredientDataPath) as IngredientData;
        m_algeaIngredientData = Resources.Load(m_algeaIngredientDataPath) as IngredientData;
        m_fireboltIngredientData = Resources.Load(m_fireboltIngredientDataPath) as IngredientData;
        m_duststormIngredientData = Resources.Load(m_duststormIngredientDataPath) as IngredientData;
        m_thunderstormIngredientData = Resources.Load(m_thunderstormIngredientDataPath) as IngredientData;
        m_tornadoIngredientData = Resources.Load(m_tornadoIngredientDataPath) as IngredientData;
        m_fungiIngredientData = Resources.Load(m_fungiIngredientDataPath) as IngredientData;
        m_pyroidIngredientData = Resources.Load(m_pyroidIngredientDataPath) as IngredientData;
        m_golemIngredientData = Resources.Load(m_golemIngredientDataPath) as IngredientData;
        m_undineIngredientData = Resources.Load(m_undineIngredientDataPath) as IngredientData;
        m_sylphIngredientData = Resources.Load(m_sylphIngredientDataPath) as IngredientData;
        m_spiritIngredientData = Resources.Load(m_spiritIngredientDataPath) as IngredientData;

        if (m_ingredientPrefab == null || m_fireIngredientData == null || m_earthIngredientData == null || 
            m_waterIngredientData == null || m_airIngredientData == null || m_etherIngredientData == null ||
            m_blazeIngredientData == null || m_coalIngredientData == null || m_vaporIngredientData == null || 
            m_fireburstIngredientData == null || m_archeaIngredientData == null || m_magmaIngredientData == null || 
            m_rockIngredientData == null || m_mudIngredientData == null || m_dustIngredientData == null || 
            m_seedIngredientData == null || m_steamIngredientData == null || m_pondIngredientData == null || 
            m_puddleIngredientData == null || m_rainIngredientData == null || m_algeaIngredientData == null || 
            m_fireboltIngredientData == null || m_duststormIngredientData == null || m_thunderstormIngredientData == null ||
            m_tornadoIngredientData == null || m_fungiIngredientData == null || m_pyroidIngredientData == null || 
            m_golemIngredientData == null || m_undineIngredientData == null || m_sylphIngredientData == null ||
            m_spiritIngredientData == null)
        {
            Debug.LogError("One of the Prefab or ingredient data not found.");
        }

        if (m_receipes == null)
        {
            LoadReceipes();
        }
    }

    public void Update()
    {
        // Putting the update in IngredientPool does not work because it is not a GameObject situated in the hierachy
        IngredientPool.UpdateContainerContent(EUiSlotContainer.Solve);
        IngredientPool.UpdateContainerContent(EUiSlotContainer.Cauldron);
        IngredientPool.UpdateContainerContent(EUiSlotContainer.Coagula);
    }

    public static void LoadReceipes()
    {
        if (m_fireReceipe == null)
        {
            Debug.LogError("A receipe is null, therefore not properly loaded.");
        }

        m_receipes = new EIngredient[(int)EIngredient.Count + 1][]
        {
                m_fireReceipe, m_airReceipe, m_earthReceipe, m_waterReceipe, m_etherReceipe, // To add 0-4 elements for the SetReceipe() function // 5 elements
                m_blazeReceipe, m_coalReceipe, m_vaporReceipe, m_fireburstReceipe, m_archeaReceipe, // 5 elements
                m_magmaReceipe, m_magmaReceipeExtended, m_rockReceipe, m_mudReceipe, m_DustReceipe, m_seedReceipe, // 6 elements
                m_steamReceipe, m_steamReceipeExtended, m_pondReceipe, m_pondReceipeExtended, m_puddleReceipe, m_rainReceipe, m_algeaReceipe, // 7 elements
                m_fireboltReceipe, m_fireboltReceipeExtended, m_duststormReceipe, m_duststormReceipeExtended, m_thunderstormReceipe, m_thunderstormReceipeExtended, m_tornadoReceipe, m_fungiReceipe, m_fungiReceipeExtended, // 9 elements
                m_pyroidReceipe, m_pyroidReceipeExtended, m_golemReceipe, m_golemReceipeExtended, m_undineReceipe, m_undineReceipeExtended, m_sylphReceipe, m_sylphReceipeExtended, m_spiritReceipe, // 9 elements
                m_nonCraftable // 1 element 
        };// Total 42 elementsthrow new NotImplementedException();
    }

    protected static void SetContainerPreviousIngredientCount(EUiSlotContainer uiSlotContainer, uint newCount)
    {
        switch (uiSlotContainer)
        {
            case EUiSlotContainer.Solve:
                m_solveContainerPreviousIngredientCount = newCount;
                break;

            case EUiSlotContainer.Cauldron:
                m_cauldronContainerPreviousIngredientCount = newCount;
                break;

            case EUiSlotContainer.Coagula:
                m_coagulaContainerPreviousIngredientCount = newCount;
                break;

            default:
                Debug.LogError("uiSlotContainer is null");
                break;
        }
    }

    protected static bool CheckCauldronIngredients()
    {
        foreach (EIngredient[] receipe in m_receipes)
        {
            if (receipe == null)
            {
                Debug.LogError("Recipe is null");
                continue;
            }

            if (receipe.Contains(EIngredient.Count))
            {
                continue;
            }

            if (IngredientPool.IsCauldronMatchingRecipe(receipe))
            {
                return true;
            }
        }
        return false;
    }

    protected static EIngredient GetResultingIngredient()
    {
        for (uint i = 0; i < m_receipes.Length; i++)
        {
            if (m_receipes[i] == null)
            {
                Debug.LogError("Recipe is null");
                continue;
            }

            if (IngredientPool.IsCauldronMatchingRecipe(m_receipes[i]))
            {
                Debug.Log("Recipe found");
                return IngredientPool.GetIngredientFromIndex(i);
            }
        }
        return EIngredient.Count;
    }

    protected IngredientData GetIngredientData(EIngredient resultingIngredient)
    {
        switch (resultingIngredient)
        {
            case EIngredient.Fire:
                return m_fireIngredientData;

            case EIngredient.Earth:
                return m_earthIngredientData;

            case EIngredient.Air:
                return m_airIngredientData;

            case EIngredient.Water: 
                return m_waterIngredientData;

            case EIngredient.Ether:
                return m_etherIngredientData;

            case EIngredient.Blaze:
                return m_blazeIngredientData;
            
            case EIngredient.Coal:
                return m_coalIngredientData;

            case EIngredient.Vapor:
                return m_vaporIngredientData;

            case EIngredient.Fireburst:
                return m_fireburstIngredientData;

            case EIngredient.Archea:
                return m_archeaIngredientData;

            case EIngredient.Magma:
                return m_magmaIngredientData;

            case EIngredient.Rock:
                return m_rockIngredientData;

            case EIngredient.Mud:
                return m_mudIngredientData;

            case EIngredient.Dust:
                return m_dustIngredientData;

            case EIngredient.Seed:
                return m_seedIngredientData;

            case EIngredient.Steam:
                return m_steamIngredientData;

            case EIngredient.Pond:
                return m_pondIngredientData;

            case EIngredient.Puddle:
                return m_puddleIngredientData;

            case EIngredient.Rain:
                return m_rainIngredientData;

            case EIngredient.Algae:
                return m_algeaIngredientData;

            case EIngredient.Firebolt:
                return m_fireboltIngredientData;

            case EIngredient.Duststorm:
                return m_duststormIngredientData;

            case EIngredient.Thunderstorm:
                return m_thunderstormIngredientData;

            case EIngredient.Tornado:
                return m_tornadoIngredientData;

            case EIngredient.Fungi:
                return m_fungiIngredientData;

            case EIngredient.Pyroid:
                return m_pyroidIngredientData;

            case EIngredient.Golem:
                return m_golemIngredientData;

            case EIngredient.Undine:
                return m_undineIngredientData;

            case EIngredient.Sylph:
                return m_sylphIngredientData;

            case EIngredient.Spirit:
                return m_spiritIngredientData;

            default:
                return null;
        }
    }
}