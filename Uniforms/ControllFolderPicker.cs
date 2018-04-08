using System;
using System.IO;
using Uniforms.Controlls;
using UnityEditor;
using UnityEngine;

namespace Uniforms
{
    public class ControllFolderPicker : Controll
    {
        public string Label { get; set; }

        private string _path;

        public string Path
        {
            get => _path;
            set
            {
                _path = value;
                if(_textField != null)
                    _textField.Text = value;
            }
        }

        public bool UseQuickPick
        {
            get => _picker.Enabled;
            set => _picker.Enabled = value;
        }

        public bool InProjectFolder { get; set; }

        public event Action<string> EventChange;

        private ControllTextField _textField;
        private ControllHorizontalLayout _mainControll;
        private ControllButton _picker;

        public ControllFolderPicker(string label, string path, bool useQuickPick) : base(null)
        {
            Path = path;
            Label = label;

            _textField = new ControllTextField(label, path);
            _textField.EventChange += _textField_EventChange;

            _picker = new ControllButton("<");
            _picker.AddLayoutOptions(GUILayout.MaxWidth(20));
            _picker.AddLayoutOptions(GUILayout.MinWidth(20));
            _picker.EventClick += PickerOnEventClick;

            var folderDotPicker = new ControllButton("...");
            folderDotPicker.AddLayoutOptions(GUILayout.MaxWidth(20));
            folderDotPicker.AddLayoutOptions(GUILayout.MinWidth(20));
            folderDotPicker.EventClick += FolderDotPickerOnEventClick;

            _mainControll = new ControllHorizontalLayout(_textField, _picker, folderDotPicker);

            UseQuickPick = useQuickPick;
        }

        private void FolderDotPickerOnEventClick()
        {
            var path = EditorUtility.OpenFolderPanel(Label, Path, "");

            if (!Directory.Exists(path))
            {
                EditorUtility.DisplayDialog("Error", "Selected folder not exist", "Ok");
                return;
            }

            if(Path == path)
                return;

            EventChange?.Invoke(path);
            Path = path;
        }

        private void PickerOnEventClick()
        {
            if(Selection.activeObject == null)
                return;

            var path = AssetDatabase.GetAssetPath(Selection.activeObject);

            if (path.Length == 0)
                return;

            if(!Directory.Exists(path))
                return;

            if(Path == path)
                return;

            EventChange?.Invoke(path);
            Path = path;
        }

        private void _textField_EventChange(string obj)
        {
            EventChange?.Invoke(obj);
            Path = obj;
        }

        public override void Draw()
        {
            _mainControll.DrawEnabled();
        }
    }
}

