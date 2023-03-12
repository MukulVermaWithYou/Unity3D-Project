using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHighlight : MonoBehaviour
{
    [SerializeField] RectTransform highlighter;
    [SerializeField] RectTransform redHighlighter;

    public void Show( bool b ) {
        highlighter.gameObject.SetActive(b);
    }
    public void redShow( bool b ) {
        redHighlighter.gameObject.SetActive(b);
    }

    public void SetSize( InventoryItem targetItem ) {
        Vector2 size = new Vector2();
        size.x = targetItem.WIDTH * ItemGrid.tileSizeWidth;
        size.y = targetItem.HEIGHT * ItemGrid.tileSizeHeight;
        highlighter.sizeDelta = size;
    }
    public void SetRedSize( InventoryItem targetItem ) {
        Vector2 size = new Vector2();
        size.x = targetItem.WIDTH * ItemGrid.tileSizeWidth;
        size.y = targetItem.HEIGHT * ItemGrid.tileSizeHeight;
        redHighlighter.sizeDelta = size;
    }

    public void SetPosition( ItemGrid targetGrid, InventoryItem targetItem )
    {
        SetParent(targetGrid);

        Vector2 pos = targetGrid.CalculatePositionOnGridForHighlighter(targetItem, targetItem.onGridPositionX, targetItem.onGridPositionY);

        highlighter.localPosition = pos;
    }
    public void SetRedPosition( ItemGrid targetGrid, InventoryItem targetItem )
    {
        SetParent(targetGrid);

        Vector2 pos = targetGrid.CalculatePositionOnGridForHighlighter(targetItem, targetItem.onGridPositionX, targetItem.onGridPositionY);

        redHighlighter.localPosition = pos;
    }

    public void SetParent(ItemGrid targetGrid)
    {
        if( targetGrid == null ) {
            return;
        }
        highlighter.SetParent(targetGrid.GetComponent<RectTransform>());
    }
    public void SetRedParent(ItemGrid targetGrid)
    {
        if( targetGrid == null ) {
            return;
        }
        redHighlighter.SetParent(targetGrid.GetComponent<RectTransform>());
    }

    public void SetPosition( ItemGrid targetGrid, InventoryItem targetItem, int posX, int posY ) {

        Vector2 pos = targetGrid.CalculatePositionOnGrid(
            targetItem, posX, posY );

            highlighter.localPosition = pos;
    }
    public void SetRedPosition( ItemGrid targetGrid, InventoryItem targetItem, int posX, int posY ) {

        Vector2 pos = targetGrid.CalculatePositionOnGrid(
            targetItem, posX, posY );

            redHighlighter.localPosition = pos;
    }

    public void SetPositionForHighlighter( ItemGrid targetGrid, InventoryItem targetItem, int posX, int posY ) {

        Vector2 pos = targetGrid.CalculatePositionOnGridForHighlighter( targetItem, posX, posY );

            highlighter.localPosition = pos;
    }
    public void SetPositionForRedHighlighter( ItemGrid targetGrid, InventoryItem targetItem, int posX, int posY ) {

        Vector2 pos = targetGrid.CalculatePositionOnGridForHighlighter( targetItem, posX, posY );

            redHighlighter.localPosition = pos;
    }
}
