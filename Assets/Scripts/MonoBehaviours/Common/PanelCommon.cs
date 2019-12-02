using UnityEngine;
using DG.Tweening;

namespace OverMars
{
    public abstract class PanelCommon<T> : Singleton<T> where T : MonoBehaviour
    {
#pragma warning disable 0649

        [SerializeField] private CanvasGroup _panelGroup;

#pragma warning restore 0649

        private Sequence _panelInteractionAnimation;
        private float _animationTime = 0.25f;

        public void OpenPanel()
        {
            PrepareForAnimation();

            _panelInteractionAnimation.Insert(0, _panelGroup.DOFade(1, _animationTime));
            _panelGroup.interactable = true;
            _panelGroup.blocksRaycasts = true;
        }

        public void ClosePanel()
        {
            PrepareForAnimation();

            _panelInteractionAnimation.Insert(0, _panelGroup.DOFade(0, _animationTime));
            _panelGroup.interactable = false;
            _panelGroup.blocksRaycasts = false;
        }

        private void PrepareForAnimation()
        {
            KillSequence();
            _panelInteractionAnimation = DOTween.Sequence();

            if (!_panelGroup)
            {
                Debug.LogError("Panel group is null! Assign it in the inspector.");
            }
        }

        private void KillSequence()
        {
            if (_panelInteractionAnimation != null)
            {
                _panelInteractionAnimation.Kill();
            }
        }
    }
}
