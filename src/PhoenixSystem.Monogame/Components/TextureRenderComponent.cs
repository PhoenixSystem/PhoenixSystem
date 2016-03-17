using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PhoenixSystem.Engine.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixSample.PCL.Monogame.Components
{
    public class TextureRenderComponent : BaseComponent
    {
        public Texture2D Texture { get; set; }
        public Rectangle SourceRect { get; set; }
        public Vector2 Origin { get; set; }
        public SpriteEffects SpriteEffects { get; set; }
        public bool IsRotated { get; set; }
        
        public override IComponent Clone()
        {
            return new TextureRenderComponent()
            {
                Texture = this.Texture,
                SourceRect = new Rectangle(this.SourceRect.X, this.SourceRect.Y, this.SourceRect.Width, this.SourceRect.Height),
                Origin = new Vector2(this.Origin.X, this.Origin.Y),
                SpriteEffects = this.SpriteEffects,
                IsRotated = this.IsRotated
            };
        }
        public override void Reset()
        {
            Texture = null;
            SourceRect = Rectangle.Empty;
            Origin = Vector2.Zero;
            SpriteEffects = SpriteEffects.None;
            this.IsRotated = false;
        }
    }
}
