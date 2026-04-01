using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TheBuildersJourney.Core; // 引入对 GameManager 的引用

namespace TheBuildersJourney.MapLogic
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private MapGridController gridController;
        [SerializeField] private Vector2Int currentGridPos = new Vector2Int(0, 2);
        
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private float fatigueCostPerStep = 5f;
        [SerializeField] private float satietyCostPerStep = 2f;

        private bool isMoving = false;

        private void Start()
        {
            // 初始化玩家位置
            UpdateWorldPosition();
        }

        public void MoveAlongPath(List<Vector2Int> path)
        {
            if (isMoving || path == null || path.Count == 0) return;
            StartCoroutine(MoveRoutine(path));
        }

        private IEnumerator MoveRoutine(List<Vector2Int> path)
        {
            isMoving = true;

            foreach (var step in path)
            {
                // 跳过起点（如果是自己当前的位置）
                if (step == currentGridPos) continue;

                Vector3 targetWorldPos = new Vector3(step.x, 0.5f, step.y);

                // 转向目标
                Vector3 direction = (targetWorldPos - transform.position).normalized;
                if (direction != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(direction);
                }

                // 移动循环
                while (Vector3.Distance(transform.position, targetWorldPos) > 0.01f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetWorldPos, moveSpeed * Time.deltaTime);
                    yield return null;
                }

                // 到达一个格子，修正位置并更新数据
                transform.position = targetWorldPos;
                currentGridPos = step;

                // 消耗玩家生存属性 (如果有 GameManager)
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.Fatigue += fatigueCostPerStep;
                    GameManager.Instance.Satiety -= satietyCostPerStep;
                    Debug.Log($"[Player] 走了一步。疲劳: {GameManager.Instance.Fatigue}, 饱食: {GameManager.Instance.Satiety}");
                }
                
                yield return new WaitForSeconds(0.1f); // 节点间的短暂亦停顿
            }

            isMoving = false;
            Debug.Log("[Player] 已到达终点！");
            
            // 检查到达终点后是否应该触发什么（比如加载关卡）
            var finalNode = gridController.GetNode(currentGridPos.x, currentGridPos.y);
            if (finalNode != null && finalNode.type == TileType.LevelGate)
            {
                Debug.Log("[Player] 抵达关卡入口！准备进入小游戏...");
                // 此处可以拓展 GameManager.Instance.LoadScene(...) 等逻辑
            }
        }

        private void UpdateWorldPosition()
        {
            // 将网格坐标转换为世界坐标 (Y轴加高一点防止穿模在地块里)
            transform.position = new Vector3(currentGridPos.x, 0.5f, currentGridPos.y);
        }
    }
}