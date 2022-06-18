using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Flame.Singleton;
using UniRx;
using UniRx.Triggers;

namespace Flame.Audio.SE
{
    [RequireComponent(typeof(AudioSource))]
    public class SEManager : SingletonMonoBehaviour<SEManager>
    {
        public const string _Label_SE = "SE";

        [SerializeField]
        protected AudioSource _audioSource;

        protected Dictionary<string, AudioClip> _audioClipByName = new Dictionary<string, AudioClip>();

        protected override void OnReset()
        {
            gameObject.TryGetComponent(out _audioSource);
        }

        protected override void OnAwake()
        {
            AudioSettingController.Instance.Setup();

            LoadAll();
        }

        public void LoadAll()
        {
            // データのロード
            var loadedAudioClips = Addressables
                .LoadAssetsAsync<AudioClip>(_Label_SE, null)
                .WaitForCompletion();

            // データの保持
            foreach (var audioClip in loadedAudioClips)
            {
                _audioClipByName.Add(audioClip.name, audioClip);
            }

            // データの破棄条件登録
            this.OnDestroyAsObservable()
                .Subscribe(x => { loadedAudioClips.Clear(); Addressables.Release(loadedAudioClips); _audioClipByName.Clear(); })
                .AddTo(this);
        }

        public AudioClip Load(string name)
        {
            bool isLoaded = _audioClipByName.TryGetValue(name, out var audioClip);

            if (isLoaded) return audioClip;

            audioClip = Addressables
                .LoadAssetAsync<AudioClip>(name)
                .WaitForCompletion();

            _audioClipByName.Add(name, audioClip);

            return _audioClipByName[name];
        }

        public void Play(string name)
        {
            Play(Load(name));
        }

        public void Play(AudioClip clip)
        {
            if (clip == null) return;

            _audioSource.PlayOneShot(clip);
        }
    }
}
