using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invader
{
    class Sprite
    {
        public Texture2D image { get; set; }
        public Vector2 position { get; set; }

        public float X
        {
            get
            {
                return position.X;
            }
            set
            {
                position = new Vector2(value, position.Y);
            }
        }
        public float Y
        {
            get
            {
                return position.Y;
            }
            set
            {
                position = new Vector2(position.X, value);
            }
        }
        public Color tint { get; set; }
        
        float Scale { get; set; }
        public Rectangle hitbox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, (int)(image.Width * Scale), (int)(image.Height * Scale));
            }
        }
        
       

        public Sprite(Texture2D image, Vector2 position, Color tint, float scale)
        {
            this.image = image;
            this.position = position;
            this.tint = tint;
            Scale = scale;
        }

        public virtual void Update()
        {
        
        }

        public virtual void Draw(SpriteBatch sb)
        {
            Vector2 hitboxLocation = new Vector2(hitbox.X, hitbox.Y);
            sb.Draw(image, position, null, tint, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
           
        }

    }
}
