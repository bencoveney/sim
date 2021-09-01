namespace sim.Render
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
    }
}