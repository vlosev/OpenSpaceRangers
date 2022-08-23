using System.Collections.Generic;
using Patterns.MVVM;
using UI.Common;
using UnityEngine;

namespace UI.Windows
{
    public abstract class ObjectDescriptionWM : ViewModel
    {
        protected readonly List<(string name, string value)> parameters; 
        
        public string Title { get; }

        public IReadOnlyList<(string name, string value)> Parameters => parameters;

        protected ObjectDescriptionWM(string title)
        {
            this.Title = title;
        }
    }

    public class ShipObjectDescriptionVM : ObjectDescriptionWM
    {
        public ShipObjectDescriptionVM(string title) : base(title)
        {
            //TODO: read from ship information
        }
    }
    
    public class ObjectDescriptionWindow : View<ObjectDescriptionWM>
    {
        [SerializeField] private Label labelTitle;
        [SerializeField] private RectTransform content;
        [SerializeField] private Label parameterLabelPrefab;

        private readonly List<Label> labels = new();
        
        protected override void OnBindViewModel(ObjectDescriptionWM vm)
        {
            CleanupView();
            
            if (vm != null)
            {
                //TODO: локализация
                labelTitle.Init("Название", vm.Title);
                
                var parameters = vm.Parameters;
                for (int i = 0; i < parameters.Count; ++i)
                {
                    var pair = parameters[i];

                    var label = Instantiate(parameterLabelPrefab, content, false);
                    label.Init(pair.name, pair.value);
                    labels.Add(label);
                }
            }
        }

        private void CleanupView()
        {
            foreach (var label in labels)
                label.Dispose();
            
            labels.Clear();
        }
    }
}