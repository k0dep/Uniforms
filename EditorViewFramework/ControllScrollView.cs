
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EditorViewFramework.Controlls
{
    public class ControllScrollView : Controll
    {
        public Vector2 ScrollPosition { get; set; }

        public List<Controll> NestedControlls { get; set; }

        public event Action<Vector2> EventScrolled;

        public ControllScrollView(GUIStyle style, params Controll[] nestedControlls) : base(style)
        {
            NestedControlls = new List<Controll>();
            NestedControlls.AddRange(nestedControlls);
        }

        public ControllScrollView(params Controll[] nestedControlls) : this(GUIStyle.none, nestedControlls)
        {
        }

        public override void Draw()
        {
            var scroll = EditorGUILayout.BeginScrollView(ScrollPosition, Style, LayoutOptions);

            if (scroll != ScrollPosition)
            {
                ScrollPosition = scroll;
                if (EventScrolled != null)
                    EventScrolled(scroll);
            }

            foreach (var nestedControll in NestedControlls)
            {
                nestedControll.DrawEnabled();
            }

            EditorGUILayout.EndScrollView();
        }
    }
}