using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCManager : MonoBehaviour
{
    SCCanvas canvas;
    Tank tankA;
    Tank tankB;
    bool loaded;
    // Start is called before the first frame update
    void Start()
    {
        canvas = gameObject.AddComponent<SCCanvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canvas.run && !loaded)
        {
            tankA = gameObject.AddComponent<Tank>();
            tankA.move = new Vector3(75, canvas.canvas.groundY);
            tankA.player1 = true;
            tankA.myTurn = true;
            tankB = gameObject.AddComponent<Tank>();
            tankB.move = new Vector3(canvas.canvas.screenSize.x - 75, canvas.canvas.groundY);
            tankB.Rotation = 180;
            tankB.player1 = false;
            tankB.myTurn = false;
            loaded = true;
        }
    }
}
