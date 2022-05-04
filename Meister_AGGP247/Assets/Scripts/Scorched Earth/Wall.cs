using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
<<<<<<< Updated upstream
=======
    SCCanvas.Scene canvas;

    [SerializeField]
    public Vector3 topLeft;
    [SerializeField]
    public Vector3 bottomRight;

    public bool drawWall = true;

>>>>>>> Stashed changes
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream
        
=======
        if(drawWall)
        {
            DrawereringTool.drawRectangle(canvas.origin + topLeft * canvas.Zoom, new Vector3(canvas.origin.x + bottomRight.x * canvas.Zoom, canvas.groundY, 0), Color.gray);
            DrawereringTool.fillRectangle(canvas.origin + topLeft * canvas.Zoom, new Vector3(canvas.origin.x + bottomRight.x * canvas.Zoom, canvas.groundY, 0), Color.gray);
        }
>>>>>>> Stashed changes
    }
}
