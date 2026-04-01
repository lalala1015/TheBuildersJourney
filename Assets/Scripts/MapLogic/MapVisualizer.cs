using UnityEngine;
using System.Collections.Generic;

namespace TheBuildersJourney.MapLogic
{
    public class MapVisualizer : MonoBehaviour
    {
        [SerializeField] private MapGridController gridController;
        
        [Header("Prefabs")]
        [SerializeField] private GameObject roadPrefab;
        [SerializeField] private GameObject emptyPrefab;
        [SerializeField] private GameObject gatePrefab;

        // 记录生成的实体对象，方便后续旋转
        private Dictionary<Vector2Int, GameObject> spawnedTiles = new Dictionary<Vector2Int, GameObject>();

        private void Start()
        {
            GenerateVisuals();
        }

        public void GenerateVisuals()
        {
            if (gridController == null) return;
            
            var grid = gridController.GetRawGrid();
            int width = gridController.Width;
            int height = gridController.Height;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var node = grid[x, y];
                    GameObject prefabToSpawn = emptyPrefab;

                    if (node.type == TileType.Road) prefabToSpawn = roadPrefab;
                    else if (node.type == TileType.LevelGate) prefabToSpawn = gatePrefab;

                    if (prefabToSpawn != null)
                    {
                        // 在 3D 空间系中，我们将 x 映射到 X 轴，y 映射到 Z 轴
                        Vector3 pos = new Vector3(x, 0, y);
                        GameObject go = Instantiate(prefabToSpawn, pos, Quaternion.Euler(0, node.rotation, 0), this.transform);
                        go.name = $"Tile_{x}_{y}_{node.type}";
                        spawnedTiles[new Vector2Int(x, y)] = go;
                    }
                }
            }
        }

        public void SyncTileRotation(Vector2Int pos, int currentRotation)
        {
            if (spawnedTiles.TryGetValue(pos, out GameObject tileObj))
            {
                // 更新真实的 3D 对象旋转
                tileObj.transform.rotation = Quaternion.Euler(0, currentRotation, 0);
            }
        }
    }
}