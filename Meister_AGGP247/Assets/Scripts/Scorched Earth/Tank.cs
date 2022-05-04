using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    SCCanvas.Scene canvas;
    public float Rotation;
    float Power;
    public Vector3 Position;
    Vector3 barLoc; //Location of bullet spawn on barrel (barrelLocation)
    static float Gravity = 9.8f;
    public bool Tracing; //shows path of bullet
    public bool Targeting; //shows point of impact
    List<Vector3> pathpoints;
    Projectile cb;
    bool mode = false; //true for movement, false for shooting
    public bool Fire;

    public int playerNum;
    public bool myTurn;
    public bool drawHitbox;

    public Vector3 HBtopLeft;
    public Vector3 HBbottomRight;

    // Start is called before the first frame update
    void Start()
    {
        Power = 50;
    }
    // Update is called once per frame
    void Update()
    {
        canvas = SCCanvas.instance.canvas;
        drawTank();
        if (myTurn)
        {
            if (Fire)
            {
                spawnProjectile();
                SCManager.instance.EndTurn();
            }
            GetInputs();
            drawPath();
        }

    }
    void spawnProjectile()
    {
        if (cb == null)
        {
            cb = gameObject.AddComponent(typeof(Projectile)) as Projectile;
        }
        cb.newCB(pathpoints, playerNum);
        cb.hit = false;
    }
    void drawPath()
    {
        pathpoints = new List<Vector3>();
        float h = barLoc.y; //height
        float t = 0; //time
        while (h > canvas.groundY)
        {
            h = barLoc.y + Power * Mathf.Sin(Rotation * Mathf.Deg2Rad) * t - Gravity * Mathf.Pow(t, 2) / 2;
            pathpoints.Add(new Vector3((barLoc.x + Power * Mathf.Cos(Rotation * Mathf.Deg2Rad) * t), (barLoc.y + Power * Mathf.Sin(Rotation * Mathf.Deg2Rad) * t - Gravity * Mathf.Pow(t, 2) / 2)));
            t += .01f;
        }
        if (Targeting)
        {
            DrawereringTool.drawOrigin(new Vector3((barLoc.x + Power * Mathf.Cos(Rotation * Mathf.Deg2Rad) * t), (barLoc.y + Power * Mathf.Sin(Rotation * Mathf.Deg2Rad) * t - Gravity * Mathf.Pow(t, 2) / 2)), 5, Color.cyan);
        }
        if (Tracing)
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
        if(myTurn)
        {
            //SWITCH
            if (Input.GetKeyDown(KeyCode.M))
            {
                mode = !mode;
            }

            //MOVEMENT
            if (mode)
            {
                if(playerNum == 1)
                {
                    if (Input.GetKey(KeyCode.A) && Position.x > -480)
                    {
                        Position.x -= .25f * canvas.Zoom;
                    }
                    if (Input.GetKey(KeyCode.D) && Position.x < SCManager.instance.wall.topLeft.x - 25)
                    {
                        Position.x += .25f * canvas.Zoom;
                    }
                }

                if (playerNum == 2)
                {
                    if (Input.GetKey(KeyCode.A) && Position.x < 480)
                    {
                        Position.x -= .25f * canvas.Zoom;
                    }
                    if (Input.GetKey(KeyCode.D) && Position.x > SCManager.instance.wall.bottomRight.x + 25)
                    {
                        Position.x += .25f * canvas.Zoom;
                    }
                }
            }

            //FIRE
            else
            {
                Fire = Input.GetKeyDown(KeyCode.Space);

                if (Input.GetKey(KeyCode.D))
                {
                    if (Rotation != 360)
                    {
                        Rotation += .25f;
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
                        Rotation -= .25f;
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
        }
    }

    void drawTank()
    {
        Color tankColor;
        Color wheelColor;
        if(playerNum == 1)
        {
            tankColor = Color.cyan;
            wheelColor = Color.red;
        }
        else
        {
            tankColor = Color.red;
            wheelColor = Color.cyan;
        }

        float xzoomed = canvas.origin.x + Position.x * canvas.Zoom;

        if (myTurn)
        {
            DrawereringTool.drawPowerbar(Position, Power, Color.yellow, canvas);
            DrawereringTool.drawRotatebar(Position, Rotation / 360 * 100, Color.blue, canvas);
            if (mode) { DrawereringTool.drawOrigin(new Vector3(xzoomed, canvas.origin.y, 0), 20 * canvas.Zoom, Color.blue); }
            else { DrawereringTool.drawOrigin(new Vector3(xzoomed, canvas.origin.y, 0), 20 * canvas.Zoom, Color.red); }
        }

        Vector3 BodyA = new Vector3(xzoomed - (20 * canvas.Zoom), canvas.groundY + (30 * canvas.Zoom), 0);
        Vector3 BodyB = new Vector3(xzoomed + (20 * canvas.Zoom), canvas.groundY + (30 * canvas.Zoom), 0);
        Vector3 BodyC = new Vector3(xzoomed + (30 * canvas.Zoom), canvas.groundY + (10 * canvas.Zoom), 0); 
        Vector3 BodyD = new Vector3(xzoomed - (30 * canvas.Zoom), canvas.groundY + (10 * canvas.Zoom), 0);
        DrawereringTool.drawRectangle(BodyA, BodyB, BodyC, BodyD, tankColor);

        Vector3 headA = new Vector3(xzoomed - (7 * canvas.Zoom), BodyA.y + (10 * canvas.Zoom)); 
        Vector3 headB = new Vector3(xzoomed + (7 * canvas.Zoom), BodyA.y + (10 * canvas.Zoom));
        Vector3 headC = new Vector3(xzoomed + (12 * canvas.Zoom), BodyA.y);
        Vector3 headD = new Vector3(xzoomed - (12 * canvas.Zoom), BodyA.y);
        DrawereringTool.drawRectangle(headA, headB, headC, headD, tankColor);

        Vector3 rotationpoint = (headA + headB + headC + headD) / 4;
        Vector3 barA = MathTool.RotatePoint(rotationpoint, Rotation, new Vector3(rotationpoint.x, rotationpoint.y + (2.5f * canvas.Zoom), 0));
        Vector3 barB = MathTool.RotatePoint(rotationpoint, Rotation, new Vector3(rotationpoint.x + (30 * canvas.Zoom), rotationpoint.y + (2.5f * canvas.Zoom), 0));
        Vector3 barC = MathTool.RotatePoint(rotationpoint, Rotation, new Vector3(rotationpoint.x + (30 * canvas.Zoom), rotationpoint.y - (2.5f * canvas.Zoom), 0));
        Vector3 barD = MathTool.RotatePoint(rotationpoint, Rotation, new Vector3(rotationpoint.x, rotationpoint.y - (2.5f * canvas.Zoom), 0));
        barLoc = new Vector3((barB.x + barC.x) / 2, (barB.y + barC.y) / 2);
        DrawereringTool.drawRectangle(barA, barB, barC, barD, tankColor);

        Vector3 WheelA = new Vector3(BodyD.x + (5 * canvas.Zoom), BodyD.y + (2.5f * canvas.Zoom));
        Vector3 WheelB = new Vector3(BodyC.x - (5 * canvas.Zoom), BodyC.y + (2.5f * canvas.Zoom));
        float rad = 10 * canvas.Zoom;
        DrawereringTool.drawCircle(16, rad, WheelA, wheelColor);
        DrawereringTool.drawCircle(16, rad, WheelB, wheelColor);


        HBtopLeft = new Vector3(BodyD.x - (5 * canvas.Zoom), headA.y + (5 * canvas.Zoom));
        HBbottomRight = new Vector3(BodyC.x + (5 + canvas.Zoom), BodyC.y - (5 * canvas.Zoom));
        if (drawHitbox)
        {
            DrawereringTool.drawRectangle(HBtopLeft, HBbottomRight, Color.green);
        }
    }
}
