using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    // 公共变量来存储ID
    public string backgroundID = "defaultID";

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("The ID of this object is: " + backgroundID);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

