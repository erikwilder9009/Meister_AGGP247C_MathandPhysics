using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Canon : MonoBehaviour
{
    public Vector3 origin;
    public Vector3 screenSize;
    float Rotation;
    float Power;
    Vector3 barLoc; //Location of bullet spawn on barrel (barrelLocation)
    static float Gravity = 9.8f;
    public bool Tracing = true; //shows path of bullet
    public bool Targeting = true; //shows point of impact
    List<Vector3> pathpoints;
    CanonBall cb;
    bool Fire;
    // Start is called before the first frame update
    void Start()
    {
        screenSize = new Vector3(Screen.width, Screen.height);
        origin = new Vector3(Screen.width / 2, Screen.height / 2);
        Rotation = 0;
        Power = 0;
    }
    // Update is called once per frame
    void Update()
    {
        DrawereringTool.DrawEscape(screenSize);
        GetInputs();
        drawScene();
        DrawereringTool.drawGaugebar(Power, Color.green);
        drawPath();


        if(Fire)
        {
            spawnProjectile();
        }
    }
    void spawnProjectile()
    {
        if(cb == null)
        {
            cb = gameObject.AddComponent(typeof(CanonBall)) as CanonBall;
        }
        cb.newCB(pathpoints);
    }
    void drawPath()
    {
        pathpoints = new List<Vector3>();
        float h = barLoc.y;
        float t = 0;
        while(h > origin.y / 2)
        {
            h = barLoc.y + Power * Mathf.Sin(Rotation * Mathf.Deg2Rad) * t - Gravity * Mathf.Pow(t, 2) / 2;
            pathpoints.Add(new Vector3((barLoc.x + Power * Mathf.Cos(Rotation * Mathf.Deg2Rad) * t), (barLoc.y + Power * Mathf.Sin(Rotation * Mathf.Deg2Rad) * t - Gravity * Mathf.Pow(t, 2) / 2)));
            t += .01f;
        }
        if(Targeting)
        {
            DrawereringTool.drawOrigin(new Vector3((barLoc.x + Power * Mathf.Cos(Rotation * Mathf.Deg2Rad) * t), (barLoc.y + Power * Mathf.Sin(Rotation * Mathf.Deg2Rad) * t - Gravity * Mathf.Pow(t, 2) / 2)), 5, Color.cyan);
        }
        if(Tracing)
        {
            int count = pathpoints.Count;
            int x = 1;
            Vector3 point;
            Vector3 lastpoint = pathpoints[0];
            while (x <= count - 1)
            {
                point = pathpoints[x];
                Glint.AddCommand(new Line(lastpoint, point, Color.grey));
                lastpoint = point;
                x += 1;
            }
        }
    }
    void GetInputs()
    {
        Fire = Input.GetKeyDown(KeyCode.Space);

        if (Input.GetKey(KeyCode.D))
        {
            if (Rotation != 360)
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
            if (Power < 100)
            {
                Power += .5f;
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (Power > 0)
            {
                Power -= .5f;
            }
        }
    }
    void drawScene()
    {
        //CannonBase
        Vector3 A = new Vector3(0 + 30, (origin.y / 2) + 90, 0);
        Vector3 B = new Vector3(0 + 80, (origin.y / 2) + 90, 0);
        Vector3 C = new Vector3(0 + 80, origin.y / 2, 0);
        Vector3 D = new Vector3(0 + 30, origin.y / 2, 0);
        DrawereringTool.drawRectangle(A, B, C, D, Color.red);
        DrawereringTool.fillRectangle(A, B, C, D, Color.red);

        //CannonHead
        float Radius = 30;
        Vector3 Center = new Vector3(0 + 55, (origin.y / 2) + 110, 0);
        DrawereringTool.drawCircle(36, Radius, Center, Color.red);
        DrawereringTool.fillCircle(Center, Radius, Color.red);

        //CannonBarrel
        Vector3 rotationpoint = new Vector3(0 + 55, (origin.y / 2) + 110, 0);
        A = MathTool.RotatePoint(rotationpoint, Rotation, new Vector3(0 + 55, (origin.y / 2) + 120, 0));
        B = MathTool.RotatePoint(rotationpoint, Rotation, new Vector3(0 + 110, (origin.y / 2) + 120, 0));
        C = MathTool.RotatePoint(rotationpoint, Rotation, new Vector3(0 + 110, (origin.y / 2) + 100, 0));
        D = MathTool.RotatePoint(rotationpoint, Rotation, new Vector3(0 + 55, (origin.y / 2) + 100, 0));
        barLoc = new Vector3((B.x + C.x) / 2, (B.y + C.y) / 2);
        DrawereringTool.drawRectangle(A, B, C, D, Color.red);
        DrawereringTool.fillRectangle(
            new Vector3(0 + 55, (origin.y / 2) + 120), 
            new Vector3(0 + 110, (origin.y / 2) + 120), 
            new Vector3(0 + 110, (origin.y / 2) + 100), 
            new Vector3(0 + 55, (origin.y / 2) + 100), 
            Color.red, rotationpoint, Rotation);
    

        //Ground
        A = new Vector3(0, origin.y / 2);
        B = new Vector3(screenSize.x, origin.y / 2);
        C = new Vector3(screenSize.x, 0);
        D = new Vector3(0, 0);
        DrawereringTool.drawRectangle(A, B, C, D, Color.grey);
        DrawereringTool.fillRectangle(A, B, C, D, Color.grey);

    }
}
