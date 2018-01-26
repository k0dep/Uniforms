using System;
using System.Collections.Generic;
using EditorViewFramework.Controlls;

namespace EditorViewFramework
{
    public class ControllTabView : Controll
    {
        public IList<TabView> Tabs { get; }

        protected ControllToolbarButtons<TabView> _toolbarButtons;

        public ControllTabView(params TabView[] tabs) : base(null)
        {
            var buttons = new List<ControllToolbarButtons<TabView>.ToolbarButtonContent>();
            foreach (var tabView in tabs)
                buttons.Add(new ControllToolbarButtons<TabView>.ToolbarButtonContent(tabView, tabView.Label));
            _toolbarButtons = new ControllToolbarButtons<TabView>(buttons.ToArray());
            _toolbarButtons.EventChangeSelected += ToolbarButtonsOnEventChangeSelected;
            Tabs = new List<TabView>(tabs);
        }

        private void ToolbarButtonsOnEventChangeSelected(TabView view)
        {
            _toolbarButtons.Selected.FireHide();
            view.FireShow();
        }

        public ControllTabView() : this(new TabView[0])
        {
        }

        public override void Draw()
        {
            if(Tabs.Count == 0)
                return;

            _toolbarButtons.Draw();

            if(_toolbarButtons.Selected != null)
                _toolbarButtons.Selected.Draw();
        }
    }

    public class TabView
    {
        public event Action EventShow;
        public event Action EventHide;

        public string Label { get; set; }
        public Controll ContentControll { get; set; }

        public TabView(string label, Controll contentControll)
        {
            Label = label;
            ContentControll = contentControll;
        }

        public TabView(string label, Controll contentControll, Action onShow, Action onHide) : this(label, contentControll)
        {
            EventHide += onHide;
            EventShow += onShow;
        }

        public void Draw()
        {
            ContentControll.Draw();
        }

        public void FireHide()
        {
            if (EventHide != null)
                EventHide();
        }

        public void FireShow()
        {
            if (EventShow != null)
                EventShow();
        }
    }
}
