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
}