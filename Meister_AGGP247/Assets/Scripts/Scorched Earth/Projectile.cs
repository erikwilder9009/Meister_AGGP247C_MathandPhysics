using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    List<Vector3> PathPoints;
    int index;
    public Vector3 curLoc;
    public int playerNum;

    public bool hit;

    void Start()
    {
        index = 0;
        hit = false;
    }
    void Update()
    {
        curLoc = PathPoints[index];
        DrawereringTool.drawOrigin(PathPoints[index], 10, Color.yellow);
        if (index < PathPoints.Count - 2 && hit == false)
        {
            index += 2;
        }

        if (playerNum == 2 && MathTool.PointInRectangle(curLoc, SCManager.instance.tankA.HBtopLeft, SCManager.instance.tankA.HBbottomRight) && hit == false)
        {
            SCManager.instance.p2Score++;
            hit = true;
        }
        if (playerNum == 1 && MathTool.PointInRectangle(curLoc, SCManager.instance.tankB.HBtopLeft, SCManager.instance.tankB.HBbottomRight) && hit == false)
        {
            SCManager.instance.p1Score++;
            hit = true;
        }
        if (MathTool.PointInRectangle(curLoc, SCCanvas.instance.canvas.origin + SCManager.instance.wall.topLeft * SCCanvas.instance.canvas.Zoom, new Vector3(SCCanvas.instance.canvas.origin.x + SCManager.instance.wall.bottomRight.x * SCCanvas.instance.canvas.Zoom, SCCanvas.instance.canvas.groundY, 0)) && hit == false)
        {
            hit = true;
        }
    }
    public void newCB(List<Vector3> points, int playNum)
    {
        index = 0;
        PathPoints = points;
        playerNum = playNum;
    }
}
