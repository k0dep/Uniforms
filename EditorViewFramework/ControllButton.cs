
using System;
using UnityEngine;

namespace EditorViewFramework.Controlls
{
    public class ControllButton : Controll
    {
        public event Action EventClick;
        public string Text { get; set; }

        public ControllButton(string text) : this(text, "Button")
        {
        }

        public ControllButton(string text, GUIStyle style) : base(style)
        {
            Text = text;
        }

        public override void Draw()
        {
            if (GUILayout.Button(Text, Style, LayoutOptions))
            {
                if (EventClick != null)
                    EventClick();
            }
        }
    }
}

