using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _Instance;

    public enum EUiSlotContainer
    {
        Solve,
        Cauldron,
        Coagula,
        Count
    }

    private static GameObject[] m_solveSlots;
    private static GameObject[] m_cauldronSlots;
    private static GameObject[] m_coagulaSlots;

    private GameObject m_firstSolveSlot;
    private GameObject m_secondSolveSlot;
    private GameObject m_thirdSolveSlot;
    private GameObject m_fourthSolveSlot;
    private GameObject m_fifthSolveSlot;
    private GameObject m_sixthSolveSlot;
    private GameObject m_seventhSolveSlot;
    private GameObject m_eigthSolveSlot;
    private GameObject m_ninethSolveSlot;
    private GameObject m_tenthSolveSlot;
    private GameObject m_eleventhSolveSlot;
    private GameObject m_twelfthSolveSlot;
    private GameObject m_thirteenthSolveSlot;
    private GameObject m_fourteenthSolveSlot;
    private GameObject m_fifteenthSolveSlot;
    private GameObject m_sixteenthSolveSlot;
    private GameObject m_seventeenthSolveSlot;

    private GameObject m_firstCauldronSlot;
    private GameObject m_secondCauldronSlot;
    private GameObject m_thirdCauldronSlot;
    private GameObject m_fourthCauldronSlot;

    private GameObject m_firstCoagulaSlot;
    private GameObject m_secondCoagulaSlot;
    private GameObject m_thirdCoagulaSlot;
    private GameObject m_fourthCoagulaSlot;
    private GameObject m_fifthCoagulaSlot;
    private GameObject m_sixthCoagulaSlot;
    private GameObject m_seventhCoagulaSlot;
    private GameObject m_eigthCoagulaSlot;
    private GameObject m_ninethCoagulaSlot;
    private GameObject m_tenthCoagulaSlot;
    private GameObject m_eleventhCoagulaSlot;
    private GameObject m_twelfthCoagulaSlot;
    private GameObject m_thirteenthCoagulaSlot;
    private GameObject m_fourteenthCoagulaSlot;
    private GameObject m_fifteenthCoagulaSlot;
    private GameObject m_sixteenthCoagulaSlot;
    private GameObject m_seventeenthCoagulaSlot;

    private string m_firstSolveSlotPath = "Canvas/Solve/LeftHorizontalGrid/InventorySlot1";
    private string m_secondSolveSlotPath = "Canvas/Solve/LeftHorizontalGrid/InventorySlot2";
    private string m_thirdSolveSlotPath = "Canvas/Solve/LeftHorizontalGrid/InventorySlot3";
    private string m_fourthSolveSlotPath = "Canvas/Solve/LeftHorizontalGrid/InventorySlot4";
    private string m_fifthSolveSlotPath = "Canvas/Solve/LeftHorizontalGrid/InventorySlot5";
    private string m_sixthSolveSlotPath = "Canvas/Solve/LeftHorizontalGrid/InventorySlot6";
    private string m_seventhSolveSlotPath = "Canvas/Solve/LeftHorizontalGrid/InventorySlot7";
    private string m_eigthSolveSlotPath = "Canvas/Solve/LeftHorizontalGrid/InventorySlot8";
    private string m_ninethSolveSlotPath = "Canvas/Solve/LeftSquareGrid/InventorySlot1";
    private string m_tenthSolveSlotPath = "Canvas/Solve/LeftSquareGrid/InventorySlot2";
    private string m_eleventhSolveSlotPath = "Canvas/Solve/LeftSquareGrid/InventorySlot3";
    private string m_twelfthSolveSlotPath = "Canvas/Solve/LeftSquareGrid/InventorySlot4";
    private string m_thirteenthSolveSlotPath = "Canvas/Solve/LeftSquareGrid/InventorySlot5";
    private string m_fourteenthSolveSlotPath = "Canvas/Solve/LeftSquareGrid/InventorySlot6";
    private string m_fifteenthSolveSlotPath = "Canvas/Solve/LeftSquareGrid/InventorySlot7";
    private string m_sixteenthSolveSlotPath = "Canvas/Solve/LeftSquareGrid/InventorySlot8";
    private string m_seventeenthSolveSlotPath = "Canvas/Solve/LeftSquareGrid/InventorySlot9";

    private string m_firstCauldronSlotPath = "Canvas/Cauldron/TopInventorySlot";
    private string m_secondCauldronSlotPath = "Canvas/Cauldron/LeftInventorySlot";
    private string m_thirdCauldronSlotPath = "Canvas/Cauldron/RightInventorySlot";
    private string m_fourthCauldronSlotPath = "Canvas/Cauldron/BottomInventorySlot";

    private string m_firstCoagulaSlotPath = "Canvas/Coagula/RightSquareGrid/InventorySlot1";
    private string m_secondCoagulaSlotPath = "Canvas/Coagula/RightSquareGrid/InventorySlot2";
    private string m_thirdCoagulaSlotPath = "Canvas/Coagula/RightSquareGrid/InventorySlot3";
    private string m_fourthCoagulaSlotPath = "Canvas/Coagula/RightSquareGrid/InventorySlot4";
    private string m_fifthCoagulaSlotPath = "Canvas/Coagula/RightSquareGrid/InventorySlot5";
    private string m_sixthCoagulaSlotPath = "Canvas/Coagula/RightSquareGrid/InventorySlot6";
    private string m_seventhCoagulaSlotPath = "Canvas/Coagula/RightSquareGrid/InventorySlot7";
    private string m_eigthCoagulaSlotPath = "Canvas/Coagula/RightSquareGrid/InventorySlot8";
    private string m_ninethCoagulaSlotPath = "Canvas/Coagula/RightSquareGrid/InventorySlot9";
    private string m_tenthCoagulaSlotPath = "Canvas/Coagula/RightHorizontalGrid/InventorySlot1";
    private string m_eleventhCoagulaSlotPath = "Canvas/Coagula/RightHorizontalGrid/InventorySlot2";
    private string m_twelfthCoagulaSlotPath = "Canvas/Coagula/RightHorizontalGrid/InventorySlot3";
    private string m_thirteenthCoagulaSlotPath = "Canvas/Coagula/RightHorizontalGrid/InventorySlot4";
    private string m_fourteenthCoagulaSlotPath = "Canvas/Coagula/RightHorizontalGrid/InventorySlot5";
    private string m_fifteenthCoagulaSlotPath = "Canvas/Coagula/RightHorizontalGrid/InventorySlot6";
    private string m_sixteenthCoagulaSlotPath = "Canvas/Coagula/RightHorizontalGrid/InventorySlot7";
    private string m_seventeenthCoagulaSlotPath = "Canvas/Coagula/RightHorizontalGrid/InventorySlot8";

    private static IngredientData m_lastClickedIngredient;
    private static uint m_lastClickedIngredientQuantity = 0;

    public static GameObject[] SolveSlots { get => m_solveSlots; }
    public static GameObject[] CauldronSlots { get => m_cauldronSlots; }
    public static GameObject[] CoagulaSlots { get => m_coagulaSlots; }

    public static IngredientData LastClickedIngredient { get => m_lastClickedIngredient; set => m_lastClickedIngredient = value; }
    public static uint LastClickedIngredientQuantity { get => m_lastClickedIngredientQuantity; set => m_lastClickedIngredientQuantity = value; }

    private void Awake()
    {
        m_firstSolveSlot = GameObject.Find(m_firstSolveSlotPath);
        m_secondSolveSlot = GameObject.Find(m_secondSolveSlotPath);
        m_thirdSolveSlot = GameObject.Find(m_thirdSolveSlotPath);
        m_fourthSolveSlot = GameObject.Find(m_fourthSolveSlotPath);
        m_fifthSolveSlot = GameObject.Find(m_fifthSolveSlotPath);
        m_sixthSolveSlot = GameObject.Find(m_sixthSolveSlotPath);
        m_seventhSolveSlot = GameObject.Find(m_seventhSolveSlotPath);
        m_eigthSolveSlot = GameObject.Find(m_eigthSolveSlotPath);
        m_ninethSolveSlot = GameObject.Find(m_ninethSolveSlotPath);
        m_tenthSolveSlot = GameObject.Find(m_tenthSolveSlotPath);
        m_eleventhSolveSlot = GameObject.Find(m_eleventhSolveSlotPath);
        m_twelfthSolveSlot = GameObject.Find(m_twelfthSolveSlotPath);
        m_thirteenthSolveSlot = GameObject.Find(m_thirteenthSolveSlotPath);
        m_fourteenthSolveSlot = GameObject.Find(m_fourteenthSolveSlotPath);
        m_fifteenthSolveSlot = GameObject.Find(m_fifteenthSolveSlotPath);
        m_sixteenthSolveSlot = GameObject.Find(m_sixteenthSolveSlotPath);
        m_seventeenthSolveSlot = GameObject.Find(m_seventeenthSolveSlotPath);

        m_firstCauldronSlot = GameObject.Find(m_firstCauldronSlotPath);
        m_secondCauldronSlot = GameObject.Find(m_secondCauldronSlotPath);
        m_thirdCauldronSlot = GameObject.Find(m_thirdCauldronSlotPath);
        m_fourthCauldronSlot = GameObject.Find(m_fourthCauldronSlotPath);

        m_firstCoagulaSlot = GameObject.Find(m_firstCoagulaSlotPath);
        m_secondCoagulaSlot = GameObject.Find(m_secondCoagulaSlotPath);
        m_thirdCoagulaSlot = GameObject.Find(m_thirdCoagulaSlotPath);
        m_fourthCoagulaSlot = GameObject.Find(m_fourthCoagulaSlotPath);
        m_fifthCoagulaSlot = GameObject.Find(m_fifthCoagulaSlotPath);
        m_sixthCoagulaSlot = GameObject.Find(m_sixthCoagulaSlotPath);
        m_seventhCoagulaSlot = GameObject.Find(m_seventhCoagulaSlotPath);
        m_eigthCoagulaSlot = GameObject.Find(m_eigthCoagulaSlotPath);
        m_ninethCoagulaSlot = GameObject.Find(m_ninethCoagulaSlotPath);
        m_tenthCoagulaSlot = GameObject.Find(m_tenthCoagulaSlotPath);
        m_eleventhCoagulaSlot = GameObject.Find(m_eleventhCoagulaSlotPath);
        m_twelfthCoagulaSlot = GameObject.Find(m_twelfthCoagulaSlotPath);
        m_thirteenthCoagulaSlot = GameObject.Find(m_thirteenthCoagulaSlotPath);
        m_fourteenthCoagulaSlot = GameObject.Find(m_fourteenthCoagulaSlotPath);
        m_fifteenthCoagulaSlot = GameObject.Find(m_fifteenthCoagulaSlotPath);
        m_sixteenthCoagulaSlot = GameObject.Find(m_sixteenthCoagulaSlotPath);
        m_seventeenthCoagulaSlot = GameObject.Find(m_seventeenthCoagulaSlotPath);

        if (m_firstCauldronSlot == null || m_secondCauldronSlot == null ||
            m_thirdCauldronSlot == null || m_fourthCauldronSlot == null)
        {
            Debug.LogError("One or more Cauldron Slots not found.");
        }

        m_solveSlots = new GameObject[17]
        {
             m_firstSolveSlot, m_secondSolveSlot, m_thirdSolveSlot,
             m_fourthSolveSlot, m_fifthSolveSlot, m_sixthSolveSlot,
             m_seventhSolveSlot, m_eigthSolveSlot, m_ninethSolveSlot,
             m_tenthSolveSlot, m_eleventhSolveSlot, m_twelfthSolveSlot, m_thirteenthSolveSlot,
             m_fourteenthSolveSlot, m_fifteenthSolveSlot, m_sixteenthSolveSlot, m_seventeenthSolveSlot
        };

        m_cauldronSlots = new GameObject[4]
        {
            m_firstCauldronSlot,
            m_secondCauldronSlot,
            m_thirdCauldronSlot,
            m_fourthCauldronSlot
        };

        m_coagulaSlots = new GameObject[17]
        {
             m_firstCoagulaSlot, m_secondCoagulaSlot, m_thirdCoagulaSlot,
             m_fourthCoagulaSlot, m_fifthCoagulaSlot, m_sixthCoagulaSlot,
             m_seventhCoagulaSlot, m_eigthCoagulaSlot, m_ninethCoagulaSlot,
             m_tenthCoagulaSlot, m_eleventhCoagulaSlot, m_twelfthCoagulaSlot, m_thirteenthCoagulaSlot,
             m_fourteenthCoagulaSlot, m_fifteenthCoagulaSlot, m_sixteenthCoagulaSlot, m_seventeenthCoagulaSlot
        };
    }

    public static UIManager GetInstance()
    {
        if (_Instance == null)
        {
            _Instance = new UIManager();
        }

        return _Instance;
    }

    public UIManager()
    {
        if (_Instance)
        {
            Destroy(this);
            return;
        }
    }

    public static GameObject GetContainerSlotFromIndex(EUiSlotContainer uiSlotContainer, int index)
    {
        switch (uiSlotContainer)
        {
            case EUiSlotContainer.Solve:
                return m_solveSlots[index];

            case EUiSlotContainer.Cauldron:
             return m_cauldronSlots[index];

            case EUiSlotContainer.Coagula:
                return m_coagulaSlots[index];

            default:
                Debug.LogError("Invalid UI Slot Container.");
                return null;
        }
    }

    public static GameObject[] GetContainer(EUiSlotContainer uiSlotContainer)
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
                Debug.LogError("uiSlotContainer is null");
                return null;
        }
    }
}
