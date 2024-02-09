using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    // 公共变量来存储ID
    public string backgroundID = "defaultID";
    public Color32 bgColor = new Color32(233, 113, 113, 255);

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("The ID of this object is: " + backgroundID);
        Debug.Log("The Color of this object is: " + bgColor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

