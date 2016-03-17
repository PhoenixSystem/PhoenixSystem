using PhoenixSystem.Engine.Component;

namespace PhoenixSystem.Monogame.Components
{
    public class AnimationComponent : BaseComponent
    {
        public string CurrentAnimation { get; set; } = string.Empty;
        public float FPS { get; set; } = 10.0f;

        public float TimePerFrame => 1.0f/FPS;

        public int CurrentFrameIndex { get; set; }
        public bool ShouldLoop { get; set; }
        public bool Active { get; set; }
        public float TimeInCurrentFrame { get; set; }

        public override IComponent Clone()
        {
            return new AnimationComponent
            {
                CurrentAnimation = CurrentAnimation,
                FPS = FPS,
                CurrentFrameIndex = CurrentFrameIndex,
                ShouldLoop = ShouldLoop,
                Active = Active,
                TimeInCurrentFrame = TimeInCurrentFrame
            };
        }

        public override void Reset()
        {
            CurrentFrameIndex = 0;
            FPS = 10.0f;
            CurrentAnimation = string.Empty;
            ShouldLoop = false;
            Active = false;
            TimeInCurrentFrame = 0.0f;
        }
    }
}