using System;
using UnityEngine;
using UnityEngine.UI;
using Assets._Project.Develop.Runtime.UI.Core;
using TMPro;
using DG.Tweening;

namespace Assets._Project.Develop.Runtime.UI.Gameplay.Popups
{
    public class GameplayPopupView : PopupViewBase
    {
        [SerializeField] private TMP_Text _message;
        [SerializeField] private Button _closeButton;

        public void SetTitle(string title) => _message.text = title;
        public void SetMessageText(string title) => _message.text = title;

        protected override void ModifyShowAnimation(Sequence animation)
        {
            base.ModifyShowAnimation(animation);

            //animation.Append(_closeButton.DO);
            //animation.AppendInterval(0.1f);
        }
    }
}
