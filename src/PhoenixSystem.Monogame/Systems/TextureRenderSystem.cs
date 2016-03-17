using System;
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
    public class TextureRenderSystem : BaseSystem, IDrawableSystem
    {
        private const float ClockwiseNinetyDegreeRotation = (float) (Math.PI/2.0f);
        private readonly SpriteBatch _spriteBatch;
        private IEnumerable<TextureRenderAspect> _aspects;

        public TextureRenderSystem(SpriteBatch spriteBatch, IChannelManager channelManager, int priority,
            params string[] channels)
            : base(channelManager, priority, channels)
        {
            _spriteBatch = spriteBatch;
        }

        public bool IsDrawing { get; set; }

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
                var origin = texture.Origin;
                if (texture.IsRotated)
                {
                    rotation -= ClockwiseNinetyDegreeRotation;

                    switch (spriteEffects)
                    {
                        case SpriteEffects.FlipHorizontally:
                            spriteEffects = SpriteEffects.FlipVertically;
                            break;

                        case SpriteEffects.FlipVertically:
                            spriteEffects = SpriteEffects.FlipHorizontally;
                            break;

                        case SpriteEffects.None:
                            break;

                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                switch (spriteEffects)
                {
                    case SpriteEffects.FlipHorizontally:
                        origin.X = texture.SourceRect.Width - origin.X;
                        break;

                    case SpriteEffects.FlipVertically:
                        origin.Y = texture.SourceRect.Height - origin.Y;
                        break;

                    case SpriteEffects.None:
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                _spriteBatch.Draw(texture.Texture, position.CurrentPosition, sourceRectangle: texture.SourceRect, color: color.Color, rotation: rotation, origin: origin, scale: new Vector2(scale.Factor, scale.Factor), effects: spriteEffects);
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