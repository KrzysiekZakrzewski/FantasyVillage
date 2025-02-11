using Audio.Manager;
using Audio.SoundsData;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class UIToogle : MonoBehaviour
{
    [SerializeField]
    private Button button;
    [SerializeField]
    private Image icon;
    [SerializeField]
    private Material offMaterial;
    [SerializeField]
    private SoundSO pressSound;

    private bool isOn;

    private AudioManager audioManager;

    [Inject]
    private void Inject(AudioManager audioManager)
    {
        this.audioManager = audioManager;
    }

    private void OnButtonClick(UnityAction action)
    {
        action.Invoke();

        ChangeButtonState();

        audioManager.Play(pressSound);
    }

    private void ForceChangeState(bool state)
    {
        isOn = state;

        icon.material = state ? null : offMaterial;
    }

    public void SetupButtonEvent(UnityAction action)
    {
        button.onClick.AddListener(() => OnButtonClick(action));
    }

    public void LoadButtonState(bool loadedValue)
    {
        ForceChangeState(loadedValue);
    }

    public void ChangeButtonState()
    {
        ForceChangeState(!isOn);
    }
}
