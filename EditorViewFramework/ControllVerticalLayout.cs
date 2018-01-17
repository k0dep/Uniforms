using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EditorViewFramework.Controlls
{
    public class ControllVerticalLayout : Controll
    {
        public List<Controll> NestedControlls { get; set; }

        public ControllVerticalLayout(GUIStyle style, params Controll[] nestedControlls) : base(style)
        {
            NestedControlls = new List<Controll>();
            NestedControlls.AddRange(nestedControlls);
        }

        public ControllVerticalLayout(params Controll[] nestedControlls) : this(GUIStyle.none, nestedControlls)
        {
        }

        public override void Draw()
        {
            EditorGUILayout.BeginVertical(Style, LayoutOptions);

            for (int i = 0; i < NestedControlls.Count; i++)
            {
                NestedControlls[i].DrawEnabled();
            }

            EditorGUILayout.EndVertical();
        }
    }
}
