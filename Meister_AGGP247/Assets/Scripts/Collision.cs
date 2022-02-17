using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    Vector3 mousePoint;
    public Grid2D grid;
    MathTool MT = new MathTool();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePoint = Input.mousePosition;
        drawCircle();
    }

    void drawCircle()
    {
        int Sides = 36;
        float Radius = 100;
        Vector3 Position = new Vector3(grid.grid.origin.x, grid.grid.origin.y + Radius, 0);

        Vector3 point;
        Vector3 lastpoint = new Vector3();
        float num = 360 / Sides;
        int count = 0;
        Circle OSCircle = new Circle(grid.grid.origin, Position, Radius, Sides, 0);
        point = new Vector3(OSCircle.Position.x, OSCircle.Position.y, 0);
        while (count <= Sides)
        {
            lastpoint = MT.CircleRadiusPoint(grid.grid.origin, Position, num + (num * count++), Radius);
            Glint.AddCommand(new Line(point, lastpoint, Color.red));
            point = lastpoint;
        }
    }


    void drawTriangle()
    {

    }

    void drawRectangle()
    {

    }

    void getCollision()
    {

    }
}
