// Source : https://www.youtube.com/watch?v=kWRyZ3hb1Vc
using UnityEngine;
using UnityEngine.EventSystems;

public class BasicIngredientInteraction : IngredientManager, IPointerClickHandler//, IBeginDragHandler, IDragHandler, IEndDragHandler // TODO Remi : For my portefolio end of session
{
    [SerializeField] IngredientData m_ingredientData;

    public IngredientData IngredientData { get { return m_ingredientData; } set { m_ingredientData = value; } }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("Is a base ingredient");

        IngredientData clickedIngredient = IngredientData;
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
