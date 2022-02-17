using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle
{
    public Vector3 Origin = Vector2.zero;
    public Vector3 Position = Vector2.zero;
    public float Rotation = 0;
    public int Sides = 32;
    public float Width = 2.0f;
    public Color color = Color.red;



    public Circle() { }
    public Circle(Vector3 origin, Vector3 position, float width, int numSides, float rotation)
    {
        Origin = origin;
        Position = position;
        Rotation = rotation;
        Sides = numSides;
        Width = width;
    }
}
