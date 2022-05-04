using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    public void Begin()
    {
        SCManager.instance.MStart = true;
        Destroy(gameObject);
    }
}
