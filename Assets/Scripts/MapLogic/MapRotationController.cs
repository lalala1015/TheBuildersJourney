using UnityEngine;

namespace TheBuildersJourney.MapLogic
{
    public class MapRotationController : MonoBehaviour
    {
        [SerializeField] private MapGridController gridController;
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

            if (clockwise) node.RotateClockwise();
            else node.RotateCounterClockwise();

            bool connected = ConnectivityValidator.IsConnected(gridController.GetRawGrid(), start, target);
            Debug.Log($"[MapRotation] Rotate {(clockwise ? "CW" : "CCW")} -> Connected: {connected}");
        }
    }
}
