using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Uniforms
{
    public class ControllPopup<T> : Controll
    {
        public string Label { get; set; }

        public List<NamedValue<T>> Variants { get; private set; }

        public T Selected { get; set; }

        public bool CanNullable { get; set; }

        public event Action<T> EventChange;


        public ControllPopup() : this(null, true, new NamedValue<T>[0]) { }

        public ControllPopup(params NamedValue<T>[] variants) : this(null, false, variants) { }

        public ControllPopup(params T[] variants) : this(null, false, VariantsFromValues(variants)) { }

        public ControllPopup(string label, bool canNull, params T[] variants) : this(label, canNull, VariantsFromValues(variants)) { }

        public ControllPopup(string label, params T[] variants) : this(label, false, VariantsFromValues(variants)) { }

        public ControllPopup(string label, params NamedValue<T>[] variants) : this(label, false, variants) { }

        public ControllPopup(string label, bool canNullable, params NamedValue<T>[] variants) : base(null)
        {
            Label = label;

            if (variants.Length == 0)
                throw new Exception("should any or more variants");

            CanNullable = false;
            Variants = new List<NamedValue<T>>();
            Variants.AddRange(variants);

            if (canNullable && !typeof(T).IsValueType)
            {
                Selected = default(T);
                CanNullable = true;
            }
            else
                Selected = variants.Length > 0 ? variants[0].Value : default(T);
        }



        public override void Draw()
        {
            var options = Variants.Select(t => t.Name).ToList();
            if(CanNullable)
                options.Insert(0, "null");

            if (!options.Any())
                throw new InvalidDataException("options for select in dropdown not contains any element");

            var index = Variants.FindIndex(t => t.Value.Equals(Selected));
            var current_selected = Selected == null ? 0 : index == -1 ? 0 : index + (CanNullable ? 1 : 0);

            int new_selection;

            if (Label == null)
                new_selection = EditorGUILayout.Popup(current_selected, options.ToArray(), LayoutOptions);
            else
                new_selection = EditorGUILayout.Popup(new GUIContent(Label), current_selected,
                    options.Select(t => new GUIContent(t)).ToArray(), LayoutOptions);

            if (current_selected == new_selection) return;
            
            var variantValue = Variants.FirstOrDefault(t => t.Name == options[new_selection]);
            var newSelected = variantValue == null ? default(T) : variantValue.Value;
            EventChange?.Invoke(newSelected);
            Selected = newSelected;
        }

        private static NamedValue<T>[] VariantsFromValues(T[] variants)
        {
            return variants.Select(t => new NamedValue<T>(t.ToString(), t)).ToArray();
        }
    }

    public class NamedValue<TType>
    {
        public string Name { get; set; }
        public TType Value { get; set; }

        public NamedValue(string name, TType value)
        {
            Name = name;
            Value = value;
        }
    }
}
