using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixSystem.Engine
{
    public interface IDrawable
    {
        bool IsDrawing { get; }
        void Draw(ITickEvent tickEvent);
    }
}
