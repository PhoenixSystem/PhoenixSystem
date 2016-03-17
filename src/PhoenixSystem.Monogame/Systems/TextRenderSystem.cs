using PhoenixSystem.Engine.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoenixSystem.Engine;
using PhoenixSystem.Engine.Game;
using PhoenixSystem.Engine.Channel;
using Microsoft.Xna.Framework.Graphics;
using PhoenixSample.PCL.Monogame.Aspects;
using Microsoft.Xna.Framework;
using PhoenixSystem.Engine.Aspect;
using PhoenixSample.PCL.Monogame.Components;
using PhoenixSystem.Engine.Extensions;

namespace PhoenixSample.PCL.Systems
{
    public class TextRenderSystem : BaseSystem, IDrawableSystem
    {
        SpriteBatch _spriteBatch;
        IEnumerable<TextRenderAspect> _aspects;
        public TextRenderSystem(SpriteBatch spriteBatch, IChannelManager channelManager, int priority, params string[] channels)
            : base(channelManager, priority, channels)
        {
            _spriteBatch = spriteBatch;
        }
        public bool IsDrawing { get; private set; } = false;

        public override void AddToGameManager(IGameManager gameManager)
        {
            _aspects = gameManager.GetAspectList<TextRenderAspect>();
        }

        public void Draw(ITickEvent tickEvent)
        {
            foreach (var aspect in _aspects)
            {
                var font = aspect.GetComponent<SpriteFontComponent>().Font;
                var text = aspect.GetComponent<StringComponent>().Text;
                var pos = aspect.GetComponent<PositionComponent>().CurrentPosition;
                var color = aspect.GetComponent<ColorComponent>().Color ?? Color.Black;
                var scale = aspect.GetComponent<ScaleComponent>().Factor;
                var layerDepth = aspect.GetComponent<RenderLayerComponent>().Depth;
                _spriteBatch.DrawString(font, text, pos, color, 0, Vector2.Zero, scale, SpriteEffects.None,0);
            }
        }

        public override void RemoveFromGameManager(IGameManager gameManager)
        {
            
        }

        public override void Update(ITickEvent tickEvent)
        {
            
        }
    }
}
