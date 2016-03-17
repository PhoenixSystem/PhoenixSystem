using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PhoenixSystem.Engine.Aspect;
using PhoenixSystem.Engine.Attributes;
using PhoenixSystem.Engine.Entity;
using PhoenixSystem.Monogame.Components;

namespace PhoenixSystem.Monogame.Aspects
{
    [AssociatedComponents(typeof (PositionComponent), typeof (TextureRenderComponent),
        typeof (ColorComponent), typeof (ScaleComponent), typeof (RotationComponent))]
    public class TextureRenderAspect : BaseAspect
    {
    }

    public static class TextureRenderAspectExtensions
    {
        public static IEntity MakeTextureRenderAspect(this IEntity entity, Vector2 position, bool isRotated,
            Vector2 origin, Rectangle sourceRect, Texture2D texture,
            SpriteEffects effects, Color color, float scale, float rotation)
        {
            return entity.AddComponent(new PositionComponent {CurrentPosition = position})
                .AddComponent(new TextureRenderComponent
                {
                    IsRotated = isRotated,
                    Origin = origin,
                    SpriteEffects = effects,
                    SourceRect = sourceRect,
                    Texture = texture
                })
                .AddComponent(new ColorComponent {Color = color})
                .AddComponent(new ScaleComponent {Factor = scale})
                .AddComponent(new RotationComponent {Factor = rotation}
                );
        }
    }
}