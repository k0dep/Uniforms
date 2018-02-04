using UnityEditor;

namespace EditorViewFramework
{
    public class Window : EditorWindow
    {
        public Controll MainControll { get; set; }

        private bool _isInit = false;



        public void OnGUI()
        {
            if (!_isInit)
            {
                _isInit = true;
                PostInit();
            }

            MainControll.DrawEnabled();
        }

        public virtual void PostInit()
        {
        }
    }
}
