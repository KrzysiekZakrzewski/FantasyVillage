using UnityEngine;
using ViewSystem;
using ViewSystem.Implementation;
using Zenject;

public class PauseView : BasicView
{
    [SerializeField]
    private UIButtonBase resumeButton;
    [SerializeField]
    private UIButtonBase mainMenuButton;
    [SerializeField]
    private UIButtonBase restartLevelButton;

    public override bool Absolute => false;

    protected override void Awake()
    {
        base.Awake();

        resumeButton.OnClickE += OnResumePerformed;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        resumeButton.OnClickE -= OnResumePerformed;
    }

    private void OnResumePerformed()
    {
        ParentStack.TryPopSafe();
    }
}