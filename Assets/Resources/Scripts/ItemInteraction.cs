// Source : https://www.youtube.com/watch?v=kWRyZ3hb1Vc

using UnityEngine;
using UnityEngine.EventSystems;

public class ItemInteraction : MonoBehaviour, IPointerClickHandler//, IBeginDragHandler, IDragHandler, IEndDragHandler // TODO Remi : For my portefolio end of session
{
    private IngredientData m_data;
    [SerializeField] IngredientData m_ingredientData;

    public IngredientData GetIngredientData() { return m_ingredientData; }

    public void OnPointerClick(PointerEventData eventData)
    {

        Debug.Log("OnPointerClick");

        if (!CompareTag("BaseIngredient"))
        {
            return;
        }

        Debug.Log("Is a base ingredient");

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
