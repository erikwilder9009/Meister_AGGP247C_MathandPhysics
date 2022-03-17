using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManger : MonoBehaviour
{

    public Vector3 origin;
    public Vector3 screenSize;

    // Start is called before the first frame update
    void Start()
    {
        screenSize = new Vector3(Screen.width, Screen.height);
        origin = new Vector3(Screen.width / 2, Screen.height / 2);
    }
}
