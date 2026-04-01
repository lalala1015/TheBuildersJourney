using UnityEngine;

namespace TheBuildersJourney.MapLogic
{
    public class MapRotationController : MonoBehaviour
    {
        [SerializeField] private MapGridController gridController;
        [SerializeField] private MapVisualizer mapVisualizer;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private Vector2Int start = new Vector2Int(0, 2);
        [SerializeField] private Vector2Int target = new Vector2Int(4, 2);
        [SerializeField] private Vector2Int selectedTile = new Vector2Int(2, 2);

        private void Update()
        {
            if (gridController == null) return;

            if (Input.GetKeyDown(KeyCode.Q))
            {
                RotateSelected(false);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                RotateSelected(true);
            }
        }

        private void RotateSelected(bool clockwise)
        {
            var node = gridController.GetNode(selectedTile.x, selectedTile.y);
            if (node == null)
            {
                Debug.LogWarning("[MapRotation] Selected tile is null.");
                return;
            }

            // Deduct some Satiety/Fatigue based on survival system if needed in future

            if (clockwise) node.RotateClockwise();
            else node.RotateCounterClockwise();

            // 通知可视化层更新模型旋转
            if (mapVisualizer != null)
            {
                mapVisualizer.SyncTileRotation(selectedTile, node.rotation);
            }

            CheckHiddenBlindSpots(node);

            bool connected = ConnectivityValidator.IsConnected(gridController.GetRawGrid(), start, target, out var path);
            Debug.Log($"[MapRotation] Rotate {(clockwise ? "CW" : "CCW")} -> Connected: {connected}");

            // 如果连通了，而且我们配了角色控制器，就让角色沿着路径走过去
            if (connected && playerController != null)
            {
                Debug.Log($"[MapRotation] Path Found! Steps: {path.Count}");
                playerController.MoveAlongPath(path);
                // 走完之后为了防止反复触发，或者你需要限制只能走一次的话，可以在这里加状态控制
            }
        }

        private void CheckHiddenBlindSpots(TileNode node)
        {
            // Placeholder: When the block rotates, check if certain rotation directions
            // expose a hidden minigame/blind spot (e.g. fishing pond, fruit tree) to the camera.
            // Example:
            // if (node.CurrentRotation == 180 && node.HasHiddenLevel)
            // {
            //      UIManager.ShowHiddenLevelIcon(node);
            //      Debug.Log("A hidden level is revealed!");
            // }
            Debug.Log("[MapRotation] Checked for blind spots / hidden levels.");
        }
    }
}
