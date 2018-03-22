# Editor view framework

 Фреймворк для Unity3D для создания окон редакторов используя компоненты(например как в Windows forms)
и все прелести ООП вместо того чтобы размазывать отрисовку контролов imgui Unity3D вместе с логикой в одном месте.

 Позволяет относительно быстро делать штуки подобно этому:

```csharp
interface IExampleView
{
    event Action Click;
}

class ExampleView : Window, IExampleView
{
    [MenuItem("Example/Window")]
    public static void CreateWindow() { GetWindow<ExampleView>(); }

    public event Action Click;

    public override void PostInit()
    {
        var label = new ControllLabel("Text");
        var button = new ControllButton("click me!");
        button.EventClick += () =>
        {
            if (Click != null) Click();
        };

        MainControll = new ControllVerticalLayout(label, button);
    }
}

class ExampleController
{
    public IExampleView View { get; private set; }

    public ExampleController(IExampleView view)
    {
        View = view;
        view.Click += () => EditorUtility.DisplayDialog("click", "click!!", "ok");
    }
}

class ExampleFactory
{
    [MenuItem("Example/Window")]
    public static void CreateWindow()
    {
        var view = EditorWindow.GetWindow<ExampleView>();
        var controller = new ExampleController(view);
    }
}
```

Резельтат:
![Пример окна](https://raw.githubusercontent.com/CTAPbIuKODEP/EditorViewFramework/master/docs/exampleview.png)