// Source : https://www.youtube.com/watch?v=kWRyZ3hb1Vc
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class IngredientInteraction : MonoBehaviour, IPointerClickHandler//, IBeginDragHandler, IDragHandler, IEndDragHandler // TODO Remi : For my portefolio end of session
{
    [SerializeField] IngredientData m_ingredientData;
    private uint m_currentQuantity = 1;

    public uint CurrentQuantity { get { return m_currentQuantity; } set { m_currentQuantity = value; } }

    public IngredientData IngredientData { get { return m_ingredientData; } set { m_ingredientData = value; } }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(CompareTag("CauldonIngredient"))
        {
            //Debug.Log("Is an ingredient inside the cauldron");
            IngredientManager.RemoveIngredient(IngredientData);
            Destroy(gameObject);
        }

        if (CompareTag("BaseIngredient"))
        {
            //Debug.Log("Is a base ingredient");

            IngredientData clickedIngredient = IngredientData;
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
