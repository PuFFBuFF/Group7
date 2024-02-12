using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostMovement : MonoBehaviour {
    public float speed = 2.0f; // 移动速度
    private Vector2 targetPosition;

    public Vector2 minPosition = new Vector2(-5, -5); // 移动范围的最小坐标
    public Vector2 maxPosition = new Vector2(5, 5); // 移动范围的最大坐标

    public Transform target; // 目标对象的Transform组件
    public float chaseDistance = 2.5f; // 追踪的距离阈值
    private bool isChasing = false; // 是否正在追踪

    // 引用目标对象的脚本以访问canMove属性
    private NewBehaviourScript targetScript;

    private void Start() {
        // 初始化随机目标位置
        targetPosition = GetRandomPosition();
        // 获取目标对象的NewBehaviourScript组件
        targetScript = target.GetComponent<NewBehaviourScript>();

    }

    private void Update() {
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        // 只有当单色
        Color32 playerColor = targetScript.curColor;
        Color32 backgroundColor = targetScript.bgColor;
        if(playerColor.Equals(backgroundColor)) {
            MoveToTarget();
        } else {
            if (isChasing && distanceToTarget < chaseDistance) {
                ChaseTarget();
            } else {
                if (!isChasing || distanceToTarget >= chaseDistance) {
                    MoveToTarget();
                    if ((Vector2)transform.position == targetPosition) {
                        targetPosition = GetRandomPosition();
                    }
                }
            }

            if (!isChasing && distanceToTarget < chaseDistance) {
                isChasing = true;
            } else if (isChasing && distanceToTarget >= chaseDistance) {
                isChasing = false;
                targetPosition = GetRandomPosition();
            }
        }
    }

    private void ChaseTarget() {
        Vector2 direction = (target.position - transform.position).normalized;
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    private void MoveToTarget() {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if ((Vector2)transform.position == targetPosition) {
            targetPosition = GetRandomPosition();
        }
    }

    private Vector2 GetRandomPosition() {
        float randomX = Random.Range(minPosition.x, maxPosition.x);
        float randomY = Random.Range(minPosition.y, maxPosition.y);
        return new Vector2(randomX, randomY);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("OnTriggerEnter2D");
        // 检查触发器碰撞的对象是否为玩家
        if (other.gameObject == target.gameObject) {
            // 获取玩家的当前颜色和背景颜色
            Color32 playerColor = targetScript.curColor; // 假设NewBehaviourScript有一个公共属性或方法curColor
            Color32 backgroundColor = targetScript.bgColor; // 同样假设有公共访问方式

            Debug.Log("playerColor = " + playerColor);
            Debug.Log("backgroundColor = " + backgroundColor);

            // 对比玩家颜色和背景颜色
            if (playerColor.Equals(backgroundColor)) {
                Debug.Log("Player's color matches the background color. Ghost disappear.");
                // 如果颜色相同，执行相应的逻辑
                //Destroy(gameObject);
            } else {
                // 如果颜色不同，执行原有逻辑
                Debug.Log("Ghost caught the player.");
                Debug.Log("bgcolor = " + targetScript.bgColor);
                Debug.Log("curColor = " + targetScript.curColor);
                Destroy(other.gameObject);
                GameOver();
            }
        }
    }

    // 游戏结束逻辑
    private void GameOver() {
        Debug.Log("Game Over! Restarting game or showing game over screen...");
        // 加载一个游戏结束场景
        SceneManager.LoadScene("GameOverScene");
    }
}