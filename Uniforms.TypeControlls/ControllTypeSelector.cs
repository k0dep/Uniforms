using System;
using System.Collections.Generic;

namespace Uniforms.TypeControlls
{
    public class ControllTypeSelector : Controll
    {
        public List<Type> TypeVariants { get; private set; }

        public Type Selected { get; set; }
        public bool CanNull { get; set; }
        public event Action<Type> EventChange;

        private ControllPopup<Type> _Popup;

        public ControllTypeSelector(string label, bool canNull, params Type[] types) : base(null)
        {
            TypeVariants = new List<Type>();

            CanNull = canNull;

            _Popup = new ControllPopup<Type>(label, canNull, types);
            _Popup.EventChange += type =>
            {
                if (EventChange != null)
                    EventChange(type);
                Selected = type;
            };
        }

        public void AddType(Type type)
        {
            TypeVariants.Add(type);
            _Popup.Variants.Add(new NamedValue<Type>(type.ToString(), type));
        }

        public void RemoveType(Type type)
        {
            TypeVariants.Remove(type);
            _Popup.Variants.RemoveAll(t => t.Value == type);
        }


        public override void Draw()
        {
            _Popup.DrawEnabled();
        }
    }
}
