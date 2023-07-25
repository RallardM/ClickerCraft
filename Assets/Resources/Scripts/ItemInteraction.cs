// Source : https://www.youtube.com/watch?v=kWRyZ3hb1Vc

using UnityEngine;
using UnityEngine.EventSystems;

public class ItemInteraction : MonoBehaviour//, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler // TODO Remi : For my portefolio end of session
{
    private IngredientData m_data;
    [SerializeField] IngredientData m_ingredientData;

    public IngredientData GetIngredientData() { return m_ingredientData; }

    public void OnPointerClick(PointerEventData eventData)
    {

        if (!CompareTag("Element"))
        {
            return;
        }

        IngredientData clickedIngredient = GetIngredientData();
        IngredientManager.AddIngredient(clickedIngredient);
    }

    // TODO Remi : For my portefolio end of session
    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    Debug.Log("OnBeginDrag");


    //    if (!CompareTag("Element"))
    //    {
    //        return;
    //    }

    //}

    //public void OnDrag(PointerEventData eventData)
    //{
    //    Debug.Log("OnDrag");
    //    transform.position = Input.mousePosition;
    //}

    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    Debug.Log("OnEndDrag");
    //}
}
