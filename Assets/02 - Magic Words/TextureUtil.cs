using UnityEngine;

namespace _02___Magic_Words
{
    public static class TextureUtil
    {
        public static Texture2D AddTexture(this Texture2D mainTex, Texture2D tex, Vector2Int lastCoordinate,
            Vector2Int size, out Vector2Int coord)
        {
            coord = lastCoordinate;

            mainTex = ResizeTexture(mainTex, size, ref coord);

            var position = new Vector2Int(coord.x * size.x, coord.y * size.y);

            for (var x = 0; x < tex.width; x++)
            {
                for (var y = 0; y < tex.height; y++)
                {
                    // Calculate the target position on the target texture
                    var targetX = position.x + x;
                    var targetY = position.y + y;

                    // Get the color from the texture to place and set it on the target texture
                    var color = tex.GetPixel(x, y);
                    mainTex.SetPixel(targetX, targetY, color);
                }
            }

            mainTex.Apply();

            return mainTex;
        }

        static Texture2D ResizeTexture(Texture2D mainTex, Vector2Int size, ref Vector2Int coord)
        {
            //Texture is empty
            if (mainTex.height == 0 || mainTex.width == 0)
            {
                coord = Vector2Int.zero;
                mainTex = new Texture2D(size.x, size.y);
            }
            //Texture not empty, find next empty spot
            else
            {
                //Texture is filled, enlarge it
                if (coord.x == 0)
                {
                    coord.x = coord.y + 1;
                    coord.y = 0;

                    var newMainTex = new Texture2D((coord.x + 1) * size.x, (coord.x + 1) * size.y, TextureFormat.ARGB32,
                        false);
                    newMainTex.SetPixels(0, 0, mainTex.width, mainTex.height, mainTex.GetPixels());
                    mainTex = newMainTex;
                }
                //Going up
                else if (coord.x > coord.y)
                {
                    coord.y++;
                }
                //Going left
                else
                {
                    coord.x--;
                }
            }

            return mainTex;
        }
    }
}