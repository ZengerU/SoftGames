using UnityEngine;

namespace MagicWords
{
    public static class TextureUtil
    {
        public static Texture2D AddTexture(this Texture2D mainTex, Texture2D tex, Vector2Int lastCoordinate,
            Vector2Int size, out Vector2Int coord)
        {
            coord = lastCoordinate;
            if (lastCoordinate.x < 0 || lastCoordinate.y < 0)
            {
                coord = Vector2Int.zero;
            }
            else if (coord.x == 0)
            {
                coord.x = coord.y + 1;
                coord.y = 0;
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
    }
}