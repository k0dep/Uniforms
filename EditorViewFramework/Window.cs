using UnityEditor;

namespace EditorViewFramework
{
    public class Window : EditorWindow
    {
        public Controll MainControll { get; set; }

        public void OnGUI()
        {
            MainControll.DrawEnabled();
        }
    }
}
