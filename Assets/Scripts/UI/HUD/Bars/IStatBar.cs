using BlueRacconGames.UI.Bars.Presentation;
using UnityEngine;

namespace BlueRacconGames.UI.Bars
{
    public interface IStatBar
    {
        int CurrentValue { get; }
        int PreviousValue {  get; }
        int MaxValue { get; }
        CanvasGroup CanvasGroup { get; }
        BaseStatBarPresentation Presentation { get; }
        void Launch(int currentValue, int maxValue);
        void OnShowed();
        void OnHided();
        void Show();
        void UpdateBar(int currentValue, int maxValue);
        void ForceUpdateBar(int currentValue, int maxValue);
        void Hide();
        void ResetBar();
    }
}