using PhoenixSystem.Engine.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixSample.PCL.Monogame.Components
{
    public class AnimationComponent : BaseComponent
    {

        public string CurrentAnimation { get; set; } = string.Empty;
        public float FPS { get; set; } = 10.0f;
        public float TimePerFrame
        {
            get
            {
                return 1.0f/ FPS;
            }
        }
        public int CurrentFrameIndex { get; set; } = 0;
        public bool ShouldLoop { get; set; } = false;
        public bool Active { get; set; } = false;
        public float TimeInCurrentFrame { get; set; } = 0.0f;

        public override IComponent Clone()
        {
            return new AnimationComponent()
            {
                CurrentAnimation = this.CurrentAnimation,
                FPS = this.FPS,
                CurrentFrameIndex = this.CurrentFrameIndex,
                ShouldLoop = this.ShouldLoop,
                Active = this.Active,
                TimeInCurrentFrame = this.TimeInCurrentFrame
            };
        }

        public override void Reset()
        {
            this.CurrentFrameIndex = 0;
            this.FPS = 10.0f;
            this.CurrentAnimation = string.Empty;
            this.ShouldLoop = false;
            this.Active = false;
            this.TimeInCurrentFrame = 0.0f;
        }
    }
}
