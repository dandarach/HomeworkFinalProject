using System;
using UnityEngine;
using UnityEngine.UI;
using Assets._Project.Develop.Runtime.UI.Core;
using TMPro;
using DG.Tweening;

namespace Assets._Project.Develop.Runtime.UI.LevelsMenuPopup
{
    public class LevelsMenuPopupView : PopupViewBase
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private LevelTilesListView _levelsListView;

        public LevelTilesListView LevelTilesListView => _levelsListView;

        public void SetTitle(string title) => _title.text = title;

        protected override void ModifyShowAnimation(Sequence animation)
        {
            base.ModifyShowAnimation(animation);

            foreach (LevelTileView levelTileView in _levelsListView.Elements)
            {
                animation.Append(levelTileView.Show());
                animation.AppendInterval(0.1f);
            }
        }
    }
}
