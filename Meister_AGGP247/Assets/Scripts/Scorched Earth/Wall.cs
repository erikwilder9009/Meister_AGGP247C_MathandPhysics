using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    SCCanvas.Scene canvas;

    [SerializeField]
    public Vector3 topLeft;
    [SerializeField]
    public Vector3 bottomRight;

    public bool drawWall = true;

    // Start is called before the first frame update
    void Start()
    {
        canvas = SCCanvas.instance.canvas;
        topLeft = new Vector3(Random.Range(-250, -20), Random.Range(-50, 0), 0);
        bottomRight = new Vector3(Random.Range(250, 20), canvas.groundY, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(drawWall)
        {
            DrawereringTool.drawRectangle(canvas.origin + topLeft * canvas.Zoom, new Vector3(canvas.origin.x + bottomRight.x * canvas.Zoom, canvas.groundY, 0), Color.gray);
            DrawereringTool.fillRectangle(canvas.origin + topLeft * canvas.Zoom, new Vector3(canvas.origin.x + bottomRight.x * canvas.Zoom, canvas.groundY, 0), Color.gray);
        }
    }
}
