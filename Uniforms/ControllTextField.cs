
using System;
using UnityEditor;
using UnityEngine;

namespace Uniforms.Controlls
{
    public class ControllTextField : Controll
    {
        public string Label { get; set; }
        public string Text { get; set; }

        public event Action<string> EventChange;

        public ControllTextField(string label, string initValue, GUIStyle style) : base(style)
        {
            Label = label;
            Text = initValue;
        }

        public ControllTextField(string label, string initValue) : this(label, initValue, "textField")
        {
        }

        public override void Draw()
        {
            var newValue = EditorGUILayout.TextField(Label, Text, Style, LayoutOptions);

            if (newValue != Text)
            {
                Text = newValue;
                if (EventChange != null)
                    EventChange(newValue);
            }
        }
    }
}