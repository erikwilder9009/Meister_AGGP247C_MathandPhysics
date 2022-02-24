using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ellipse
{
    public Vector3 Position = Vector2.zero;
    public Vector3 Origin = Vector2.zero;
    public Vector3 Axis = Vector2.one;
    public int Sides = 32;
    public float Width = 2.0f;
    public Color color = Color.red;

    public Ellipse(Vector3 origin, Vector3 Center, Vector3 axis, float width, int numSides)
    {
        Origin = origin;
        Position = Center;
        Width = width;
        Sides = numSides;
        Axis = axis;
    }
}
