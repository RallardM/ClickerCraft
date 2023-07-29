// Source : https://www.youtube.com/watch?v=kWRyZ3hb1Vc

using UnityEngine;
using UnityEngine.EventSystems;

public class IngredientInteraction : MonoBehaviour, IPointerClickHandler//, IBeginDragHandler, IDragHandler, IEndDragHandler // TODO Remi : For my portefolio end of session
{
    [SerializeField] IngredientData m_ingredientData;
    public uint m_quantity = 0;

    public IngredientData GetIngredientData() { return m_ingredientData; }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(CompareTag("CauldonIngredient"))
        {
            Debug.Log("Is an ingredient inside the cauldron");
            IngredientManager.RemoveIngredient(GetIngredientData());
            Destroy(gameObject);
        }

        if (CompareTag("BaseIngredient"))
        {
            //Debug.Log("Is a base ingredient");

            IngredientData clickedIngredient = GetIngredientData();
            IngredientManager.AddIngredient(clickedIngredient);
        }
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
