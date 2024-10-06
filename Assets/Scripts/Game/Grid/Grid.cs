using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int columns = 0;
    public int rows = 0;
    public float squareGaps = 0.1f;
    public GameObject gridSquare; 
    public Vector2 startPosition = new Vector2(0f, 0f);
    public float squareScale = 0.5f;
    public float everysquareOffset = 0f;

    private Vector2 _offset = new Vector2(0f, 0f);
    private List<GameObject> _gridSquares = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        GenerateGridSquares();
        SetGridSquarePositions();
    }

    private void GenerateGridSquares()
    {
        int square_index = 0;
        for (var row = 0; row< rows; row++)
        {
            for(var column = 0; column< columns; column++)
            {
                _gridSquares.Add(Instantiate(gridSquare) as GameObject);
                int index = _gridSquares.Count - 1;
                _gridSquares[index].transform.SetParent(this.transform);
                _gridSquares[index].transform.localScale = new Vector3(squareScale, squareScale, squareScale);
                _gridSquares[index].GetComponent<GridSquare>().SetImage(square_index % 2 == 0);
                square_index++;
            }

        }


    }

    private void SetGridSquarePositions()
    {
        int column_number = 0;
        int row_number = 0;
        Vector2 square_gap_number = new Vector2(0f, 0f);
        bool row_moved = false;

        var square_rect = _gridSquares[0].GetComponent<RectTransform>();

        _offset.x = square_rect.rect.width * square_rect.transform.localScale.x + everysquareOffset;
        _offset.y = square_rect.rect.height * square_rect.transform.localScale.y + everysquareOffset;

        foreach (GameObject square in _gridSquares)
        {
            if (column_number + 1 >  columns)
            {
                square_gap_number.x = 0;
                // go to next row;
                column_number = 0;
                row_number++;
                row_moved = false;
            }

            var pos_x_offset = _offset.x * column_number + (square_gap_number.x * squareGaps);
            var pos_y_offset = _offset.y * row_number + (square_gap_number.y * squareGaps);

            if (column_number > 0 && column_number % 3 == 0)
            {
                square_gap_number.x++;
                pos_x_offset += squareGaps;
            }

            if (row_number > 0 && row_number % 3 == 0 && row_moved == false)
            {
                row_moved = true;
                square_gap_number.y++;
                pos_y_offset += squareGaps;
            }

            square.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                startPosition.x + pos_x_offset,
                startPosition.y - pos_y_offset);
            square.GetComponent<RectTransform>().localPosition = new Vector3(
                startPosition.x + pos_x_offset,
                startPosition.y - pos_y_offset,
                0f
                );

            column_number++;

        }
    }
}
