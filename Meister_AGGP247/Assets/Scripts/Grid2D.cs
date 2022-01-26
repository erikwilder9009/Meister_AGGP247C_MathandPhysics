using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid2D : MonoBehaviour
{
    public class Grid
    {
        public Vector3 screenSize;
        public Vector3 origin;

        public float gridSize = 10f;
        public float minGridSize = 2f;
        public float originSize = 20f;

        public int divisionCount = 5;
        public int minDivisionCount = 2;
    }

    public Color axisColor = Color.white;
    public Color lineColor = Color.gray;
    public Color divisionColor = Color.yellow;

    public bool isDrawingOrigin = true;
    public bool isDrawingAxis = true;
    public bool isDrawingDivisions = true;

    Grid grid = new Grid();

    //User Inputs
    float zoomGrid;
    float zoomDivision;
    bool moveGrid;
    bool toggleOrigin;
    bool toggleAxis;
    bool toggleDivisions;


    private void Start()
    {
        grid.screenSize = new Vector3(Screen.width, Screen.height);
        grid.origin = new Vector3(Screen.width / 2, Screen.height / 2);
    }

    void Update()
    {
        getInputs();
        if (isDrawingOrigin)
        {
            DrawOrigin(grid.origin);
        }
        if(isDrawingDivisions)
        {
            DrawGrid();
        }
    }

    //Takes the potential grid space and outputs it into screen space
    public Vector3 GridToScreen(Vector3 gridSpace)
    {
        return Vector3.zero;
    }

    //Takes in screen space and outputs it as grid space
    public Vector3 ScreenToGrid(Vector3 screenSpace)
    {
        return Vector3.zero;
    }

    //Draws the given line
    public void DrawLine(Line line)
    {
        Glint.AddCommand(line);
    }

    //Draws the Origin Point(or Symbol)
    public void DrawOrigin(Vector3 drawPoint)
    {
        int size = 3;
        DrawLine(new Line(new Vector3(drawPoint.x + size, drawPoint.y, 0), new Vector3(drawPoint.x, drawPoint.y + size, 0), Color.red));
        DrawLine(new Line(new Vector3(drawPoint.x, drawPoint.y + size, 0), new Vector3(drawPoint.x - size, drawPoint.y, 0), Color.red));
        DrawLine(new Line(new Vector3(drawPoint.x - size, drawPoint.y, 0), new Vector3(drawPoint.x, drawPoint.y - size, 0), Color.red));
        DrawLine(new Line(new Vector3(drawPoint.x, drawPoint.y - size, 0), new Vector3(drawPoint.x + size, drawPoint.y, 0), Color.red));
    }

    public void DrawGrid()
    {
        float drawSize = grid.gridSize * grid.divisionCount;
        Vector2 Max = new Vector2(grid.origin.x + drawSize , grid.origin.y + drawSize);
        Vector2 Min = new Vector2(grid.origin.x - drawSize, grid.origin.y - drawSize);
        Vector2 V = new Vector2 (Min.x, Min.y);

        while (V.x <= Max.x)
        {
            DrawLine(new Line(new Vector3(V.x, Max.y, 0), new Vector3(V.x, Min.y, 0), ColorCheck(V.x)));
            V.x += grid.gridSize;
        }

        while (V.y <= Max.y)
        {
            DrawLine(new Line(new Vector3(Max.x, V.y, 0), new Vector3(Min.x, V.y, 0), ColorCheck(V.y)));
            V.y += grid.gridSize;
        }
    }
    public Color ColorCheck(float num)
    {
        Color drawColor;
        if (num == grid.origin.x || num == grid.origin.y)
        {
            drawColor = divisionColor;
        }
        else
        {
            drawColor = lineColor;
        }
        return drawColor;
    }


    public void getInputs()
    {
        if(Input.GetKey(KeyCode.LeftControl))
        {
            grid.divisionCount += (int)Mathf.Round(Input.mouseScrollDelta.y);
        }
        else
        {
            grid.gridSize += Input.mouseScrollDelta.y;
        }

        if (Input.GetMouseButton(2))
        {
            grid.origin = Input.mousePosition;
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            isDrawingAxis = !isDrawingAxis;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isDrawingDivisions = !isDrawingDivisions;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            isDrawingOrigin = !isDrawingOrigin;
        }

    }
}

