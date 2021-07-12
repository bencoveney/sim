using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using Sim.Utils;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace sim.Render
{
    class Texture
    {
        public Handle handle;

        public Texture()
        {
            handle = new Handle(GL.GenTexture());

            // Bind the handle
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, handle);

            var bytes = Resource.ReadBytes("town_tiles.png", Resource.Kind.Texture);

            // Load the image
            Image<Rgba32> image = Image.Load<Rgba32>(bytes);

            // ImageSharp loads from the top-left pixel, whereas OpenGL loads from the bottom-left, causing the texture to be flipped vertically.
            // This will correct that, making the texture display properly.
            image.Mutate(x => x.Flip(FlipMode.Vertical));

            // Convert ImageSharp's format into a byte array, so we can use it with OpenGL.
            var pixels = new List<byte>(4 * image.Width * image.Height);

            for (int y = 0; y < image.Height; y++)
            {
                var row = image.GetPixelRowSpan(y);
                for (int x = 0; x < image.Width; x++)
                {
                    pixels.Add(row[x].R);
                    pixels.Add(row[x].G);
                    pixels.Add(row[x].B);
                    pixels.Add(row[x].A);
                }
            }

            GL.TexImage2D(
                TextureTarget.Texture2D,
                0,
                PixelInternalFormat.Rgba,
                image.Width,
                image.Height,
                0,
                PixelFormat.Rgba,
                PixelType.UnsignedByte,
                pixels.ToArray()
            );

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }
    }
}