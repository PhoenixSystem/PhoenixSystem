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

namespace PhoenixSample.PCL.Monogame.Aspects
{
    [AssociatedComponents(typeof(ColorComponent), typeof(PositionComponent), 
        typeof(RenderLayerComponent), typeof(ScaleComponent),typeof(SpriteFontComponent),
        typeof(StringComponent))]
    public class TextRenderAspect : BaseAspect
    {
    }

    public static class TextRenderAspectHelpers
    {
        public static IEntity CreateTextRenderEntity(this IEntity entity, string text, Color color, Vector2 position, float layerDepth, float scaleFactor, SpriteFont font)
        {
            return entity.AddComponent(new StringComponent() { Text = text }).AddComponent(new ColorComponent() { Color = color })
                            .AddComponent(new PositionComponent() { CurrentPosition = new Vector2(position.X, position.Y) })
                            .AddComponent(new RenderLayerComponent() { Depth = layerDepth }).AddComponent(new ScaleComponent() { Factor = scaleFactor })
                            .AddComponent(new SpriteFontComponent() { Font = font });
        }
    }
}
