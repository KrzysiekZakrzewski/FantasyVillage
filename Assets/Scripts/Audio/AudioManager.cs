using Audio.SoundsData;
using System;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;
using System.Collections;
using DG.Tweening;

namespace Audio.Manager
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField]
        [Range(0f, 1f)]
        private float masterVolume = 1f;

        [SerializeField]
        private SoundCollectionSO musicCollectionSO;

        [SerializeField]
        private AudioMixer audioMixer;

        [SerializeField]
        private AudioMixerGroup musicMixerGroup;
        [SerializeField]
        private AudioMixerGroup sfxMixerGroup;

        private AudioSource currentMusic;

        private readonly float musicFader = 5f;

        float maxLevel = 0f;
        float minLevel = -80f;

        private readonly string musicVolumeKey = "Music";
        private readonly string sfxVolumeKey = "SFX";

        private IEnumerator WaitForNextMusic(float duration)
        {
            yield return new WaitForSeconds(duration);

            PlayRandomMusic();
        }

        private void SoundToPlay(SoundSO soundDataSO)
        {
            if (soundDataSO == null)
                return;

            AudioClip clip = soundDataSO.Clip;
            float volume = soundDataSO.Volume * masterVolume;
            float pitch = soundDataSO.Pitch;
            bool loop = soundDataSO.Loop;

            pitch = RandomizePitch(soundDataSO, pitch);

            AudioMixerGroup audioMixerGroup = GetAudioMixerGroup(soundDataSO.AudioType);

            PlaySound(clip, volume, pitch, loop, audioMixerGroup);
        }

        private float RandomizePitch(SoundSO soundDataSO, float pitch)
        {
            if (soundDataSO.RandomizePitch)
            {
                float randomPitchModifier = Random.Range(
                    -soundDataSO.RandomPitchRangeModifier,
                    soundDataSO.RandomPitchRangeModifier);

                pitch = soundDataSO.Pitch + randomPitchModifier;
            }

            return pitch;
        }

        private void PlaySound(AudioClip clip, float volume, float pitch, bool loop, AudioMixerGroup audioMixerGroup)
        {
            GameObject soundObject = new GameObject("TempAudioSource");

            AudioSource audioSource = soundObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.pitch = pitch;
            audioSource.loop = loop;
            audioSource.outputAudioMixerGroup = audioMixerGroup;

            if (DeterminMusic(audioMixerGroup, audioSource, volume, clip.length))
                return;

            audioSource.Play();

            if (!loop || !musicMixerGroup)
                Destroy(soundObject, clip.length);
        }

        private bool DeterminMusic(AudioMixerGroup audioMixerGroup, AudioSource audioSource, float volume, float lenght)
        {
            if (audioMixerGroup != musicMixerGroup)
                return false;

            if (currentMusic != null)
                currentMusic.DOFade(0, 1f).OnComplete(() =>
                {
                    Destroy(currentMusic.gameObject);

                    currentMusic = audioSource;

                    currentMusic.volume = 0f;

                    currentMusic.Play();

                    currentMusic.DOFade(volume, 1f);
                });
            else
            {
                currentMusic = audioSource;

                currentMusic.volume = 0f;

                currentMusic.Play();

                currentMusic.DOFade(volume, 1f);
            }
            StartCoroutine(WaitForNextMusic(lenght - musicFader));

            return true;
        }

        private AudioMixerGroup GetAudioMixerGroup(AudioTypes audioType)
        {
            switch(audioType)
            {
                case AudioTypes.SFX:
                    return sfxMixerGroup;
                case AudioTypes.Music: 
                    return musicMixerGroup;
                default: 
                    return null;
            }
        }

        private string GetAudioKeyName(AudioTypes audioType)
        {
            switch (audioType)
            {
                case AudioTypes.SFX:
                    return sfxVolumeKey;
                case AudioTypes.Music:
                    return musicVolumeKey;
                default:
                    return null;
            }
        }

        public void PlayRandomMusic()
        {
            if (musicCollectionSO == null)
                return;

            SoundToPlay(musicCollectionSO.GetRandomMusic());
        }

        public bool SetSoundGroupMuted(AudioTypes audioType)
        {
            var key = GetAudioKeyName(audioType);

            audioMixer.GetFloat(key, out float volume);

            volume = volume == minLevel ? maxLevel : minLevel;

            audioMixer.SetFloat(key, volume);

            return volume != minLevel;
        }

        public void SetSoundGroupMuted(AudioTypes audioType, bool isOn)
        {
            var key = GetAudioKeyName(audioType);

            float volume = isOn ? maxLevel : minLevel;

            audioMixer.SetFloat(key, volume);
        }

        public void Play(SoundSO soundDataSO)
        {
            SoundToPlay(soundDataSO);
        }
    }
}