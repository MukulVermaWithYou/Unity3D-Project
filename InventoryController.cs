using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [HideInInspector] public ItemGrid selectedItemGrid;

    InventoryItem selectedItem;
    InventoryItem overlapItem;
    RectTransform rectTransform;

    [SerializeField] List<ItemData> items;
    [SerializeField] GameObject itemPrefab;

    [SerializeField] Transform canvasTransform;

    InventoryHighlight inventoryHighlight;
    InventoryHighlight inventoryRedHighlight;

    private void Awake() {
        inventoryHighlight = GetComponent<InventoryHighlight>();

        inventoryRedHighlight = GetComponent<InventoryHighlight>();
    }

    private void Update()
    {
        ItemIconDrag();


        if( Input.GetKeyDown(KeyCode.Q) ) {
            CreateRandomItem();
        }

        if (selectedItemGrid == null)
        {
            inventoryHighlight.Show(false);
            return;
        }

        if (Input.GetKeyDown( KeyCode.R ) ) {
            RotateItem();
        } 
        // This statement displays the coordinate of the tile on which the cursor is currently being placed.
        HandleHighlight();

        if (Input.GetMouseButtonDown(0))
        {
            LeftMouseButtonPress();
        }

    }


    private void RotateItem()
    { 
        if( selectedItem == null ) {
            return;
        } // Exit Gate in case no item at hand.

        selectedItem.Rotate();
    }

    InventoryItem itemToHighlight;
    InventoryItem itemToRedHighlight;
    private void HandleHighlight() {

        Vector2Int positionOnGrid = GetTileGridPosition();

        if( selectedItem == null ) {

            itemToHighlight = selectedItemGrid.GetItem( positionOnGrid.x, positionOnGrid.y );

            if( itemToHighlight != null && ( itemToHighlight.HEIGHT > 1 || itemToHighlight.WIDTH > 1 ) ) {
                itemToRedHighlight = itemToHighlight;
                // itemToHighlight.itemData.redHighlighted = true;
                itemToHighlight = null;
                HandleRedHighlight();
                return;
            }
            else if( itemToHighlight != null ) {
                inventoryHighlight.Show(true);
                inventoryHighlight.SetSize(itemToHighlight);
                inventoryHighlight.SetParent(selectedItemGrid);
                inventoryHighlight.SetPosition( selectedItemGrid, itemToHighlight );
            }
            else{
                inventoryHighlight.Show(false);
            }
        }
        else{
            inventoryHighlight.Show(selectedItemGrid.BoundaryCheck( positionOnGrid.x, positionOnGrid.y, selectedItem.WIDTH, selectedItem.HEIGHT ) );
                inventoryHighlight.SetSize(selectedItem);
                inventoryHighlight.SetParent(selectedItemGrid);
                inventoryHighlight.SetPositionForHighlighter( selectedItemGrid, selectedItem, positionOnGrid.x, positionOnGrid.y );
        }
    }


    private void HandleRedHighlight() {

        // InventoryHighlight redHighlight;
        // redHighlight = GetComponent<InventoryHighlight>();
    
        if( itemToRedHighlight != null ) {
            inventoryRedHighlight.redShow(true);
            inventoryRedHighlight.SetRedSize(itemToRedHighlight);
            inventoryRedHighlight.SetRedParent(selectedItemGrid);
            inventoryRedHighlight.SetRedPosition( selectedItemGrid, itemToRedHighlight );
            itemToRedHighlight = null;
        }
    }

    private void CreateRandomItem()
    {
        InventoryItem inventoryItem = Instantiate( itemPrefab ).GetComponent<InventoryItem>();
        selectedItem = inventoryItem;

        rectTransform = inventoryItem.GetComponent<RectTransform>();

        rectTransform.SetParent(canvasTransform);

        rectTransform.SetAsLastSibling();

        int selecteditemID = UnityEngine.Random.Range( 0, items.Count );
        inventoryItem.Set(items[selecteditemID]);
    }

    private void LeftMouseButtonPress()
    {
        Vector2Int tileGridPosition = GetTileGridPosition();

        if (selectedItem == null)
        {
            PickUpItem(tileGridPosition);
        }
        else
        {
            PlaceItem(tileGridPosition);
        }
    }

    private Vector2Int GetTileGridPosition()
    {
        Vector2 position = Input.mousePosition;

        if (selectedItem != null)
        {
            position.x -= (selectedItem.WIDTH - 1) * ItemGrid.tileSizeWidth / 2;
            position.y += (selectedItem.HEIGHT - 1) * ItemGrid.tileSizeHeight / 2;
        }

        return selectedItemGrid.GetTileGridPosition(position);
    }

    private void PlaceItem(Vector2Int tileGridPosition)
    {
        bool complete = selectedItemGrid.PlaceItem(selectedItem, tileGridPosition.x, tileGridPosition.y, ref overlapItem );

        if( complete ) {
            selectedItem = null;
            if( overlapItem != null ) {
                selectedItem = overlapItem;
                overlapItem = null;
                rectTransform = selectedItem.GetComponent<RectTransform>();

                rectTransform.SetAsLastSibling();
            }
        }
    }

    private void PickUpItem(Vector2Int tileGridPosition)
    {
        selectedItem = selectedItemGrid.PickUpItem(tileGridPosition.x, tileGridPosition.y);

        if (selectedItem != null)
        {
            rectTransform = selectedItem.GetComponent<RectTransform>();
        }
    }

    private void ItemIconDrag()
    {
        if (selectedItem != null)
        {
            rectTransform.position = Input.mousePosition + (new Vector3(-17f, 20f, 0f));
        }
    }
}
