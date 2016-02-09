using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixSystem.Engine
{
    public class AspectManager<AspectType> where AspectType : BaseAspect, new()
    {
        LinkedList<AspectType> _activeAspects = new LinkedList<AspectType>();
        LinkedList<AspectType> _availableAspects = new LinkedList<AspectType>();         

        public AspectType Get(Entity e)
        {
            AspectType aspect = null;
            if(_availableAspects.Count > 0)
            {
                aspect = _availableAspects.First.Value;
                aspect.Reset();
                _availableAspects.RemoveFirst();   
            }
            else
            {
                aspect = new AspectType();                
            }
            aspect.Init(e);
            aspect.Deleted += Aspect_Deleted;
            _activeAspects.AddLast(aspect);
            return aspect;
        }

        protected virtual void Aspect_Deleted(object sender, EventArgs e)
        {
            AspectType aspect = (AspectType)sender;
            aspect.Deleted -= Aspect_Deleted;
            _availableAspects.AddLast(aspect);

            _activeAspects.Remove(aspect);
        }

        public void ClearCache()
        {
            _availableAspects.Clear();

        }

        public int AvailableAspectCount
        {
            get
            {
                return _availableAspects.Count;
            }
        }
    }
}
