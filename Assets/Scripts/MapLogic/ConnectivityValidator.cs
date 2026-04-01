using System.Collections.Generic;
using UnityEngine;

namespace TheBuildersJourney.MapLogic
{
    public static class ConnectivityValidator
    {
        private static readonly Vector2Int[] Directions =
        {
            new Vector2Int(0, 1),
            new Vector2Int(1, 0),
            new Vector2Int(0, -1),
            new Vector2Int(-1, 0)
        };

        public static bool IsConnected(TileNode[,] grid, Vector2Int start, Vector2Int target, out List<Vector2Int> path)
        {
            path = new List<Vector2Int>();
            int width = grid.GetLength(0);
            int height = grid.GetLength(1);

            bool[,] visited = new bool[width, height];
            // 记录父节点以便回溯路径
            Dictionary<Vector2Int, Vector2Int> parentMap = new Dictionary<Vector2Int, Vector2Int>();
            Queue<Vector2Int> queue = new Queue<Vector2Int>();

            queue.Enqueue(start);
            visited[start.x, start.y] = true;

            bool found = false;

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                
                if (current == target) 
                {
                    found = true;
                    break;
                }

                var currentNode = grid[current.x, current.y];
                if (currentNode == null || !currentNode.IsWalkable) continue;

                for (int i = 0; i < Directions.Length; i++)
                {
                    var next = current + Directions[i];
                    if (next.x < 0 || next.y < 0 || next.x >= width || next.y >= height) continue;
                    if (visited[next.x, next.y]) continue;

                    var nextNode = grid[next.x, next.y];
                    if (nextNode == null || !nextNode.IsWalkable) continue;

                    if (CanPass(currentNode, nextNode, i))
                    {
                        visited[next.x, next.y] = true;
                        parentMap[next] = current;
                        queue.Enqueue(next);
                    }
                }
            }

            if (found)
            {
                // 回溯构建路径
                Vector2Int step = target;
                while (step != start)
                {
                    path.Add(step);
                    step = parentMap[step];
                }
                path.Add(start);
                path.Reverse(); // 将目标回溯的过程倒转为起点到终点的顺序
                return true;
            }

            return false;
        }

        private static bool CanPass(TileNode from, TileNode to, int dirIndex)
        {
            return dirIndex switch
            {
                0 => from.mask.HasFlag(DirectionMask.Up) && to.mask.HasFlag(DirectionMask.Down),
                1 => from.mask.HasFlag(DirectionMask.Right) && to.mask.HasFlag(DirectionMask.Left),
                2 => from.mask.HasFlag(DirectionMask.Down) && to.mask.HasFlag(DirectionMask.Up),
                3 => from.mask.HasFlag(DirectionMask.Left) && to.mask.HasFlag(DirectionMask.Right),
                _ => false
            };
        }
    }
}
