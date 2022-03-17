using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    Vector3 mousePoint;
    public Grid2D grid;

    int shapeIndex;

    
    // Update is called once per frame
    void Update()
    {
        mousePoint = Input.mousePosition;


        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(shapeIndex == 2)
            {
                shapeIndex = 0;
            }
            else
            {
                shapeIndex++;
            }
        }
        if(shapeIndex == 0)
        {
            int sides = 36;
            float radius = 10 * grid.grid.gridSize;
            Vector3 center = grid.grid.origin;
            DrawereringTool.drawCircle(sides, radius, center, Color.red);
            if (MathTool.PointInCircle(center, radius, mousePoint))
            {
                DrawereringTool.fillCircle(center, radius, Color.green);
            }
        }
        if(shapeIndex == 1)
        {
            Vector3 A = MathTool.GridToScreen(new Vector3(0, 10, 0), grid.grid);
            Vector3 B = MathTool.GridToScreen(new Vector3(10, -10, 0), grid.grid);
            Vector3 C = MathTool.GridToScreen(new Vector3(-10, -10, 0), grid.grid);
            DrawereringTool.drawTriangle(A, B, C, Color.red);
            if (MathTool.PointInTriangle(mousePoint, A, B, C))
            {
                DrawereringTool.fillTriangle(A, C, B, Color.green);
            }
        }
        if(shapeIndex == 2)
        {
            Vector3 A = MathTool.GridToScreen(new Vector3(-10, 10, 0), grid.grid);
            Vector3 B = MathTool.GridToScreen(new Vector3(10, 10, 0), grid.grid);
            Vector3 C = MathTool.GridToScreen(new Vector3(10, -10, 0), grid.grid);
            Vector3 D = MathTool.GridToScreen(new Vector3(-10, -10, 0), grid.grid);
            DrawereringTool.drawRectangle(A, B, C, D, Color.red);
            if (MathTool.PointInRectangle(mousePoint, A, B, C, D))
            {
                DrawereringTool.fillRectangle(A, B, C, D, Color.green);
            }
        }
    }
}
