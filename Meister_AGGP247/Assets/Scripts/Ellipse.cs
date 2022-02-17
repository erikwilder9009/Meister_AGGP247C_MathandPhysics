using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ellipse : MonoBehaviour
{
    public Vector3 Position = Vector2.zero;
    public Vector3 Center = Vector2.zero;
    //public Vector3 Axis = Vector2.one;
    public float Rotation = 0;
    public int Sides = 32;
    public float WidthA = 2.0f;
    public float WidthB = 1.0f;
    public Color color = Color.red;

    public Ellipse(Vector3 Origin, Vector3 Center, float Width1, float Width2, int numSides, float rotation)
    {
        Center = Origin;
        Position = Center;
        WidthA = Width1;
        WidthB = Width2;
        Sides = numSides;
        Rotation = rotation;
    }
}
