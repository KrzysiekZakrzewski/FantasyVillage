using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ViewSystem.Utils;

namespace BlueRacconGames.UI.Bars.Presentation
{
    [Serializable]
    public class DotweenStatBarIconPresentation : BaseStatBarPresentation
    {
        [SerializeField]
        private float tweenDuration = 0.33f;
        [SerializeField]
        private Ease ease = Ease.OutCubic;
        [SerializeField]
        private CanvasGroup canvasGroup;
        [SerializeField]
        private GameObject iconPrefab;
        [SerializeField]
        private float spawnWaitDuration;

        private List<Image> iconsLUT = new();

        private Sequence sequence;

        public override event Action<IStatBarPresentation> OnShowPresentationComplete;
        public override event Action<IStatBarPresentation, IStatBar> OnUpdatePresentationComplete;
        public override event Action<IStatBarPresentation> OnHidePresentationComplete;
        public override event Action<IStatBarPresentation> OnLaunchComplete;

        public override void Launch(IStatBar statBar)
        {
            SpawnIcons(statBar.MaxValue);
        }

        public override void PlayShowPresentation(IStatBar statBar)
        {
            PrepareSequence();
            sequence = GetShowSequence(statBar);
            sequence.onComplete += () =>
            {
                OnShowPresentationComplete?.Invoke(this);
            };
            sequence.SetUpdate(true);
        }

        public override void PlayUpdatePresentation(IStatBar statBar)
        {
            bool state = statBar.PreviousValue < statBar.CurrentValue;

            if (state)
                IncreasePresentation(statBar);
            else
                DecreasePresentation(statBar);
        }

        public override void PlayHidePresentation(IStatBar statBar)
        {
            PrepareSequence();
            sequence = GetHideSequence(statBar);
            sequence.onComplete += () =>
            {
                OnHidePresentationComplete?.Invoke(this);
            };
            sequence.SetUpdate(true);
        }
        public override void ForceHidePresentationComplete()
        {
            sequence?.Kill();
            OnHidePresentationComplete?.Invoke(this);
        }
        public override void ForceUpdate(IStatBar statBar)
        {
            OnUpdatePresentationComplete?.Invoke(this, statBar);
        }

        public override void ResetPresentation(IStatBar statBar)
        {
            if (iconsLUT == null) return;

            for(int i = 0; i < iconsLUT.Count; i++)
            {
                Destroy(iconsLUT[i].gameObject);
            }

            iconsLUT.Clear();
        }

        protected virtual Sequence GetShowSequence(IStatBar statBar)
        {
            return DotweenViewAnimationUtil.FadeIn(canvasGroup, ease, tweenDuration);
        }
        protected virtual Sequence GetHideSequence(IStatBar statBar)
        {
            return DotweenViewAnimationUtil.FadeOut(canvasGroup, ease, tweenDuration);
        }

        private void IncreasePresentation(IStatBar statBar)
        {
            if (statBar.MaxValue > iconsLUT.Count)
            {
                for (int i = statBar.CurrentValue; i < iconsLUT.Count; i++)
                    OnIcon(i);

                SpawnIcons(statBar.MaxValue - iconsLUT.Count);
                return;
            }

            for(int i = statBar.PreviousValue; i <= statBar.CurrentValue; i++)
                OnIcon(i);
        }

        private void DecreasePresentation(IStatBar statBar)
        {
            if (statBar.CurrentValue <= 0) return;

            for (int i = statBar.PreviousValue; i >= statBar.CurrentValue; i--)
                OffIcon(i - 1);
        }

        private void PrepareSequence()
        {
            sequence?.Kill();
        }

        private void OnIcon(int iconId)
        {
            ChangeIconState(iconId, true);
        }

        private void OffIcon(int iconId)
        {
            ChangeIconState(iconId, false);
        }

        private void ChangeIconState(int iconId, bool state)
        {
            iconsLUT[iconId].gameObject.SetActive(state);
        }

        private void SpawnIcons(int iconsAmount)
        {
            StartCoroutine(SpawnIconsSequence(iconsAmount));
        }

        private IEnumerator SpawnIconsSequence(int iconsAmount)
        {
            for (int i = 0; i < iconsAmount; i++) 
            {
                GameObject gm = Instantiate(iconPrefab, canvasGroup.transform);

                iconsLUT.Add(gm.GetComponent<Image>());

                yield return new WaitForSeconds(spawnWaitDuration);
            }

            OnLaunchComplete?.Invoke(this);
        }
    }
}