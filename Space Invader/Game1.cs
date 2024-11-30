using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Space_Invader
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Ship ship;
        List<Alien> aliens = new List<Alien>();

        List<Shield> shields = new List<Shield>();

        Texture2D alienImage;
        Texture2D bulletImage;


        Texture2D shield1;
        Texture2D shield2;
        Texture2D shield3;
        Texture2D shield4;
        bool game = true;
        Random rand = new Random();
        TimeSpan bulletUpdateTime;
        TimeSpan bulletElapsedTime;

        TimeSpan aliensUpdateTime;
        TimeSpan aliensElapsedTime;
        Vector2 shieldpos = new Vector2(60, 350);

        bool l = false;
        bool w = false;

        SpriteFont font;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;

            graphics.PreferredBackBufferWidth = 809;
            graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            shield1 = Content.Load<Texture2D>("shield1");
            shield2 = Content.Load<Texture2D>("shield2");
            shield3 = Content.Load<Texture2D>("shield3");
            shield4 = Content.Load<Texture2D>("shield4");

            // TODO: use this.Content to load your game content here

            bulletImage = Content.Load<Texture2D>("bullet2");
            bulletUpdateTime = TimeSpan.FromMilliseconds(250);
            bulletElapsedTime = TimeSpan.Zero;

            aliensElapsedTime = TimeSpan.Zero;
            aliensUpdateTime = TimeSpan.FromMilliseconds(500);

            font = Content.Load<SpriteFont>("spritefont");

            alienImage = Content.Load<Texture2D>("aliens");

            int x = 0;
            int y = 0;
            for (int i = 0; i < 3; i++)
            {
                x = 1;
                for (int j = 0; j <10; j++)
                {
                    aliens.Add(new Alien(new Vector2(10, 10), alienImage, new Vector2(x, y), 0.4f, Color.White));
                    x += (int)(alienImage.Width * 0.4f) + 22;

                }
                y += (int)(alienImage.Height * 0.4f) + 5;
            }

            ship = new Ship(new Vector2(5, 5), Content.Load<Texture2D>("shipimage"), Vector2.Zero, 0.1f, Color.White, bulletImage);
            ship.Y = GraphicsDevice.Viewport.Height - ship.hitbox.Height;
            int num = 0;

            for (int i = 0; i < 4; i++)
            {
                shields.Add(new Shield(shield1, new Vector2(shieldpos.X + 200 * num, shieldpos.Y), Color.White, 0.4f, 10));
                num++;
            }

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 
        KeyboardState lastKs;
        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            ship.Update(GraphicsDevice.Viewport, Keyboard.GetState(), aliens, lastKs, game);
            // TODO: Add your update logic here


            KeyboardState ks = Keyboard.GetState();

            bulletElapsedTime += gameTime.ElapsedGameTime;

            if (bulletElapsedTime >= bulletUpdateTime)
            {

                for (int i = 0; i < aliens.Count; i++)
                {
                    aliens[i].BulletMove();
                }
                bulletElapsedTime = TimeSpan.Zero;
            }





            if (game)
            {
                aliensElapsedTime += gameTime.ElapsedGameTime;
                if (aliensElapsedTime >= aliensUpdateTime)
                {
                    aliensElapsedTime = TimeSpan.Zero;

                    Alien alienToShootFrom = aliens[rand.Next(0, aliens.Count)];

                    alienToShootFrom.alienBullets.Add(new Bullet(new Vector2(3, 20), bulletImage, new Vector2(alienToShootFrom.position.X + alienToShootFrom.hitbox.Width / 2, alienToShootFrom.position.Y + alienToShootFrom.hitbox.Height), 0.1f, Color.White));

                    for (int i = 0; i < aliens.Count; i++)
                    {
                        bool didBounce = aliens[i].DidBounce(GraphicsDevice.Viewport.Width - 10);
                        if (didBounce)
                        {
                            for (int j = aliens.Count-1; j >= 0; j--)
                            {
                                if (j != i)
                                {
                                    aliens[j].Bounce();
                                }
                            }
                            for (int counter = 0; counter < aliens.Count; counter++)
                            {
                                //if (counter == i)
                                //{
                                //    game = aliens[counter].Update(GraphicsDevice.Viewport.Height, ship.hitbox);
                                //    if (game == false)
                                //    {
                                //        i = 1000;
                                //        break;
                                //    }
                                //    continue;
                                //}


                                game = aliens[counter].Update(GraphicsDevice.Viewport.Height, ship.hitbox);
                                if (game == false)
                                {
                                    i = 1000;
                                    l = true;
                                    break;
                                }
                            }
                            break;
                        }

                        game = aliens[i].Update(GraphicsDevice.Viewport.Height, ship.hitbox);
                        if (game == false)
                        {
                            l = true;
                            break;
                        }
                    }
                }



                base.Update(gameTime);

                lastKs = ks;
            }



            for (int i = 0; i < shields.Count; i++)
            {
                for (int j = 0; j < ship.bullets.Count; j++)
                {
                    bool hi = shields[i].Damage(ship.bullets[j].hitbox);
                    if (hi == true)
                    {
                        ship.bullets.RemoveAt(j);
                    }
                }
                for (int alienIndex = 0; alienIndex < aliens.Count; alienIndex++)
                {
                    for (int k = 0; k < aliens[alienIndex].alienBullets.Count; k++)
                    {
                        bool bye = shields[i].Damage(aliens[alienIndex].alienBullets[k].hitbox);
                        if (bye == true)
                        {
                            aliens[alienIndex].alienBullets.RemoveAt(k);
                        }
                    }
                }
            }

            for (int i = 0; i < shields.Count; i++)
            {
                if (shields[i].health > 8)
                {
                    shields[i].image = shield1;
                }
                else if (shields[i].health>6 && shields[i].health <9)
                {
                    shields[i].image = shield2;

                }
                else if (shields[i].health > 3 && shields[i].health < 7)
                {
                    shields[i].image = shield3;

                }
                else if (shields[i].health > 0 && shields[i].health < 4)
                {
                    shields[i].image = shield4;

                }
                else
                {
                    shields.RemoveAt(i);
                }
            }


            if (aliens.Count == 0)
            {
                game = false;
                w = true;
            }

            if(!game)
            {
                for (int i = 0; i < aliens.Count; i++)
                {
                    aliens[i].alienBullets.Clear();
                }
            }



            if (game == false && ks.IsKeyDown(Keys.R))
            {
                aliens.Clear();
                ship.bullets.Clear();
                shields.Clear();
                int num = 0;
                for (int i = 0; i < 4; i++)
                {
                    shields.Add(new Shield(shield1, new Vector2(shieldpos.X + 200 * num, shieldpos.Y), Color.White, 0.4f, 10));
                    num++;
                }
                game = true;
                int x = 1;
                int y = 0;
                for (int i = 0; i < 3; i++)
                {
                    x = 1;
                    for (int j = 0; j < 10; j++)
                    {
                        aliens.Add(new Alien(new Vector2(10, 10), alienImage, new Vector2(x, y), 0.4f, Color.White));
                        x += (int)(alienImage.Width * 0.4f) + 22;

                    }
                    y += (int)(alienImage.Height * 0.4f) + 5;
                }
                ship = new Ship(new Vector2(5, 5), Content.Load<Texture2D>("shipimage"), Vector2.Zero, 0.1f, Color.White, bulletImage);
                ship.Y = GraphicsDevice.Viewport.Height - ship.hitbox.Height;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            // TODO: Add your drawing code here

            ship.Draw(spriteBatch);
            if (game == false && l == true)
            {
                string text = "You Lose!";
                Vector2 size = font.MeasureString(text);
                spriteBatch.DrawString(font, text, new Vector2(GraphicsDevice.Viewport.Width / 2 - size.X / 2, GraphicsDevice.Viewport.Height / 2 - size.Y / 2), Color.White);
            }

            if (game == false && w == true)
            {
                string text = "You Win!";
                Vector2 size = font.MeasureString(text);
                spriteBatch.DrawString(font, text, new Vector2(GraphicsDevice.Viewport.Width / 2 - size.X / 2, GraphicsDevice.Viewport.Height / 2 - size.Y / 2), Color.White);
            }
            if (!game)
            {
                string text = "Press r to play again!!";
                Vector2 size = font.MeasureString(text);
                spriteBatch.DrawString(font, text, new Vector2(GraphicsDevice.Viewport.Width / 2 - size.X / 2, (GraphicsDevice.Viewport.Height / 2 - size.Y / 2)-60 ), Color.White);
            }

            for (int i = 0; i < aliens.Count; i++)
            {
                aliens[i].Draw(spriteBatch);
            }
            for (int i = 0; i < shields.Count; i++)
            {
                shields[i].Draw(spriteBatch);
            }

            
            spriteBatch.End();
            base.Draw(gameTime);
        }





    }
}









