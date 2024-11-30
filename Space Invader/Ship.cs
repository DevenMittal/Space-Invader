using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invader
{
    class Ship : Sprite
    {
        public Vector2 speed{get; set;}

        Texture2D bulletImage;
        public List<Bullet> bullets { get; }


        public Ship(Vector2 speed, Texture2D image, Vector2 position, float scale, Color tint, Texture2D bulletImage) : base(image, position, tint, scale)
        {
            this.speed = speed;
            bullets = new List<Bullet>();
            this.bulletImage = bulletImage;
        }

        public void Update(Viewport v, KeyboardState ks, List<Alien> aliens, KeyboardState lastKs, bool game)
        {
            base.Update();

            if (ks.IsKeyDown(Keys.Left) && position.X > 0)
            {
                this.X -= speed.X;
            }
            else if (ks.IsKeyDown(Keys.Right) && position.X + hitbox.Width <= v.Width)
            {
                this.X += speed.X;
            }


            if (ks.IsKeyDown(Keys.Space) && !lastKs.IsKeyDown(Keys.Space) && bullets.Count < 1 && game == true)
            {
                bullets.Add(new Bullet(new Vector2(5, 5), bulletImage, new Vector2(hitbox.Center.X, Y), 0.1f, Color.Green));
            }


            for (int i = 0; i < bullets.Count; i++)
            {
                for (int j = 0; j < aliens.Count; j++)
                {
                    if (bullets[i].hitbox.Intersects(aliens[j].hitbox))
                    {
                        aliens.Remove(aliens[j]);
                        bullets.Remove(bullets[i]);
                        break;

                    }

                }

            }


            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Update();
                if (bullets[i].Y < 0)
                {
                    bullets.Remove(bullets[i]);
                }
            }


        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Draw(sb);
            }
        }


    }
}
