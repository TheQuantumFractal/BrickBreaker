using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace BrickBreaker
{
    public class Sprite
    {
        public Texture2D Image;
        public Vector2 position;
        public Color Color;

        public Sprite(Texture2D image, Vector2 Position, Color color)
        {
            Image = image;
            position = Position;
            Color = color;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image, position, Color);
        }

    }
}
