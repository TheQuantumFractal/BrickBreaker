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
    public class Brick : Sprite
    {
        public Rectangle Hitbox;
        public Brick(Texture2D image, Vector2 position):
            base(image, position, Color.White)
        {
            Hitbox = new Rectangle((int)position.X, (int)position.Y, Image.Width, Image.Height);
        }

        public void Gen(Random r)
        {
            Color = new Color(r.Next(1, 256), r.Next(1, 256), r.Next(1, 256));
        }

    }
}
