using System;

namespace TheBuildersJourney.MapLogic
{
    [Flags]
    public enum DirectionMask
    {
        None = 0,
        Up = 1 << 0,
        Right = 1 << 1,
        Down = 1 << 2,
        Left = 1 << 3
    }

    public enum TileType
    {
        Empty,
        Road,
        Block,
        LevelGate
    }

    [Serializable]
    public class TileNode
    {
        public int x;
        public int y;
        public TileType type;
        public int rotation; // 0, 90, 180, 270
        public DirectionMask mask;

        public bool IsWalkable => type == TileType.Road || type == TileType.LevelGate;

        public TileNode(int x, int y, TileType type, DirectionMask mask)
        {
            this.x = x;
            this.y = y;
            this.type = type;
            this.mask = mask;
            rotation = 0;
        }

        public void RotateClockwise()
        {
            rotation = (rotation + 90) % 360;
            mask = RotateMaskClockwise(mask);
        }

        public void RotateCounterClockwise()
        {
            rotation = (rotation + 270) % 360;
            mask = RotateMaskCounterClockwise(mask);
        }

        private static DirectionMask RotateMaskClockwise(DirectionMask m)
        {
            DirectionMask r = DirectionMask.None;
            if (m.HasFlag(DirectionMask.Up)) r |= DirectionMask.Right;
            if (m.HasFlag(DirectionMask.Right)) r |= DirectionMask.Down;
            if (m.HasFlag(DirectionMask.Down)) r |= DirectionMask.Left;
            if (m.HasFlag(DirectionMask.Left)) r |= DirectionMask.Up;
            return r;
        }

        private static DirectionMask RotateMaskCounterClockwise(DirectionMask m)
        {
            DirectionMask r = DirectionMask.None;
            if (m.HasFlag(DirectionMask.Up)) r |= DirectionMask.Left;
            if (m.HasFlag(DirectionMask.Left)) r |= DirectionMask.Down;
            if (m.HasFlag(DirectionMask.Down)) r |= DirectionMask.Right;
            if (m.HasFlag(DirectionMask.Right)) r |= DirectionMask.Up;
            return r;
        }
    }
}
