// Source : https://www.youtube.com/watch?v=kWRyZ3hb1Vc

using UnityEngine;
using UnityEngine.EventSystems;

public class ItemInteraction : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    private IngredientData m_data;
    [SerializeField] IngredientData m_ingredientData;

    public IngredientData GetIngredientData() { return m_ingredientData; }

    //private bool m_isDragged = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick");

        //m_isDragged = false;

        if (!CompareTag("Element"))
        {
            return;
        }

        IngredientManager.AddIngredient(m_ingredientData.IngredientType);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");

        //m_isDragged = true;

        if (!CompareTag("Element"))
        {
            return;
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
    }
}
