using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float speed = 5.0f; // 可以在Unity编辑器中调整速度

    public Color32 myR = new Color32(233, 113, 113, 255); 
    public Color32 myY = new Color32(234, 238, 134, 255); 
    public Color32 myB = new Color32(137, 150, 236, 255);
    public Color[] colors; // 定义颜色数组
    private int currentColorIndex = 0; // 用于追踪当前颜色索引的变量


    // Start is called before the first frame update
    void Start()
    {
        colors = new Color[] { myR, myY, myB };
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        float moveHorizontal = Input.GetAxis("Horizontal"); // 获取水平轴（A和D键或左右箭头）的输入
        float moveVertical = Input.GetAxis("Vertical"); // 获取垂直轴（W和S键或上下箭头）的输入
        Vector2 movement = new Vector2(moveHorizontal, moveVertical); // 创建一个二维向量，基于输入方向
        transform.position += (Vector3)movement * speed * Time.deltaTime; // 移动小球

        // Color Changing
        if (Input.GetKeyDown(KeyCode.Space)) // 检查是否按下空格键
        {
            currentColorIndex = (currentColorIndex + 1) % colors.Length; // 更新颜色索引，循环回数组开始如果超出范围
            GetComponent<SpriteRenderer>().color = colors[currentColorIndex]; // 应用新的颜色
        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("触发器触发了"); // 首先检查这个是否被打印，确保方法被调用

        Background background = other.GetComponent<Background>();
        if (background != null) {
            Debug.Log("当前背景的ID: " + background.backgroundID);
        } else {
            Debug.Log("触发器内没有Background组件");
        }
    }

}