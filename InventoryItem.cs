using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryItem : MonoBehaviour
{
    public ItemData itemData;

    public int HEIGHT {

        get {
            if( rotated == false ) {
                return itemData.itemHeight;
            }
            return( itemData.itemWidth );
        }
    }
    public int WIDTH {

        get {
            if( rotated == false ) {
                return itemData.itemWidth;
            }
            return( itemData.itemHeight );
        }
    }


    public bool rotated = false;

    public int onGridPositionX;
    public int onGridPositionY;


    internal void Set(ItemData itemData)
    {
        this.itemData = itemData;

        GetComponent<Image>().sprite = itemData.itemIcon;

        Vector2 size = new Vector2();
        size.x = WIDTH * ItemGrid.tileSizeWidth;
        size.y = HEIGHT * ItemGrid.tileSizeHeight;

        GetComponent<RectTransform>().sizeDelta = size;
    }
    internal void Rotate()
    {
        rotated = !rotated;

        RectTransform rectTransform = GetComponent<RectTransform>();
        
        rectTransform.rotation = Quaternion.Euler(0, 0, rotated == true ? 90f : 0f );
    }

}