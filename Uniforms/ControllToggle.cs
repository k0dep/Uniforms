using System;
using UnityEditor;
using UnityEngine;

namespace Uniforms
{
    public class ControllToggle : Controll
    {
        public string Label { get; set; }
        public bool Value { get; set; }

        public event Action<bool> EventChanged;

        public ControllToggle(string label, bool value, GUIStyle style) : base(style)
        {
            Label = label;
            Value = value;
        }

        public ControllToggle(string label, bool value) : this(label, value, "Toggle")
        {
        }

        public override void Draw()
        {
            var newValue = GUILayout.Toggle(Value, Label, Style, LayoutOptions);

            if (newValue != Value)
            {
                Value = newValue;

                if (EventChanged != null)
                    EventChanged(newValue);
            }
        }
    }
}
