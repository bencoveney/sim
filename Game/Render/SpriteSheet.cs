using OpenTK;

namespace Sim.Render
{
    class SpriteSheet
    {
        public Texture Texture;
        public uint TileSize;
        public uint Rows;
        public uint Columns;
        public SpriteSheet(Texture texture, uint tileSize)
        {
            Texture = texture;
            TileSize = tileSize;
            Rows = (uint)texture.Height / tileSize;
            Columns = (uint)texture.Width / tileSize;
        }

        public Vector2 GetOffsets(uint row, uint column)
        {
            return new Vector2(column * TileSize, row * TileSize);
        }

        public Vector2 GetOffsets(Tile index)
        {
            return GetOffsets((uint)index);
        }

        public Vector2 GetOffsets(uint index)
        {
            var row = index / Columns;
            var column = index - (row * Columns);
            var offsets = GetOffsets(row, column);
            return offsets;
        }
    }
}