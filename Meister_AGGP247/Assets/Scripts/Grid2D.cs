using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid2D : MonoBehaviour
{
    DrawereringTool drawerer = new DrawereringTool();
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

    public int originType;

    public Vector3 Curser;

    public Color axisColor = Color.white;
    public Color lineColor = Color.gray;
    public Color divisionColor = Color.yellow;

    public bool isDrawingOrigin = true;
    public bool isDrawingAxis = true;
    public bool isDrawingDivisions = true;
    public bool isDrawingCurser = true;

    Grid grid = new Grid();


    private void Start()
    {
        grid.screenSize = new Vector3(Screen.width, Screen.height);
        grid.origin = new Vector3(Screen.width / 2, Screen.height / 2);
        Curser = grid.origin;
        Debug.Log("Origin = " + grid.origin);
    }

    void Update()
    {
        getInputs();
        if (isDrawingCurser)
        {
            drawCurser(Curser);
        }
        if (isDrawingOrigin)
        {
            DrawOrigin(grid.origin, 3, originType);
        }
        if(isDrawingDivisions)
        {
            DrawGrid();
        }
    }

    //Takes the potential grid space and outputs it into screen space
    public Vector3 GridToScreen(Vector3 gridSpace)
    {
        return (gridSpace * grid.gridSize) + grid.origin;
    }

    //Takes in screen space and outputs it as grid space
    public Vector3 ScreenToGrid(Vector3 screenSpace)
    {
        return (screenSpace - grid.origin) / grid.gridSize;
    }

    //Draws the given line
    public void DrawLine(Line line)
    {
        Glint.AddCommand(line);
    }

    //Draws the Origin Point(or Symbol)
    public void DrawOrigin(Vector3 drawPoint, float size, int type)
    {
        if(type == 1)
        {
            drawerer.drawOrigin(drawPoint, size, Color.red);
        }
        if(type == 2)
        {
            drawerer.drawTicker(drawPoint, size);
        }
        if (type == 3)
        {
            drawerer.drawHexagon(drawPoint, size);
        }
        if (type == 4)
        {
            drawerer.ParabolasA();
        }
        //if (type == 5)
        //{
        //    drawerer.ParabolasB();
        //}
        //if (type == 6)
        //{
        //    drawerer.ParabolasC();
        //}
        //if (type == 7)
        //{
        //    drawerer.ParabolasD();
        //}
        if (type == 8)
        {
            drawerer.drawDiamond(drawPoint);
        }
        if (type == 9)
        {
        }
        else
        {
            drawerer.drawOrigin(drawPoint, size, Color.red);
        }
    }
    public void drawCurser(Vector3 Curser)
    {
        Glint.AddCommand(new Line(new Vector3(Curser.x + 2, Curser.y, 0), new Vector3(Curser.x + 5, Curser.y, 0), Color.cyan));
        Glint.AddCommand(new Line(new Vector3(Curser.x, Curser.y + 2, 0), new Vector3(Curser.x, Curser.y + 5, 0), Color.cyan));
        Glint.AddCommand(new Line(new Vector3(Curser.x - 2, Curser.y, 0), new Vector3(Curser.x - 5, Curser.y, 0), Color.cyan));
        Glint.AddCommand(new Line(new Vector3(Curser.x, Curser.y - 2, 0), new Vector3(Curser.x, Curser.y - 5, 0), Color.cyan));
    }

    public void DrawGrid()
    {
        float drawSize = grid.gridSize * grid.divisionCount;
        Vector2 Max = new Vector2(grid.origin.x + drawSize , grid.origin.y + drawSize);
        Vector2 Min = new Vector2(grid.origin.x - drawSize, grid.origin.y - drawSize);
        Vector2 V = new Vector2 (Min.x, Min.y);

        while (V.x <= Max.x)
        {
            Glint.AddCommand(new Line(new Vector3(V.x, Max.y, 0), new Vector3(V.x, Min.y, 0), ColorCheck(V.x)));
            V.x += grid.gridSize;
        }

        while (V.y <= Max.y)
        {
            Glint.AddCommand(new Line(new Vector3(Max.x, V.y, 0), new Vector3(Min.x, V.y, 0), ColorCheck(V.y)));
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

        if (Input.GetMouseButton(1))
        {
            Curser = Input.mousePosition;
            Debug.Log("New curser = " + Curser);
            Debug.Log("Chart point = " + ScreenToGrid(Curser));
        }

        if (Input.GetMouseButton(2))
        {
            grid.origin = Input.mousePosition;
            Debug.Log("New Origin = " + grid.origin);
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

