// Different Generations of the code while learning for submission.

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class ParentChildGrid : MonoBehaviour
// {
//     public int numRows;
//     public int numCols;
//     public GameObject prefab;
//     public GameObject childPrefab;
//     public Color childColor;
//     public Color parentColor;

//     void Start()
//     {
//         for (int row = 0; row < numRows; row++)
//         {
//             for (int col = 0; col < numCols; col++)
//             {
//                 Vector3 pos = new Vector3(col, row, 0);
//                 GameObject obj = Instantiate(prefab, pos, Quaternion.identity, transform);
//                 GameObject childObj = Instantiate(childPrefab, obj.transform);
//                 float scale = Random.Range(0.5f, 2.0f);
//                 childObj.transform.localScale = new Vector3(scale, scale, scale);
//                 Renderer parentRenderer = obj.GetComponent<Renderer>();
//                 Renderer childRenderer = childObj.GetComponent<Renderer>();
                
//                 if (childRenderer.bounds.size.x > parentRenderer.bounds.size.x ||
//                     childRenderer.bounds.size.y > parentRenderer.bounds.size.y ||
//                     childRenderer.bounds.size.z > parentRenderer.bounds.size.z)
//                 {
//                     parentRenderer.material.color = childColor;
//                 }
//                 else
//                 {
//                     parentRenderer.material.color = parentColor;
//                 }
//             }
//         }
//     }
// }
    



/*
using UnityEngine;

public class ParentChildGrid : MonoBehaviour
{
    public GameObject prefab; // The prefab to instantiate in the grid
    public int rows; // The number of rows in the grid
    public int columns; // The number of columns in the grid
    public float spacing = 1f; // The spacing between each prefab

    void Start()
    {
        // Create a new game object to act as the parent for the instantiated prefabs
        GameObject parentObject = new GameObject("Grid");

        // Loop through each row and column and instantiate a prefab
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector3 position = new Vector3(col * spacing, row * spacing, 0); // Calculate the position of the prefab
                GameObject newPrefab = Instantiate(prefab, position, Quaternion.identity, parentObject.transform); // Instantiate the prefab and set its parent to the parent object
            }
        }
    }
}
*/




/*
using UnityEngine;

public class ParentChildGrid : MonoBehaviour
{
    public GameObject prefab; // The prefab to instantiate in the grid
    public GameObject childPrefab; // The child prefab to instantiate in each grid cell
    public int rows; // The number of rows in the grid
    public int columns; // The number of columns in the grid
    public float spacing = 1f; // The spacing between each prefab

    void Start()
    {
        // Create a new game object to act as the parent for the instantiated prefabs
        GameObject parentObject = new GameObject("Grid");

        // Loop through each row and column and instantiate a prefab with a child object
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector3 position = new Vector3( col * spacing, row * spacing, 0 ); // Calculate the position of the prefab
                GameObject newPrefab = Instantiate(prefab, position, Quaternion.identity, parentObject.transform); // Instantiate the prefab and set its parent to the parent object

                // Instantiate a child object for the prefab
                GameObject newChild = Instantiate(childPrefab, newPrefab.transform);
                newChild.transform.localPosition = Vector3.zero; // Center the child object relative to the parent prefab
            }
        }
    }
}
*/



/*
// Implementing the part where only a set of Predefined Values that can be asssumed

using UnityEngine;

public class ParentChildGrid : MonoBehaviour
{
    public GameObject prefab; // The prefab to instantiate in the grid
    public GameObject childPrefab; // The child prefab to instantiate in each grid cell
    public int rows; // The number of rows in the grid
    public int columns; // The number of columns in the grid
    public float spacing = 1f; // The spacing between each prefab

    // Custom Sizes:
    private static Vector3 size1 = new Vector3( 3f, 3f, 0f );
    private static Vector3 size2 = new Vector3( 1f, 1f, 0f );
    public Vector3[] scaleValues = { size1, size2 }; // An array of scale values to choose from for the child object

    void Start()
    {
        // Create a new game object to act as the parent for the instantiated prefabs
        GameObject parentObject = new GameObject("Grid");

        // Loop through each row and column and instantiate a prefab with a child object
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector3 position = new Vector3( col * spacing, row * spacing, 0 ); // Calculate the position of the prefab
                GameObject newPrefab = Instantiate(prefab, position, Quaternion.identity, parentObject.transform); // Instantiate the prefab and set its parent to the parent object

                // Instantiate a child object for the prefab
                GameObject newChild = Instantiate(childPrefab, newPrefab.transform);
                newChild.transform.localPosition = Vector3.zero; // Center the child object relative to the parent prefab
                newChild.transform.localScale = scaleValues[Random.Range(0, scaleValues.Length)]; // Set the scale of the child object to a randomly chosen value from the array of scale values
            }
        }
    }
}
*/

