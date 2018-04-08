using System;
using UnityEngine;

namespace Uniforms.Controlls
{
    public class ControllObjectField<TType> : Controll
        where TType : class
    {
        public string Label
        {
            get { return originControllObjectFieldAccess.Label; }
            set { originControllObjectFieldAccess.Label = value; }
        }

        public event Action<TType> EventChange
        {
            add { originControllObjectFieldAccess.EventChange += value; }
            remove { originControllObjectFieldAccess.EventChange -= value; }
        }

        public TType Value { get; set; }

        private ControllObjectFieldAccess<TType> originControllObjectFieldAccess;

        public ControllObjectField(string label, TType value = null) : base(null)
        {
            Value = value;
            originControllObjectFieldAccess = new ControllObjectFieldAccess<TType>(label, () => Value, type => Value = type);
        }

        public override void Draw()
        {
            originControllObjectFieldAccess.DrawEnabled();
        }

        public override void AddLayoutOptions(GUILayoutOption option)
        {
            originControllObjectFieldAccess.AddLayoutOptions(option);
        }
    }
}