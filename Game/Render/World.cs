using System;
using OpenTK;

namespace Sim.Render
{
    class World
    {
        public readonly int Width, Height;
        public readonly Tile[] Tiles;

        public ref Tile this[int x, int y] => ref Tiles[x + y * Width];

        public World(int width, int height)
        {
            Width = width;
            Height = height;
            Tiles = new Tile[width * height];
        }
    }

    internal static class WorldExtensions
    {
        internal static void Fill(this World world, Tile tile)
        {
            for (var x = 0; x < world.Width; x++)
            {
                for (var y = 0; y < world.Height; y++)
                {
                    world[x, y] = tile;
                }
            }
        }

        internal static Point Clamp(this World world, Vector2 position)
        {
            return new Point(
                Math.Clamp((int)Math.Floor(position.X), 0, world.Width - 1),
                Math.Clamp((int)Math.Floor(position.Y), 0, world.Height - 1)
            );
        }
    }

    internal static class WorldFactory
    {
        internal static World Build()
        {
            var world = new World(10, 10);
            world.Fill(Tile.Grass);
            for (var x = 0; x < world.Width; x++)
            {
                world[x, 0] = Tile.Wall;
            }
            for (var y = 1; y < world.Height; y++)
            {
                world[5, y] = Tile.PathVertical;
            }
            world[5, 0] = Tile.DoorClosed;
            return world;
        }
    }
}