using Common;
using Common.Unity;
using UnityEngine;

namespace UI.Common
{
    /// <summary>
    /// базовый лейбл для отображения текста
    /// </summary>
    public class Label : NotifiableMonoBehaviour
    {
        [SerializeField] private Color colorTitle = Color.blue;
        [SerializeField] private Color colorValue = Color.yellow;
        [SerializeField] private string separator = ":";

        [SerializeField] private TMPro.TextMeshProUGUI text;

        public void Init(string title, string value)
        {
        }

        protected override void OnDispose()
        {
            Destroy(gameObject);
        }
    }
}