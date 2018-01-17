
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EditorViewFramework.Controlls
{
    public class ControllToolbarButtons<TType> : Controll
    {
        public struct ToolbarButtonContent
        {
            public TType Value;
            public string ButtonText;

            public ToolbarButtonContent(TType value, string buttonText)
            {
                Value = value;
                ButtonText = buttonText;
            }
        }


        public event Action<TType> EventChangeSelected;

        public TType Selected { get; set; }

        public List<ToolbarButtonContent> Buttons { get; set; }

        private int selected;

        public ControllToolbarButtons(params ToolbarButtonContent[] buttons) : base(null)
        {
            Buttons = buttons.ToList();
            Selected = buttons[0].Value;
        }

        public override void Draw()
        {
            var newSelected = GUILayout.Toolbar(selected, Buttons.Select(t => t.ButtonText).ToArray(), LayoutOptions);
            if (selected != newSelected && EventChangeSelected != null)
                EventChangeSelected(Buttons[newSelected].Value);

            selected = newSelected;
            Selected = Buttons[newSelected].Value;
        }
    }
}
