using PhoenixSystem.Engine.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoenixSystem.Engine;
using PhoenixSystem.Engine.Game;
using Microsoft.Xna.Framework.Graphics;
using PhoenixSystem.Engine.Channel;
using PhoenixSystem.Engine.Extensions;
using PhoenixSample.PCL.Monogame.Components;
using Microsoft.Xna.Framework;

namespace PhoenixSample.PCL
{
    public class TextureRenderSystem : BaseSystem, IDrawableSystem
    {
        private IEnumerable<TextureRenderAspect> _aspects;
        private SpriteBatch _spriteBatch;
        private const float ClockwiseNinetyDegreeRotation = (float)(Math.PI / 2.0f);

        public TextureRenderSystem(SpriteBatch spriteBatch, IChannelManager channelManager, int priority, params string[] channels)
            : base(channelManager, priority, channels)
        {
            _spriteBatch = spriteBatch;
        }
        public bool IsDrawing { get; set; } = false;

        public override void AddToGameManager(IGameManager gameManager)
        {
            _aspects = gameManager.GetAspectList<TextureRenderAspect>();
        }

        public void Draw(ITickEvent tickEvent)
        {
            IsDrawing = true;
            foreach (var aspect in _aspects)
            {
                var position = aspect.GetComponent<PositionComponent>();
                var texture = aspect.GetComponent<TextureRenderComponent>();
                var color = aspect.GetComponent<ColorComponent>();
                var scale = aspect.GetComponent<ScaleComponent>();
                var rotation = aspect.GetComponent<RotationComponent>().Factor;
                var spriteEffects = texture.SpriteEffects;
                Vector2 origin = texture.Origin;
                if (texture.IsRotated)
                {
                    rotation -= ClockwiseNinetyDegreeRotation;
                    switch (spriteEffects)
                    {
                        case SpriteEffects.FlipHorizontally: spriteEffects = SpriteEffects.FlipVertically; break;
                        case SpriteEffects.FlipVertically: spriteEffects = SpriteEffects.FlipHorizontally; break;
                    }
                }
                switch (spriteEffects)
                {
                    case SpriteEffects.FlipHorizontally: origin.X = texture.SourceRect.Width - origin.X; break;
                    case SpriteEffects.FlipVertically: origin.Y = texture.SourceRect.Height - origin.Y; break;
                }

                _spriteBatch.Draw(
                    texture: texture.Texture,
                    position: position.CurrentPosition,
                    sourceRectangle: texture.SourceRect,
                    color: color.Color,
                    rotation: rotation,
                    origin: origin,
                    scale: new Vector2(scale.Factor, scale.Factor),
                    effects: spriteEffects
                    );
            }

            IsDrawing = false;
        }

        public override void RemoveFromGameManager(IGameManager gameManager)
        {
            
        }

        public override void Update(ITickEvent tickEvent)
        {
            
        }
    }
}
