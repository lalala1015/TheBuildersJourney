using UnityEngine;

namespace TheBuildersJourney.MapLogic
{
    public class MapGridController : MonoBehaviour
    {
        [SerializeField] private int width = 5;
        [SerializeField] private int height = 5;

        private TileNode[,] grid;

        public int Width => width;
        public int Height => height;

        private void Awake()
        {
            BuildTestGrid();
        }

        public void BuildTestGrid()
        {
            grid = new TileNode[width, height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    grid[x, y] = new TileNode(x, y, TileType.Empty, DirectionMask.None);
                }
            }

            // 涓€鏉″彲鏃嬭浆娴嬭瘯璺?
            grid[0, 2] = new TileNode(0, 2, TileType.Road, DirectionMask.Right);
            grid[1, 2] = new TileNode(1, 2, TileType.Road, DirectionMask.Left | DirectionMask.Right);
            // 鍒濆璁剧疆涓轰笂涓嬭繛閫氾紙鏂紑宸﹀彸杩為€氱姸鎬侊級锛岃繖鏍锋墠鑳芥祴璇?鏃嬭浆鍚庤繛閫?
            grid[2, 2] = new TileNode(2, 2, TileType.Road, DirectionMask.Up | DirectionMask.Down);
            grid[3, 2] = new TileNode(3, 2, TileType.Road, DirectionMask.Left | DirectionMask.Right);
            grid[4, 2] = new TileNode(4, 2, TileType.LevelGate, DirectionMask.Left);
        }

        public TileNode GetNode(int x, int y)
        {
            if (x < 0 || y < 0 || x >= width || y >= height) return null;
            return grid[x, y];
        }

        public TileNode[,] GetRawGrid() => grid;
    }
}
