using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public int originType;

    public Vector3 Curser;

    public Color axisColor = Color.white;
    public Color lineColor = Color.gray;
    public Color divisionColor = Color.yellow;

    public bool isDrawingOrigin = true;
    public bool isDrawingAxis = true;
    public bool isDrawingDivisions = true;
    public bool isDrawingCurser = true;

    public Grid grid = new Grid();


    public Text gridData;

    public int originSize = 3;

    public bool drawCircle;
    public bool drawEllipse;

    private void Start()
    {
        grid.screenSize = new Vector3(Screen.width, Screen.height);
        grid.origin = new Vector3(Screen.width / 2, Screen.height / 2);
        Curser = grid.origin;
    }

    void Update()
    {
        UpdateData();
        getInputs();
        if (isDrawingCurser)
        {
            drawCurser(Curser);
        }
        if (isDrawingOrigin)
        {
            DrawOrigin(grid.origin, originSize, originType);
        }
        if(isDrawingDivisions)
        {
            DrawGrid();
        }
        if(drawCircle)
        {
            DrawCircle(Curser, 1, 32, Color.red);
        }
        if(drawEllipse)
        {
            DrawereringTool.DrawEllipse(grid.origin, 10, 36);
        }
    }
    //Special version to work with Curser
    public void DrawCircle(Vector3 Position, float Radius, int Sides, Color color)
    {
        Vector3 point;
        Vector3 lastpoint = new Vector3();
        float num = 360 / Sides;
        int count = 0;
        Circle OSCircle = new Circle(grid.origin, Position, Radius, Sides, 0);
        point = new Vector3(OSCircle.Position.x, OSCircle.Position.y + Radius, 0);
        while (count <= Sides)
        {
            lastpoint = MathTool.CircleRadiusPoint(grid.origin, Position, num + (num * count++), Radius);
            Glint.AddCommand(new Line(point, lastpoint, color));
            point = lastpoint;
        }
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
            DrawereringTool.drawOrigin(drawPoint, size, Color.red);
        }
        if(type == 2)
        {
            DrawereringTool.drawTicker(drawPoint, size);
        }
        if (type == 3)
        {
            DrawereringTool.drawHexagon(drawPoint, size);
        }
        if (type == 4)
        {
            DrawereringTool.ParabolaA();
        }
        if (type == 5)
        {
            DrawereringTool.ParabolaB();
        }
        if (type == 6)
        {
            DrawereringTool.ParabolaC();
        }
        if (type == 7)
        {
            DrawereringTool.ParabolaD();
        }
        if (type == 8)
        {
            DrawereringTool.drawDiamond(drawPoint);
        }
    }
    public void drawCurser(Vector3 Curser)
    {
        Glint.AddCommand(new Line(new Vector3(Curser.x + (.1f * grid.gridSize), Curser.y, 0), new Vector3(Curser.x + (.5f * grid.gridSize), Curser.y, 0), Color.cyan));
        Glint.AddCommand(new Line(new Vector3(Curser.x, Curser.y + (.1f * grid.gridSize), 0), new Vector3(Curser.x, Curser.y + (.5f * grid.gridSize), 0), Color.cyan));
        Glint.AddCommand(new Line(new Vector3(Curser.x - (.1f * grid.gridSize), Curser.y, 0), new Vector3(Curser.x - (.5f * grid.gridSize), Curser.y, 0), Color.cyan));
        Glint.AddCommand(new Line(new Vector3(Curser.x, Curser.y - (.1f * grid.gridSize), 0), new Vector3(Curser.x, Curser.y - (.5f * grid.gridSize), 0), Color.cyan));
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
    void UpdateData()
    {
        gridData.text = "T:(1024x768)" +
            "\nResolution = " + grid.screenSize +
            "\nOrigin = " + grid.origin +
            "\nCurser Location = " + Curser + " (" + MathTool.ScreenToGrid(Curser, grid) + ")" +
            "\nMouseLocation = " + Input.mousePosition +
            "\nDivision Count = " + grid.divisionCount + 
            "\n" + Time.deltaTime;
    }

    public void getInputs()
    {
        if(Input.GetKey(KeyCode.LeftControl)) 
        {
            DrawereringTool.GetParabolas();
            grid.divisionCount += (int)Mathf.Round(Input.mouseScrollDelta.y);
        }
        else
        {
            if(grid.gridSize > grid.minGridSize)
            {
                grid.gridSize += Input.mouseScrollDelta.y;
            }
            else if (grid.gridSize <= grid.minGridSize && Input.mouseScrollDelta.y > 0)
            {
                grid.gridSize += Input.mouseScrollDelta.y;
            }


        }

        if (Input.GetMouseButton(1))
        {
            Curser = Input.mousePosition;
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

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            isDrawingCurser = !isDrawingCurser;
        }

        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            if(originType < 8)
            {
                originType++;
            }
            else
            {
                originType = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            drawCircle = !drawCircle;
            drawEllipse = false;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            drawCircle = false;
            drawEllipse = !drawEllipse;
        }
    }
}

