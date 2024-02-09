using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour {
    public float speed = 5.0f; // 移动速度
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
        if (other.transform == target) {
            // 如果鬼追到了玩家，销毁鬼对象
            Debug.Log("Ghost caught the player, ghost disappears.");
            Destroy(gameObject); // 销毁鬼对象
        }
    }
}