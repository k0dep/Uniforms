using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EditorViewFramework.Controlls;


namespace EditorViewFramework.TypeControlls
{
    public class ControllObjectConstructor : Controll
    {
        public ObjectConstructorParams CurrentConstructorParams { get; private set; }

        private ControllPopup<ConstructorInfo> _constructorPopup;
        private ControllVerticalLayout _mainLayout;


        public ControllObjectConstructor(string label, bool canNull, params Type[] types) : this(label, canNull, ConstructorsFromTypes(types)) {}

        public ControllObjectConstructor(string label, bool canNull, params ConstructorInfo[] constructors) : base(null)
        {
            _constructorPopup = new ControllPopup<ConstructorInfo>(label, canNull, ConstructorsPopupValues(constructors));
            _constructorPopup.EventChange += RefreshConstructor;
            RefreshConstructor(_constructorPopup.Selected);
        }




        private static ConstructorInfo[] ConstructorsFromTypes(Type[] types)
        {
            return types
                .SelectMany(t => t.GetConstructors())
                .Where(t => !t.GetCustomAttributes(typeof(ConstructorIgnoreAttribute), true).Any())
                .ToArray();
        }


        private void RefreshConstructor(ConstructorInfo constructor)
        {
            CurrentConstructorParams = null;
            if(constructor == null)
                return;

            CurrentConstructorParams = new ObjectConstructorParams(constructor);
            RefreshParametersView(constructor);
        }

        private NamedValue<ConstructorInfo>[] ConstructorsPopupValues(ConstructorInfo[] constructors)
        {
            return constructors.Select(t =>
            {
                var constructorAttr = (ConstructorNameAttribute) t.GetCustomAttributes(typeof(ConstructorNameAttribute), false).FirstOrDefault();



                var constructorName = t.DeclaringType.Name + "(";
                foreach (var paramName in t.GetParameters().Select(p => p.ParameterType.Name).ToList())
                    constructorName += paramName + ", ";
                constructorName += ")";

                if (constructorAttr != null)
                    constructorName = constructorAttr.Name;

                return new NamedValue<ConstructorInfo>(constructorName, t);

            }).ToArray();
        }

        private void RefreshParametersView(ConstructorInfo constructor)
        {
            var parameters = constructor.GetParameters();

            var parametersProperties = new List<ControllGeneralProperty>();

            var parameterIndex = -1;
            foreach (var parameterInfo in parameters)
            {
                parameterIndex++;

                object defaultValue = GetDefaulValueParameter(parameterInfo);

                if (IsIgnoreParameter(parameterInfo, ref defaultValue))
                {
                    CurrentConstructorParams.Params[parameterIndex] = defaultValue;
                    continue;
                }

                CurrentConstructorParams.Params[parameterIndex] = defaultValue;

                var parameterProp = new ControllGeneralProperty(parameterInfo.ParameterType,
                    defaultValue, GetParameterName(parameterInfo));

                var index = parameterIndex;
                parameterProp.EventChange += o => CurrentConstructorParams.Params[index] = o;

                parametersProperties.Add(parameterProp);
            }

            _mainLayout = new ControllVerticalLayout(parametersProperties.ToArray());
        }

        private static string GetParameterName(ParameterInfo parameter)
        {
            var defAttribute = (ParameterAttribute)parameter.GetCustomAttributes(typeof(ParameterAttribute), true).FirstOrDefault();

            if (defAttribute == null)
                return parameter.Name;

            return defAttribute.Name;
        }

        private static object GetDefaulValueParameter(ParameterInfo parameter)
        {
            var defAttribute = (ParameterAttribute) parameter.GetCustomAttributes(typeof(ParameterAttribute), true).FirstOrDefault();

            if (defAttribute == null)
                return GetDefault(parameter.ParameterType);

            return defAttribute.DefaultValue;
        }

        private static object GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }

        private static bool IsIgnoreParameter(ParameterInfo param, ref object defValue)
        {
            var attr = (IgnoreParameterAttribute) param.GetCustomAttributes(typeof(IgnoreParameterAttribute), true).FirstOrDefault();
            if (attr == null)
                return false;

            defValue = attr.DefaultValue;
            return true;
        }

        public override void Draw()
        {
            _constructorPopup.Draw();
            _mainLayout.Draw();
        }
    }


    public class ObjectConstructorParams
    {
        public ConstructorInfo Constructor { get; set; }
        public List<object> Params { get; set; }

        public ObjectConstructorParams(ConstructorInfo constructor)
        {
            Constructor = constructor;
            Params = new List<object>(new object[constructor.GetParameters().Length]);
        }

        public object Create()
        {
            return Constructor.Invoke(Params.ToArray());
        }
    }
}


[AttributeUsage(AttributeTargets.Constructor)]
public sealed class ConstructorIgnoreAttribute : Attribute { }

[AttributeUsage(AttributeTargets.Constructor)]
public sealed class ConstructorNameAttribute : Attribute
{
    public ConstructorNameAttribute(string name)
    {
        Name = name;
    }

    public readonly string Name;
}

[AttributeUsage(AttributeTargets.Parameter)]
public sealed class IgnoreParameterAttribute : Attribute
{
    public readonly object DefaultValue;

    public IgnoreParameterAttribute(object defaultValue)
    {
        DefaultValue = defaultValue;
    }
}



[AttributeUsage(AttributeTargets.Parameter)]
public sealed class ParameterAttribute : Attribute
{
    public readonly string Name;
    public readonly object DefaultValue;

    public ParameterAttribute(string name, object defaultValue)
    {
        Name = name;
        DefaultValue = defaultValue;
    }
}