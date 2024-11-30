using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invader
{
    class Shield : Sprite
    {
        public int health { get; set; }

        public Shield(Texture2D image, Vector2 position, Color tint, float scale, int health) : base(image, position, tint, scale)
        {
            this.health = health;

        }

        public override void Update()
        {
            base.Update();
        }

        public bool Damage(Rectangle bullethitbox)
        {
            if (bullethitbox.Intersects(hitbox))
            {                
                health--;
                return true;
            }
            return false;




        }




    }
}
