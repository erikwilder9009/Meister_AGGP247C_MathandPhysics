using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCCanvas : MonoBehaviour
{
    public class Canvas
    {
        public Vector3 screenSize;
        public Vector3 origin;

        public float Zoom = 1f;
        public float minZoom = .1f;
        public float maxZoom = 2f;
    }
    public bool isDrawingOrigin = false;
    public Canvas canvas = new Canvas();

    // Start is called before the first frame update
    void Start()
    {
        canvas.screenSize = new Vector3(Screen.width, Screen.height);
        canvas.origin = new Vector3(Screen.width / 2, Screen.height / 2);
    }

    // Update is called once per frame
    void Update()
    {
        getInputs();
        if (isDrawingOrigin)
        {
            DrawereringTool.drawOrigin(canvas.origin, 20f, Color.red);
        }
        drawScene();
    }

    void drawScene()
    {
        Vector3 frameA = new Vector3(canvas.origin.x - (canvas.screenSize.x / 2) * canvas.Zoom, canvas.origin.y - (canvas.screenSize.y / 2) * canvas.Zoom, 0);
        Vector3 frameB = new Vector3(canvas.origin.x + (canvas.screenSize.x / 2) * canvas.Zoom, canvas.origin.y + (canvas.screenSize.y / 2) * canvas.Zoom, 0);
        DrawereringTool.drawRectangle(frameA, frameB, Color.yellow);

        Vector3 groundA = new Vector3((canvas.origin.x - (canvas.screenSize.x / 2) * canvas.Zoom) + 1, (canvas.origin.y - (canvas.screenSize.y / 2) * canvas.Zoom) + 1, 0);
        Vector3 groundB = new Vector3((canvas.origin.x + (canvas.screenSize.x / 2) * canvas.Zoom) - 1, (canvas.origin.y + ((canvas.screenSize.y / 2) - canvas.screenSize.y * .75f) * canvas.Zoom) - 1, 0);
        DrawereringTool.drawRectangle(groundA, groundB, Color.green);
        DrawereringTool.fillRectangle(groundB, groundA, Color.green);
    }

    public void getInputs()
    {
        canvas.Zoom += (Input.mouseScrollDelta.y * .1f);

        if (Input.GetMouseButton(2))
        {
            canvas.origin = Input.mousePosition;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            isDrawingOrigin = !isDrawingOrigin;
        }
    }
}


