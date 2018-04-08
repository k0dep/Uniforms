
using System;
using UnityEditor;

namespace Uniforms.Controlls
{
    public class ControllObjectFieldAccess<TType> : Controll
        where TType : class
    {
        public Func<TType> ValueGetter { get; set; }
        public Action<TType> ValueSetter { get; set; }
        public string Label { get; set; }

        public event Action<TType> EventChange;

        public ControllObjectFieldAccess(string label, Func<TType> valueGetter, Action<TType> valueSetter) : base(null)
        {
            Label = label;
            ValueGetter = valueGetter;
            ValueSetter = valueSetter;
        }

        public override void Draw()
        {
            if (ValueGetter == null)
                throw new Exception("value getter of ControllObjectField is null");
            if (ValueSetter == null)
                throw new Exception("value setter of ControllObjectField is null");

            var lastValue = ValueGetter();

            var newValue = EditorGUILayout.ObjectField(Label, lastValue as UnityEngine.Object, typeof(TType), true, LayoutOptions) as TType;

            if (newValue != lastValue)
            {
                ValueSetter(newValue);
                if (EventChange != null)
                    EventChange(newValue);
            }
        }
    }
}