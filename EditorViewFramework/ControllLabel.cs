using UnityEditor;
using UnityEngine;

namespace EditorViewFramework
{
    public class ControllLabel : Controll
    {
        public string Label { get; set; }

        public ControllLabel(string label, GUIStyle style) : base(style)
        {
            Label = label;
        }

        public ControllLabel(string label) : this(label, new GUIStyle())
        {
        }

        public override void Draw()
        {
            EditorGUILayout.LabelField(Label, Style, LayoutOptions);
        }
    }
}
