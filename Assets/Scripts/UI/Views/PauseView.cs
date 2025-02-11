using UnityEngine;
using ViewSystem;
using ViewSystem.Implementation;
using Zenject;

public class PauseView : BasicView
{
    [SerializeField]
    private UIButton resumeButton;
    [SerializeField]
    private UIButton mainMenuButton;
    [SerializeField]
    private UIButton restartLevelButton;

    public override bool Absolute => false;

    protected override void Awake()
    {
        base.Awake();

        resumeButton.SetupButtonEvent(OnResumePerformed);
    }

    private void OnResumePerformed()
    {
        ParentStack.TryPopSafe();
    }
}