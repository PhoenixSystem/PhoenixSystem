using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PhoenixSystem.Engine;
using PhoenixSystem.Engine.Channel;
using PhoenixSystem.Engine.Extensions;
using PhoenixSystem.Engine.Game;
using PhoenixSystem.Engine.System;
using PhoenixSystem.Monogame.Aspects;
using PhoenixSystem.Monogame.Components;

namespace PhoenixSystem.Monogame.Systems
{
    public class TextRenderSystem : BaseSystem, IDrawableSystem
    {
        private IEnumerable<TextRenderAspect> _aspects;
        private readonly SpriteBatch _spriteBatch;

        public TextRenderSystem(SpriteBatch spriteBatch, IChannelManager channelManager, int priority,
            params string[] channels)
            : base(channelManager, priority, channels)
        {
            _spriteBatch = spriteBatch;
        }

        public bool IsDrawing { get; } = false;

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
                _spriteBatch.DrawString(font, text, pos, color, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
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