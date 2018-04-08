using System.Linq;
using UnityEngine;

namespace Uniforms
{
    public abstract class Controll
    {
        public virtual GUIStyle Style { get; set; }
        protected GUILayoutOption[] LayoutOptions { get; set; }

        private bool _Enabled = true;

        protected Controll(GUIStyle style)
        {
            Style = style;
            LayoutOptions = new GUILayoutOption[0];
        }

        public virtual void AddLayoutOptions(GUILayoutOption option)
        {
            var list = LayoutOptions.ToList();
            list.Add(option);
            LayoutOptions = list.ToArray();
        }

        public bool Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; }
        }

        public void DrawEnabled()
        {
            if(Enabled)
                Draw();
        }

        public abstract void Draw();
    }
}
