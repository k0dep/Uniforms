using System;
using UnityEditor;
using UnityEngine;

namespace Uniforms
{
    public class ControllIntField : Controll
    {
        public string Label { get; set; }
        public int Value { get; set; }

        public event Action<int> EventChanged;

        public ControllIntField(string label, int value, GUIStyle style) : base(style)
        {
            Label = label;
            Value = value;
        }

        public ControllIntField(string label, int value) : this(label, value, "textField")
        {
        }

        public override void Draw()
        {
            var newValue = EditorGUILayout.IntField(Label, Value, Style, LayoutOptions);

            if (newValue != Value)
            {
                Value = newValue;

                if (EventChanged != null)
                    EventChanged(newValue);
            }
        }
    }
}
