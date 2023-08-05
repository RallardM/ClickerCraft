using UnityEngine;
using UnityEngine.EventSystems;
using static UIManager;

public class IngredientInteraction : IngredientManager, IPointerClickHandler
{
    private IngredientData m_ingredientData;
    private uint m_currentQuantity = 1;
    public IngredientData IngredientData { get { return m_ingredientData; } set { m_ingredientData = value; } }
    public uint CurrentQuantity { get { return m_currentQuantity; } set { m_currentQuantity = value; } }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            TransferIngredient();
            EUiSlotContainer parentUiSlot = GetParentUiSlot;
            IngredientPool.RemoveIngredientFromTransitPool(IngredientData, parentUiSlot);
            IngredientPool.RemoveIngredientFromTransformPool(transform, parentUiSlot);
            SetContainerPreviousIngredientCount(parentUiSlot, (uint)IngredientPool.GetTransitPool(parentUiSlot).Count);
            Destroy(gameObject);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            SubstractIngredient();
        }
    }

    private void TransferIngredient()
    {
        if (transform.parent.CompareTag("Solve"))
        {
            LastClickedIngredientQuantity = CurrentQuantity;
            IngredientPool.AddIngredientToTransitPool(IngredientData, UIManager.EUiSlotContainer.Cauldron);
        }
        else if (transform.parent.CompareTag("Cauldron"))
        {
            LastClickedIngredientQuantity = CurrentQuantity;
            IngredientPool.AddIngredientToTransitPool(IngredientData, UIManager.EUiSlotContainer.Solve);
        }
        else if (transform.parent.CompareTag("Coagula")) // Coagula only receive newly crafted ingredients
        {
            LastClickedIngredientQuantity = CurrentQuantity;
            IngredientPool.AddIngredientToTransitPool(IngredientData, UIManager.EUiSlotContainer.Solve);
        }
        else
        {
            Debug.LogError("The parent of the ingredient is not a valid slot, tag : " + transform.parent.tag + " Name : " + transform.name + " Parent name : " + transform.parent.name);
        }
    }

    private void SubstractIngredient()
    {
        //Debug.Log("Is an ingredient inside the UI slot : " + GetParentUiSlot.ToString());

        if (IsRemovedFromStack())
        {
            return;
        }

        EUiSlotContainer parentUiSlot = GetParentUiSlot;
        IngredientPool.RemoveIngredientFromTransitPool(IngredientData, parentUiSlot);
        IngredientPool.RemoveIngredientFromTransformPool(transform, parentUiSlot);
        SetContainerPreviousIngredientCount(parentUiSlot, (uint)IngredientPool.GetTransitPool(parentUiSlot).Count);
        Destroy(gameObject);
    }

    private bool IsRemovedFromStack()
    {
        //Check if the clicked ingredient has stacked ingredients
        if (CurrentQuantity > 1)
        {
            //Remove one ingredient from the stack
            //Debug.Log("Remove one ingredient from the stack");
            CurrentQuantity--;
            return true;
        }

        return false;
    }

    private EUiSlotContainer GetParentUiSlot
    {
        get
        {
            if (transform.parent.CompareTag("Solve"))
            {
                return EUiSlotContainer.Solve;
            }
            else if (transform.parent.CompareTag("Cauldron"))
            {
                return EUiSlotContainer.Cauldron;
            }
            else if (transform.parent.CompareTag("Coagula"))
            {
                return EUiSlotContainer.Coagula;
            }
            else
            {
                Debug.LogError("The parent of the ingredient is not a valid slot, tag : " + transform.parent.tag + " Name : " + transform.name );
                return EUiSlotContainer.Count;
            }
        }
    }
}