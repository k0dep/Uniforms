using UnityEditor;

namespace Uniforms
{
    public class Window : EditorWindow
    {
        public Controll MainControll { get; set; }

        public void OnGUI()
        {
            if (MainControll == null)
                PostInit();

            MainControll?.DrawEnabled();
        }

        public virtual void PostInit()
        {
        }
    }
}
