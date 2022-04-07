using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curser : MonoBehaviour
{
    Vector3 Location;
    SCCanvas.Scene canvas;
    public bool Draw;

    // Start is called before the first frame update
    void Start()
    {
        canvas = SCCanvas.instance.canvas;
        Location = SCCanvas.instance.canvas.origin;
    }

    // Update is called once per frame
    void Update()
    {
        if(Draw)
        {
            drawCurser(Location);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Location = Input.mousePosition;
            Debug.Log("Curser at : " + (Location - canvas.origin));
        }
    }

    public void drawCurser(Vector3 location)
    {
        Glint.AddCommand(new Line(new Vector3(location.x + (1 * canvas.Zoom), location.y, 0), new Vector3(location.x + (5 * canvas.Zoom), location.y, 0), Color.cyan));
        Glint.AddCommand(new Line(new Vector3(location.x, location.y + (1 * canvas.Zoom), 0), new Vector3(location.x, location.y + (5 * canvas.Zoom), 0), Color.cyan));
        Glint.AddCommand(new Line(new Vector3(location.x - (1 * canvas.Zoom), location.y, 0), new Vector3(location.x - (5 * canvas.Zoom), location.y, 0), Color.cyan));
        Glint.AddCommand(new Line(new Vector3(location.x, location.y - (1 * canvas.Zoom), 0), new Vector3(location.x, location.y - (5 * canvas.Zoom), 0), Color.cyan));
    }
}
