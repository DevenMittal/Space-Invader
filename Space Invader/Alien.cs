using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invader
{
    class Alien : Sprite
    {
        public Vector2 Speed;
        public List<Bullet> alienBullets { get; }

        public Alien(Vector2 speed, Texture2D image, Vector2 position, float scale, Color tint) : base(image, position, tint, scale)
        {
            this.Speed = speed;

            alienBullets = new List<Bullet>();

        }

        public bool Update(int height, Rectangle shiphitbox)
        {

            X += Speed.X;

            for (int i = 0; i < alienBullets.Count; i++)
            {
                if (alienBullets[i].position.Y > height)
                {
                    alienBullets.Remove(alienBullets[i]);
                }
            }

            for (int i = 0; i < alienBullets.Count; i++)
            {
                if (alienBullets[i].hitbox.Intersects(shiphitbox))
                {
                    return false;
                }
            }

           
           


            return true;
        }

        public void BulletMove()
        {
            for (int i = 0; i < alienBullets.Count; i++)
            {
                alienBullets[i].Update2();
            }
        }
         
        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
            for (int i = 0; i < alienBullets.Count; i++)
            {
                alienBullets[i].Draw(sb);
            }
        }

        public bool DidBounce(int width)
        {
            if (position.X < 0|| position.X + hitbox.Width > width)
            {
                Bounce();
                return true;
            }
            return false;
        }


        public void Bounce()
        {
            Speed.X *= -1;
            Y += 15;
        }


        public bool ShieldInterception(Rectangle shieldHitbox)
        {
            for (int i = 0; i < alienBullets.Count; i++)
            {
                if (alienBullets[i].hitbox.Intersects(shieldHitbox))
                {
                    return true;
                }
            }
            return false;
        }







    }
}
