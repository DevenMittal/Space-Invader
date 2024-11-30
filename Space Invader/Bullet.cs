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
    class Bullet : Sprite
    {
        public Vector2 speed { get; set; }

        public Bullet(Vector2 speed, Texture2D image, Vector2 position, float scale, Color tint) : base(image, position, tint, scale)
        {
            this.speed = speed;           
        }


        public void Update()
        {
            this.Y -= speed.Y;
            base.Update();
        }

        public void Update2()
        {
            this.Y += speed.Y;
            base.Update();
        }

        




    }
}