/*
// Code to make child prefab assume more than one grid cell, and restrict occupied grid cells from generating their own child prefab.

using UnityEngine;
using System.Collections.Generic;

public class ParentChildGrid : MonoBehaviour
{
    public GameObject prefab; // The prefab to instantiate in the grid
    public GameObject childPrefab; // The child prefab to instantiate in each grid cell
    public int rows; // The number of rows in the grid
    public int columns; // The number of columns in the grid
    public float spacing = 1f; // The spacing between each prefab
    public Vector3 childSize = Vector3.one; // The size of the child object
    public int childOccupancy = 1; // The number of grid cells occupied by a child object
    public List<Vector2Int> occupiedCells = new List<Vector2Int>(); // The cells occupied by larger child prefabs

    void Start()
    {
        // Create a new game object to act as the parent for the instantiated prefabs
        GameObject parentObject = new GameObject("Grid");

        // Loop through each row and column and instantiate a prefab with a child object
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                // Check if the current cell is occupied by a larger child prefab
                bool isOccupied = false;
                for (int i = 0; i < childOccupancy; i++)
                {
                    Vector2Int cell = new Vector2Int(col + i, row);
                    if (occupiedCells.Contains(cell))
                    {
                        isOccupied = true;
                        break;
                    }
                }

                if (!isOccupied)
                {
                    // Calculate the position of the prefab
                    Vector3 position = new Vector3(col * spacing, row * spacing, 0 ); 

                    // Instantiate the prefab and set its parent to the parent object
                    GameObject newPrefab = Instantiate(prefab, position, Quaternion.identity, parentObject.transform);

                    // Instantiate a child object for the prefab
                    GameObject newChild = Instantiate(childPrefab, newPrefab.transform);
                    newChild.transform.localPosition = Vector3.zero; // Center the child object relative to the parent prefab

                    // Set the scale of the child object to the fixed size specified in the editor
                    newChild.transform.localScale = childSize;

                    // Mark the cells occupied by the larger child prefab
                    for (int i = 0; i < childOccupancy; i++)
                    {
                        Vector2Int cell = new Vector2Int(col + i, row);
                        occupiedCells.Add(cell);
                    }
                }
            }
        }
    }
}

*/


