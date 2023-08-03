
using UnityEngine;
using UnityEngine.EventSystems;

public class IngredientInteraction : IngredientManager, IPointerClickHandler
{
    private IngredientData m_ingredientData;
    private uint m_currentQuantity = 1;
    public IngredientData IngredientData { get { return m_ingredientData; } set { m_ingredientData = value; } }
    public uint CurrentQuantity { get { return m_currentQuantity; } set { m_currentQuantity = value; } }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Is an ingredient inside the UI slot : " + GetParentUiSlot.ToString());

        //Check if the clicked ingredient has stacked ingredients
        if (CurrentQuantity > 1)
        {
            //Remove one ingredient from the stack
            //Debug.Log("Remove one ingredient from the stack");
            CurrentQuantity--;
            return;
        }

        //Debug.Log("Remove the ingredient from the cauldron");
        IngredientPool.RemoveIngredient(IngredientData, GetParentUiSlot);
        Destroy(gameObject);
        //InGameCauldronPreviousIngredientQuantity--; //TODO: should be relatable to all containers except the basic ingredients not only the cauldron
        ReduceUiContainerPreviousIngredientCount(GetParentUiSlot);
    }

    private UIManager.EUiSlotContainer GetParentUiSlot
    {
        get
        {
            if (transform.parent.parent.name == "Solve")
            {
                return UIManager.EUiSlotContainer.Solve;
            }
            else if (transform.parent.parent.name == "Cauldron")
            {
                return UIManager.EUiSlotContainer.Cauldron;
            }
            else if (transform.parent.parent.name == "Coagula")
            {
                return UIManager.EUiSlotContainer.Coagula;
            }
            else
            {
                Debug.LogError("The parent of the ingredient is not a valid slot");
                return UIManager.EUiSlotContainer.Count;
            }
        }
    }
}