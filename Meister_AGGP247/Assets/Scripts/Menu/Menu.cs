using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Vector3 origin;
    public Vector3 screenSize;
    Vector3 mousePoint;
    // Start is called before the first frame update
    void Start()
    {
        screenSize = new Vector3(Screen.width, Screen.height);
        origin = new Vector3(Screen.width / 2, Screen.height / 2);
    }
    // Update is called once per frame
    void Update()
    {
        DrawShipButton();
        DrawCannonButton();
        mousePoint = Input.mousePosition;
    }
    void DrawShipButton()
    {
        Vector3 A = new Vector3(origin.x - 200, origin.y + 120, 0);
        Vector3 B = new Vector3(origin.x + 200, origin.y + 120, 0);
        Vector3 C = new Vector3(origin.x + 200, origin.y + 20);
        Vector3 D = new Vector3(origin.x - 200, origin.y + 20);
        DrawereringTool.drawRectangle(A, B, C, D, Color.yellow);
        if (MathTool.PointInRectangle(mousePoint, A, B, C, D))
        {
            DrawereringTool.fillRectangle(A, B, C, D, Color.yellow);
            if(Input.GetMouseButtonDown(0))
            {
                LoadShipGame();
            }
        }
    }
    void DrawCannonButton()
    {
        Vector3 A = new Vector3(origin.x + 200, origin.y -20);
        Vector3 B = new Vector3(origin.x - 200, origin.y -20);
        Vector3 C = new Vector3(origin.x - 200, origin.y - 120);
        Vector3 D = new Vector3(origin.x + 200, origin.y - 120);
        DrawereringTool.drawRectangle(A, B, C, D, Color.green);
        if (MathTool.PointInRectangle(mousePoint, A, B, C, D))
        {
            DrawereringTool.fillRectangle(A, B, C, D, Color.green); 
            if (Input.GetMouseButtonDown(0))
            {
                LoadCannonGame();
            }
        }
    }
    public static void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
    public static void LoadShipGame()
    {
        SceneManager.LoadScene(1);
    }
    public static void LoadCannonGame()
    {
        SceneManager.LoadScene(2);
    }
}
