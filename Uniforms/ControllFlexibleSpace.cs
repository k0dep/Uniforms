using UnityEngine;

namespace Uniforms.Controlls
{
    public class ControllFlexibleSpace : Controll
    {
        public ControllFlexibleSpace() : base(null)
        {
        }

        public override void Draw()
        {
            GUILayout.FlexibleSpace();
        }
    }
}

