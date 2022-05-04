using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCManager : MonoBehaviour
{
    public bool MStart;

    public static SCManager instance;

    SCCanvas.Scene canvas;

    public int p1Score;
    public int p2Score;

    public Tank tankA;
    public Tank tankB;
    bool loaded;

    public bool loadWall;
    public Wall wall;

    public bool drawHitboxes;

    void Awake()
    {
        MStart = false;

        instance = this;
        gameObject.AddComponent<SCCanvas>();
        canvas = SCCanvas.instance.canvas;
        if (loadWall)
        {
            wall = gameObject.AddComponent<Wall>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(MStart)
        {
            DrawereringTool.SCEscape(canvas);
            DrawereringTool.drawNum(p1Score, 15, new Vector3(canvas.frameB.x - (100 * canvas.Zoom), canvas.frameB.y - (100 * canvas.Zoom)), Color.yellow);
            DrawereringTool.drawNum(p2Score, 15, new Vector3(canvas.frameA.x + (100 * canvas.Zoom), canvas.frameB.y - (100 * canvas.Zoom)), Color.yellow);
            if (p1Score > 2)
            {
                EndGame(1);
            }
            if (p2Score > 2)
            {
                EndGame(2);
            }


            if (SCCanvas.instance.run && !loaded)
            {
                tankA = gameObject.AddComponent<Tank>();
                tankA.Position = new Vector3(-450, canvas.groundY);
                tankA.playerNum = 1;
                tankA.myTurn = true;
                tankB = gameObject.AddComponent<Tank>();
                tankB.Position = new Vector3(450, canvas.groundY);
                tankB.Rotation = 180;
                tankB.playerNum = 2;
                tankB.myTurn = false;
                loaded = true;
            }

            if (drawHitboxes)
            {
                DrawereringTool.drawRectangle(tankA.HBtopLeft, tankA.HBbottomRight, Color.magenta);
                DrawereringTool.drawRectangle(tankB.HBtopLeft, tankB.HBbottomRight, Color.magenta);
                DrawereringTool.drawRectangle(canvas.origin + wall.topLeft * canvas.Zoom, new Vector3(canvas.origin.x + wall.bottomRight.x * canvas.Zoom, canvas.groundY, 0), Color.magenta);
            }
        }
    }

    public void EndTurn()
    {
        tankA.myTurn = !tankA.myTurn;
        tankB.myTurn = !tankB.myTurn;
        tankA.Fire = false;
        tankB.Fire = false;
    }

    public void EndGame(int winner)
    {
        if(winner == 1)
        {
            tankA.Tracing = true;
            tankA.Targeting = true;
            tankA.Fire = true;
            tankA.myTurn = true; 
            tankB.Tracing = false;
            tankB.Targeting = false;
            tankB.Fire = false;
            tankB.myTurn = false;
        }
        if(winner == 2)
        {
            
            
            tankB.Fire = true;
            tankA.Fire = false;
            tankB.myTurn = true;
            tankB.Tracing = true;
            tankA.myTurn = false;
            tankA.Tracing = false;
            tankB.Targeting = true;
            tankA.Targeting = false;
        }
        drawHitboxes = true;
    }
}
