using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public ShipManger manager;

    public Vector3 Location;
    public float Velocity;
    public float Rotation;

    // Start is called before the first frame update
    void Start()
    {
        Location = new Vector3(Screen.width / 2, Screen.height / 2);
        Velocity = 0;
        Rotation = 0;
    }
    // Update is called once per frame
    void Update()
    {
        DrawereringTool.DrawShip(Location, Rotation);
        DrawereringTool.DrawEscape(manager.screenSize);

        GetInputs();

        //Finds new location after movement factoring for Velocity + Rotation
        Location.x += Velocity * Mathf.Cos(MathTool.Deg2Rad(Rotation));
        Location.y += Velocity * Mathf.Sin(MathTool.Deg2Rad(Rotation));
    }
    void GetInputs()
    {
        if(Input.GetKey(KeyCode.D))
        {
            if(Rotation != 360)
            {
                Rotation += .5f;
            }
            else
            {
                Rotation = 0;
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (Rotation != 0)
            {
                Rotation -= .5f;
            }
            else
            {
                Rotation = 360;
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            if(Velocity < 1.5f)
            {
                Velocity += .01f;
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (Velocity > 0)
            {
                Velocity -= .05f;
            }
        }
    }
}