/*
// Making the correction of child prefabs assume more than one grid, and restricting the overlapped grid to generate child prefab.

using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public GameObject prefab; // The prefab to instantiate in the grid
    public GameObject childPrefab; // The child prefab to instantiate in each grid cell
    public int rows; // The number of rows in the grid
    public int columns; // The number of columns in the grid
    public float spacing = 1f; // The spacing between each prefab
    public Vector2[] childSizes = { }; // The sizes of the child objects
      // Custom Sizes:
    private static Vector2 size1 = new Vector3( 2f, 2f );
    private static Vector2 size2 = new Vector3( 2f, 1f );
    private static Vector2 size3 = new Vector3( 1f, 1f );
    public List<Vector2Int> occupiedCells = new List<Vector2Int>(); // The cells occupied by larger child prefabs

    void Start()
    {
        // Create a new game object to act as the parent for the instantiated prefabs
        GameObject parentObject = new GameObject("Grid");

        // Loop through each row and column and instantiate a prefab with a child object
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {

                // Calculate the position of the prefab
                Vector3 position = new Vector3(col * spacing, row * spacing, 0); 

                // Instantiate the prefab and set its parent to the parent object
                GameObject newPrefab = Instantiate(prefab, position, Quaternion.identity, parentObject.transform);

                // Check if the current cell is occupied by a larger child prefab
                bool isOccupied = false;
                foreach (Vector2Int cell in GetOccupiedCells(col, row))
                {
                    if (occupiedCells.Contains(cell))
                    {
                        isOccupied = true;
                        break;
                    }
                }

                if (!isOccupied)
                {

                    // Instantiate a child object for the prefab
                    GameObject newChild = Instantiate(childPrefab, newPrefab.transform);
                    newChild.transform.localPosition = Vector3.zero; // Center the child object relative to the parent prefab

                    // Choose a random size for the child object from the list of available sizes
                    Vector2 childSize = childSizes[Random.Range(0, childSizes.Length)];

                    // Set the scale of the child object to the chosen size
                    newChild.transform.localScale = new Vector3(childSize.x, childSize.y, 1);

                    // Mark the cells occupied by the larger child prefab
                    foreach (Vector2Int cell in GetOccupiedCells(col, row, childSize))
                    {
                        occupiedCells.Add(cell);
                    }
                }
            }
        }
    }

    List<Vector2Int> GetOccupiedCells(int col, int row, Vector2 size = default)
    {
        if (size == default)
        {
            size = new Vector2(1, 1);
        }

        List<Vector2Int> cells = new List<Vector2Int>();

        for (int r = row; r < row + size.y; r++)
        {
            for (int c = col; c < col + size.x; c++)
            {
                if (c < columns && r < rows)
                {
                    cells.Add(new Vector2Int(c, r));
                }
            }
        }

        return cells;
    }
}
*/

/*

using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public GameObject prefab; // The prefab to instantiate in the grid
    public GameObject childPrefab; // The child prefab to instantiate in each grid cell
    public int rows; // The number of rows in the grid
    public int columns; // The number of columns in the grid
    public float spacing = 1f; // The spacing between each prefab
    public Vector2[] childSizes = { }; // The sizes of the child objects
    // Custom Sizes:
    private static Vector2 size1 = new Vector3( 2f, 2f );
    private static Vector2 size2 = new Vector3( 2f, 1f );
    private static Vector2 size3 = new Vector3( 1f, 1f );
    public List<Vector2Int> occupiedCells = new List<Vector2Int>(); // The cells occupied by larger child prefabs

    void Start()
    {
        // Create a new game object to act as the parent for the instantiated prefabs
        GameObject parentObject = new GameObject("Grid");

        // Loop through each row and column and instantiate a prefab with a child object
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {

                // Calculate the position of the prefab
                Vector3 position = new Vector3(col * spacing, row * spacing, 0); 

                // Instantiate the prefab and set its parent to the parent object
                GameObject newPrefab = Instantiate(prefab, position, Quaternion.identity, parentObject.transform);

                // Check if the current cell is occupied by a larger child prefab
                bool isOccupied = false;
                foreach (Vector2Int cell in GetOccupiedCells(col, row))
                {
                    if (occupiedCells.Contains(cell))
                    {
                        isOccupied = true;
                        break;
                    }
                }

                if (!isOccupied)
                {

                    // Instantiate a child object for the prefab
                    GameObject newChild = Instantiate(childPrefab, newPrefab.transform);
                    newChild.transform.localPosition = Vector3.zero; // Center the child object relative to the parent prefab

                    // Choose a random size for the child object from the list of available sizes
                    Vector2 childSize = childSizes[Random.Range(0, childSizes.Length)];

                    // Set the scale of the child object to the chosen size
                    newChild.transform.localScale = new Vector3(childSize.x, childSize.y, 1);

                    // Mark the cells occupied by the larger child prefab
                    foreach (Vector2Int cell in GetOccupiedCells(col, row, childSize))
                    {
                        occupiedCells.Add(cell);
                    }
                }
            }
        }
    }

    List<Vector2Int> GetOccupiedCells(int col, int row, Vector2 size = default)
    {
        if (size == default)
        {
            size = new Vector2(1, 1);
        }

        List<Vector2Int> cells = new List<Vector2Int>();

        for (int r = row; r < row + size.y; r++)
        {
            for (int c = col; c < col + size.x; c++)
            {
                if (c < columns && r < rows)
                {
                    cells.Add(new Vector2Int(c, r));
                }
            }
        }

        return cells;
    }
}

*/

