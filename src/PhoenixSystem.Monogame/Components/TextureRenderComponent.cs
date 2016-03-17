using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PhoenixSystem.Engine.Component;

namespace PhoenixSystem.Monogame.Components
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
            return new TextureRenderComponent
            {
                Texture = Texture,
                SourceRect = new Rectangle(SourceRect.X, SourceRect.Y, SourceRect.Width, SourceRect.Height),
                Origin = new Vector2(Origin.X, Origin.Y),
                SpriteEffects = SpriteEffects,
                IsRotated = IsRotated
            };
        }

        public override void Reset()
        {
            Texture = null;
            SourceRect = Rectangle.Empty;
            Origin = Vector2.Zero;
            SpriteEffects = SpriteEffects.None;
            IsRotated = false;
        }
    }
}