﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace XnaUtility
{
    public static class UtilityMethods
    {
        public static Game CurrentGame = null;
        public static Vector2 TexturesCollide(Color[,] tex1, Matrix mat1, Color[,] tex2, Matrix mat2)
        {
            int width1 = tex1.GetLength(0);
            int height1 = tex1.GetLength(1);
            int width2 = tex2.GetLength(0);
            int height2 = tex2.GetLength(1);
            Matrix mat1to2 = mat1 * Matrix.Invert(mat2);
            for (int x1 = 0; x1 < width1; x1++)
            {
                for (int y1 = 0; y1 < height1; y1++)
                {
                    Vector2 pos1 = new Vector2(x1, y1);
                    Vector2 pos2 = Vector2.Transform(pos1, mat1to2);
                    int x2 = (int)pos2.X;
                    int y2 = (int)pos2.Y;
                    if ((x2 < 0) || (x2 >= width2)) continue;
                    if ((y2 < 0) || (y2 >= height2)) continue;
                    if (tex1[x1, y1].A <= 0) continue;
                    if (tex2[x2, y2].A <= 0) continue;
                    Vector2 screenPos = Vector2.Transform(pos1, mat1);
                    return screenPos;
                }
            }
            return new Vector2(-1, -1);
        }

        public static Matrix Get2DTransformationMatrix(Vector2? origin, float? rotation, float? scale, Vector2? translation)
        {
            var _origin = (Vector2)(origin ?? new Vector2(0, 0));
            var _rotation = (float)(rotation ?? 0);
            var _scale = (float)(scale ?? 1);
            var _translation = (Vector2)(translation ?? new Vector2(0, 0));
            return Matrix.CreateTranslation(_origin.X, _origin.Y, 0) * Matrix.CreateRotationZ(_rotation) * Matrix.CreateScale(_scale) * Matrix.CreateTranslation(_translation.X, _translation.Y, 0);
        }

        public static Color[,] TextureTo2DArray(Texture2D texture)
        {
            Color[] colors1D = new Color[texture.Width * texture.Height];
            texture.GetData(colors1D);

            Color[,] colors2D = new Color[texture.Width, texture.Height];
            for (int x = 0; x < texture.Width; x++)
                for (int y = 0; y < texture.Height; y++)
                    colors2D[x, y] = colors1D[x + y * texture.Width];

            return colors2D;
        }

        public static bool ClickPress(this KeyboardState state, KeyboardState previousState, Keys key)
        {
            return state.IsKeyDown(key) &&
                   previousState.IsKeyUp(key);
        }
    }
}
