using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCCanvas : MonoBehaviour
{
    public class Scene
    {
        public Vector3 screenSize;
        public Vector3 origin;
        public float groundY;

        public float Zoom = 1f;
        public float minZoom = .1f;
        public float maxZoom = 2f;
    }
    public bool isDrawingOrigin = false;
    public Scene canvas = new Scene();

    public bool run;

    // Start is called before the first frame update
    void Start()
    {
        run = false;
        canvas.screenSize = new Vector3(Screen.width, Screen.height);
        canvas.origin = new Vector3(Screen.width / 2, Screen.height / 2);
        run = true;
    }

    // Update is called once per frame
    void Update()
    {
        getInputs();
        if (isDrawingOrigin)
        {
            DrawereringTool.drawOrigin(canvas.origin, 2, Color.red);
        }

        drawScene();
    }

    void drawScene()
    {
        Vector3 frameA = new Vector3(canvas.origin.x - (canvas.screenSize.x / 2) * canvas.Zoom, canvas.origin.y - (canvas.screenSize.y / 2) * canvas.Zoom, 0);
        Vector3 frameB = new Vector3(canvas.origin.x + (canvas.screenSize.x / 2) * canvas.Zoom, canvas.origin.y + (canvas.screenSize.y / 2) * canvas.Zoom, 0);
        DrawereringTool.drawRectangle(frameA, frameB, Color.yellow);

        Vector3 groundA = new Vector3((canvas.origin.x - (canvas.screenSize.x / 2) * canvas.Zoom) + 1, (canvas.origin.y - (canvas.screenSize.y / 2) * canvas.Zoom) + 1, 0);
        Vector3 groundB = new Vector3((canvas.origin.x + (canvas.screenSize.x / 2) * canvas.Zoom) - 1, (canvas.origin.y + ((canvas.screenSize.y / 2) - canvas.screenSize.y * .9f) * canvas.Zoom) - 1, 0);
        canvas.groundY = groundB.y;
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


