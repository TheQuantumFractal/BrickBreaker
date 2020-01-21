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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D texture;
        Vector2 position;
        Color color;
        Vector2 speed;
        Texture2D padT;
        Vector2 padPos;
        Color padColor;

        Sprite spritebat;
        Sprite spriteba;
        Rectangle padRect;
        Rectangle rect;
        

        List<Brick> bricks;
        List<lives> life;


        SpriteFont font;
        string text;
        Vector2 textposition;
        Color Textcolor;
        bool play;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            bricks = new List<Brick>();
            life = new List<lives>();
            base.Initialize();
        }
        Random random;

        int var = 1;
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = Content.Load<Texture2D>("ball");
            position = new Vector2(GraphicsDevice.Viewport.Width/2, (GraphicsDevice.Viewport.Height/2) + 100);
            color = Color.White;
            speed = new Vector2(6, 6);
            padColor = Color.White;
            padT = Content.Load<Texture2D>("paddle");
            padPos = new Vector2(GraphicsDevice.Viewport.Width / 2 - 120, GraphicsDevice.Viewport.Height - 52);
            spritebat = new Sprite(texture, position, color);
            spriteba = new Sprite(padT, padPos, padColor);
            random = new Random();
            play = true;
            int y = 0;
            spritebat.position = new Vector2(GraphicsDevice.Viewport.Width / 2 + 15, (GraphicsDevice.Viewport.Height / 2) + 100);
            int x;
            for (int z = 0; z < (int)Math.Ceiling((decimal)(11 / var)); z++)
            {
                x = 0;
                for (int i = 0; i < 11; i++)
                {
                    Brick brick = new Brick(Content.Load<Texture2D>("brick"), new Vector2(x, y));
                    brick.Gen(random);
                    bricks.Add(brick);
                    x += brick.Image.Width;
                    if (i == 10)
                    {
                        y += brick.Image.Height;
                    }
                }
            }
            int a = 0;
            for (int f = 0; f < 3; f++)
            {
                lives lifes = new lives(Content.Load<Texture2D>("life"), new Vector2(a, 2));
                life.Add(lifes);
                a += lifes.Image.Width + 2;
            }
            font = Content.Load<SpriteFont>("gamefont");
            text = "";
            Textcolor = Color.Yellow;
            textposition = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        protected override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Space))
            {
            }
            if (play)
            {
                padRect = new Rectangle((int)spriteba.position.X, (int)spriteba.position.Y, padT.Width, padT.Height);
                rect = new Rectangle((int)spritebat.position.X, (int)spritebat.position.Y, texture.Width, texture.Height);
                // Allows the game to exit

                // TODO: Add your update logic here

                if (spritebat.position.X > GraphicsDevice.Viewport.Width - texture.Width || spritebat.position.X < 0)
                {
                    speed.X *= -1;
                }
                if (spritebat.position.Y < 0)
                {
                    speed.Y = Math.Abs(speed.Y);
                }
                MouseState mouse = Mouse.GetState();
                if (mouse.X < GraphicsDevice.Viewport.Width - 240 && mouse.X > 0)
                {
                    spriteba.position.X = mouse.X;
                }
                if (rect.Intersects(padRect))
                {
                    speed.Y = (Math.Abs(speed.Y) * -1);
                    spritebat.position.Y -= 15;
                }
                int y = 0;
                if (spritebat.position.Y > GraphicsDevice.Viewport.Height)
                {
                    spritebat.position = new Vector2(GraphicsDevice.Viewport.Width / 2, (GraphicsDevice.Viewport.Height / 2) + 100);
                    if (life.Count > 0)
                    {
                        life.RemoveAt(life.Count - 1);
                    }
                    if (life.Count == 0)
                    {
                        int a = 0;
                        for (int f = 0; f < 3; f++)
                        {
                            lives lifes = new lives(Content.Load<Texture2D>("life"), new Vector2(a, 2));
                            life.Add(lifes);
                            a += lifes.Image.Width + 2;
                        }
                        bricks.RemoveRange(0, bricks.Count);
                        for (int z = 0; z < (int)Math.Ceiling((decimal)(11 / var)); z++)
                        {
                            int x = 0;
                            for (int i = 0; i < 11; i++)
                            {
                                Brick brick = new Brick(Content.Load<Texture2D>("brick"), new Vector2(x, y));
                                brick.Gen(random);
                                bricks.Add(brick);
                                x += brick.Image.Width;
                                if (i == 10)
                                {
                                    y += var * brick.Image.Height;
                                }
                            }
                        }
                    }
                }
                if (bricks.Count == 0)
                {
                    text = "YOU WIN! \r\n Press Any Key to Continue";
                    play = false;
                }
                spritebat.position.X += speed.X;
                spritebat.position.Y += speed.Y;


                for (int i = 0; i < bricks.Count; i++)
                {
                    Brick brick = bricks[i];


                    if (bricks[i].Hitbox.Intersects(rect))
                    {
                        bricks.RemoveAt(i);
                        i--;
                        speed.Y *= -1;
                        break;
                    }
                }
            }

            else
            {
                if (ks.GetPressedKeys().Length > 0)
                {
                    play = true;
                    int x;
                    int y = 0;
                    for (int z = 0; z < 6; z++)
                    {
                        x = 0;
                        for (int i = 0; i < 11; i++)
                        {
                            Brick brick = new Brick(Content.Load<Texture2D>("brick"), new Vector2(x, y));
                            brick.Gen(random);
                            bricks.Add(brick);
                            x +=  brick.Image.Width;
                            if (i == 5)
                            {
                                y += 2 * brick.Image.Height;
                            }
                        }
                    }
                    spritebat.position = new Vector2(GraphicsDevice.Viewport.Width / 2 + 15, (GraphicsDevice.Viewport.Height / 2) + 100);
                    text = "";
                    speed.X = 8;
                    speed.Y = 8;
                    var = 2;
                }
            }
            base.Update(gameTime);
            
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.DrawString(font, text, textposition, Textcolor);
            spritebat.Draw(spriteBatch);
            spriteba.Draw(spriteBatch);

            //for(int i = 0; i < bricks.Count; i++)
            //{
            //    Brick brick = bricks[i];
            //}

            foreach(Brick brick in bricks)
            {
                brick.Draw(spriteBatch);
            }
            foreach(lives lifes in life)
            {
                lifes.Draw(spriteBatch);
            }

            spriteBatch.End();

        }
    }
}
