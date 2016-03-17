using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PhoenixSample.PCL.Monogame.Components;
using PhoenixSystem.Engine.Aspect;
using PhoenixSystem.Engine.Attributes;
using PhoenixSystem.Engine.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixSample.PCL
{
    [AssociatedComponents(typeof(PositionComponent), typeof(TextureRenderComponent), 
        typeof(ColorComponent), typeof(ScaleComponent), typeof(RotationComponent))]
    public class TextureRenderAspect :BaseAspect
    {
        
    }

    public static class TextureRenderAspectExtensions
    {
        public static IEntity MakeTextureRenderAspect(this IEntity entity, Vector2 position, bool isRotated, Vector2 origin, Rectangle sourceRect, Texture2D texture, 
                                                                           SpriteEffects effects, Color color, float scale, float rotation )
        {
            return entity.AddComponent(new PositionComponent() { CurrentPosition = position })
                        .AddComponent(new TextureRenderComponent() { IsRotated= isRotated, Origin = origin, SpriteEffects = effects, SourceRect = sourceRect, Texture = texture})
                        .AddComponent(new ColorComponent(){ Color = color})
                        .AddComponent(new ScaleComponent(){ Factor = scale })
                        .AddComponent(new RotationComponent() { Factor = rotation }
                        );
        }
    }
}
