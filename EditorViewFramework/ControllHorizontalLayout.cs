
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EditorViewFramework.Controlls
{
    public class ControllHorizontalLayout : Controll
    {
        public List<Controll> NestedControlls { get; set; }

        public ControllHorizontalLayout(GUIStyle style, params Controll[] nestedControlls) : base(style)
        {
            NestedControlls = new List<Controll>();
            NestedControlls.AddRange(nestedControlls);
        }

        public ControllHorizontalLayout(params Controll[] nestedControlls) : this(new GUIStyle(), nestedControlls)
        {
        }

        public override void Draw()
        {
            EditorGUILayout.BeginHorizontal(Style, LayoutOptions);

            for (int i = 0; i < NestedControlls.Count; i++)
            {
                NestedControlls[i].DrawEnabled();
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}