/*

// Code trying to implement the feature which turns the cells red if they are spanned by an enlarged child prefab.
i.e. if the child prefab spans more than one cell, all spanned cells turn red.

using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject prefab; // The prefab to instantiate in the grid
    public GameObject childPrefab; // The child prefab to instantiate in each grid cell
    public int rows; // The number of rows in the grid
    public int columns; // The number of columns in the grid
    public float spacing = 1f; // The spacing between each prefab
    public Vector3 minScale = new Vector3(0.5f, 0.5f, 0.5f); // The minimum scale value for the child object
    public Vector3 maxScale = new Vector3(2.0f, 2.0f, 1.5f); // The maximum scale value for the child object

    void Start()
    {
        // Create a new game object to act as the parent for the instantiated prefabs
        GameObject parentObject = new GameObject("Grid");

        // Loop through each row and column and instantiate a prefab with a child object
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector3 position = new Vector3(col * spacing, row * spacing, 0); // Calculate the position of the prefab
                GameObject newPrefab = Instantiate(prefab, position, Quaternion.identity, parentObject.transform); // Instantiate the prefab and set its parent to the parent object
                newPrefab.GetComponent<Renderer>().material.color = Color.yellow; // Set the color of the instantiated prefab to yellow

                // Instantiate a child object for the prefab
                GameObject newChild = Instantiate(childPrefab, newPrefab.transform);
                newChild.transform.localPosition = Vector3.zero; // Center the child object relative to the parent prefab
                newChild.transform.localScale = new Vector3(Random.Range(minScale.x, maxScale.x), Random.Range(minScale.y, maxScale.y), Random.Range(minScale.z, maxScale.z)); // Set the scale of the child object to a random value within the specified range

                // Check if the child object's bounds overlap with any other grid cell and change their color accordingly
                Bounds childBounds = newChild.GetComponent<Renderer>().bounds;
                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < columns; c++)
                    {
                        GameObject currentCell = parentObject.transform.GetChild(r * columns + c).gameObject;
                        Bounds currentBounds = currentCell.GetComponent<Renderer>().bounds;
                        if (childBounds.Intersects(currentBounds))
                        {
                            currentCell.GetComponent<Renderer>().material.color = Color.red;
                        }
                    }
                }
            }
        }
    }
}

*/


// Implementing a feature which makes the child prefabs assume size scaled by Custom Float Ranged Values
// and also the parent prefab is instantiated as Yellow.

using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject prefab; // The prefab to instantiate in the grid
    public GameObject childPrefab; // The child prefab to instantiate in each grid cell
    public int rows; // The number of rows in the grid
    public int columns; // The number of columns in the grid
    public float spacing = 1f; // The spacing between each prefab
    public Vector3 minScale = new Vector3( 0.5f, 0.5f, 0.5f ); // The minimum scale value for the child object
    public Vector3 maxScale = new Vector3( 2.0f, 2.0f, 1.5f ); // The maximum scale value for the child object

    void Start()
    {
        // Create a new game object to act as the parent for the instantiated prefabs
        GameObject parentObject = new GameObject("Grid");

        // Loop through each row and column and instantiate a prefab with a child object
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector3 position = new Vector3(col * spacing, row * spacing, 0); // Calculate the position of the prefab
                GameObject newPrefab = Instantiate(prefab, position, Quaternion.identity, parentObject.transform); // Instantiate the prefab and set its parent to the parent object
                newPrefab.GetComponent<Renderer>().material.color = Color.yellow; // Set the color of the instantiated prefab to yellow

                // Instantiate a child object for the prefab
                GameObject newChild = Instantiate(childPrefab, newPrefab.transform);
                newChild.transform.localPosition = Vector3.zero; // Center the child object relative to the parent prefab
                // newChild.transform.localScale = new Vector3(Random.Range(minScale.x, maxScale.x), Random.Range(minScale.y, maxScale.y), Random.Range(minScale.z, maxScale.z)); // Set the scale of the child object to a random value within the specified range
            }
        }
    }
}
