using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public float speed = 5.0f; // 移动速度
    private Vector2 targetPosition;

    public Vector2 minPosition = new Vector2(-5, -5); // 移动范围的最小坐标
    public Vector2 maxPosition = new Vector2(5, 5); // 移动范围的最大坐标


    private void Start() {
        // 初始化随机目标位置
        targetPosition = GetRandomPosition();
    }

    private void Update() {
        MoveToTarget();
    }

    private void MoveToTarget() {
        // 向目标位置移动
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // 如果物体到达目标位置，则选择新的随机目标位置
        if ((Vector2)transform.position == targetPosition) {
            targetPosition = GetRandomPosition();
        }
    }

    private Vector2 GetRandomPosition() {
        //// 获取屏幕的宽度和高度
        //float screenX = Screen.width;
        //float screenY = Screen.height;
        //// 将屏幕坐标转换为世界坐标
        //Vector2 screenPosition = new Vector2(Random.Range(0, screenX), Random.Range(0, screenY));
        //Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        //return worldPosition;



        // 在指定的范围内生成随机位置
        float randomX = Random.Range(minPosition.x, maxPosition.x);
        float randomY = Random.Range(minPosition.y, maxPosition.y);
        return new Vector2(randomX, randomY);
    }
}
