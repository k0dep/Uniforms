using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Uniforms
{
    public class ControllGeneralProperty : Controll
    {
        public Type PropertyType { get; set; }
        public object Value { get; set; }
        public string Label { get; set; }

        public event Action<object> EventChange;

        public ControllGeneralProperty(Type propertyType, object defValue, string label, GUIStyle style) : base(style)
        {
            if (!IsPrimitive(propertyType)
                && !IsArray(propertyType)
                && !IsEnum(propertyType)
                && !IsUnityObject(propertyType))
                throw new Exception("wrong type");

            Label = label;
            PropertyType = propertyType;
            Value = defValue;
        }

        public ControllGeneralProperty(Type propertyType, object defValue, string label) : this(propertyType, defValue, label, null) { }

        private static bool IsUnityObject(Type propertyType)
        {
            return typeof(Object).IsAssignableFrom(propertyType);
        }

        private static bool IsEnum(Type t)
        {
            return t.IsEnum;
        }

        private static bool IsPrimitive(Type t)
        {
            return t == typeof(int)
                   || t == typeof(float)
                   || t == typeof(bool)
                   || t == typeof(string)
                   || t == typeof(Vector2)
                   || t == typeof(Vector3);
        }

        private static bool IsArray(Type t)
        {
            return t.IsArray && IsPrimitive(t.GetElementType());
        }



        public override void Draw()
        {
            object newValue = null;
            if (IsPrimitive(PropertyType))
                newValue = DrawPrimitive();
            else if (IsArray(PropertyType))
                newValue = DrawArray();
            else if (IsEnum(PropertyType))
                newValue = DrawEnum();
            else if (IsUnityObject(PropertyType))
                newValue = DrawUnityObject();

            if (newValue == Value) return;

            EventChange?.Invoke(newValue);
            Value = newValue;
        }

        private object DrawUnityObject()
        {
            return EditorGUILayout.ObjectField(Label, (Object) Value, PropertyType, true, LayoutOptions);
        }

        private object DrawEnum()
        {
            return EditorGUILayout.EnumPopup(Label, (Enum) Value, LayoutOptions);
        }

        private object DrawArray()
        {
            throw new NotImplementedException();
        }

        private object DrawPrimitive()
        {
            if (PropertyType == typeof(int))
                return Style != null
                    ? EditorGUILayout.IntField(Label, (int) Value, Style, LayoutOptions)
                    : EditorGUILayout.IntField(Label, (int) Value, LayoutOptions);

            if (PropertyType == typeof(bool))
                return Style != null
                    ? EditorGUILayout.Toggle(Label, (bool) Value, Style, LayoutOptions)
                    : EditorGUILayout.Toggle(Label, (bool)Value, LayoutOptions);

            if (PropertyType == typeof(float))
                return Style != null
                    ? EditorGUILayout.FloatField(Label, (float)Value, Style, LayoutOptions)
                    : EditorGUILayout.FloatField(Label, (float)Value, LayoutOptions);

            if (PropertyType == typeof(string))
                return Style != null
                    ? EditorGUILayout.TextField(Label, (string)Value, Style, LayoutOptions)
                    : EditorGUILayout.TextField(Label, (string)Value, LayoutOptions);

            if (PropertyType == typeof(Vector3))
                return EditorGUILayout.Vector3Field(Label, (Vector3) Value, LayoutOptions);

            if (PropertyType == typeof(Vector2))
                return EditorGUILayout.Vector2Field(Label, (Vector2)Value, LayoutOptions);

            return null;
        }
    }
}
