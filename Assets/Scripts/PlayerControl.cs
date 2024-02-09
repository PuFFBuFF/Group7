using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
    public float speed = 2.0f; // 控制速度

    // 预定义颜色
    public Color32 myR = new Color32(233, 113, 113, 255);
    public Color32 myY = new Color32(234, 238, 134, 255);
    public Color32 myB = new Color32(137, 150, 236, 255);
    private Color[] colors; // 存储颜色的数组
    private int currentColorIndex = 0; // 当前颜色索引

    private Rigidbody2D rb; // Rigidbody2D组件
    public Color32 bgColor; // 背景颜色
    public bool canMove = true; // 是否可以移动

    void Start() {
        colors = new Color[] { myR, myY, myB }; // 初始化颜色数组
        rb = GetComponent<Rigidbody2D>(); // 获取Rigidbody2D组件
    }

    void Update() {
        HandleColorChange(); // 处理颜色变化
        HandleMovement(); // 处理移动
    }

    void HandleColorChange() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            // 更新当前颜色索引并循环
            currentColorIndex = (currentColorIndex + 1) % colors.Length;
            // 应用新颜色
            GetComponent<SpriteRenderer>().color = colors[currentColorIndex];

            // 更新canMove状态
            canMove = GetComponent<SpriteRenderer>().color != bgColor;
        }
    }

    void HandleMovement() {
        if (canMove) {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            transform.position += (Vector3)movement * speed * Time.deltaTime;
            rb.simulated = true;
        }

        if(!canMove) {
            if (rb != null) {
                rb.simulated = false; // 禁用物理模拟
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Background background = other.GetComponent<Background>();
        if (background != null) {
            // 当进入新的背景时更新背景颜色
            bgColor = background.bgColor;
            // 基于当前颜色和背景颜色更新canMove状态
            canMove = GetComponent<SpriteRenderer>().color != bgColor;
            HandleMovement(); // 处理移动
        }
    }
}
