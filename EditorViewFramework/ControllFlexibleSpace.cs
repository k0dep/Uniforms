using UnityEngine;

namespace EditorViewFramework.Controlls
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

