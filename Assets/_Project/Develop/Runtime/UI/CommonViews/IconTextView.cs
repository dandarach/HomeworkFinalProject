using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Assets._Project.Develop.Runtime.UI.Core;

namespace Assets._Project.Develop.Runtime.UI.CommonViews
{
    public class IconTextView : MonoBehaviour, IView
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _icon;

        public void SetText(string text) => _text.text = text;

        public void SetIcon(Sprite icon) => _icon.sprite = icon;
    }
}
