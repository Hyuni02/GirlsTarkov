using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropableCustom : Dropable
{
    public Item.ItemType type;
    Item.ItemType itemType;

    public override void OnDrop(PointerEventData eventData) {
        if (transform.childCount > 0)
            return;

        itemType = eventData.pointerDrag.transform.GetComponent<Clickable>().real.GetComponent<Item>().itemType;

        if (itemType == type)
            base.OnDrop(eventData);
    }
}
