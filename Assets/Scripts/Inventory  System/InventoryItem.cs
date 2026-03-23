using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public Image ItemImage;
    public Items Item;
    [HideInInspector]
    public Transform parentAfterDrag;
    public void OnBeginDrag(PointerEventData eventData)
    {
        ItemImage.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ItemImage.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }
    

}
