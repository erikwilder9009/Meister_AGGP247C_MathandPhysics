using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCManager : MonoBehaviour
{
    public static SCManager instance;

    SCCanvas.Scene canvas;

    Tank tankA;
    Tank tankB;
    bool loaded;

    public bool loadWall;
    Wall wall;

    void Awake()
    {
        instance = this;
        gameObject.AddComponent<SCCanvas>();
        canvas = SCCanvas.instance.canvas;
        if(loadWall)
        {
            wall = gameObject.AddComponent<Wall>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(SCCanvas.instance.run && !loaded)
        {
            tankA = gameObject.AddComponent<Tank>();
            tankA.Position = new Vector3(-450, canvas.groundY);
            tankA.player1 = true;
            tankA.myTurn = true;
            tankB = gameObject.AddComponent<Tank>();
            tankB.Position = new Vector3(450, canvas.groundY);
            tankB.Rotation = 180;
            tankB.player1 = false;
            tankB.myTurn = false;
            loaded = true;
        }
    }

    public void EndTurn()
    {
        tankA.myTurn = !tankA.myTurn;
        tankB.myTurn = !tankB.myTurn;
    }
}
