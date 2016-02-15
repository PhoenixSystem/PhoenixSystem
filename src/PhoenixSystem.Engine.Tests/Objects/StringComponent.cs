using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixSystem.Engine.Tests.Classes
{
    class StringComponent : BaseComponent
    {
        public string Value { get; set; } = string.Empty;
        public override IComponent Clone()
        {
            return new StringComponent() { Value = this.Value };
        }

        public override void Reset()
        {
            Value = string.Empty;
        }
    }
}
