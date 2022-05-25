using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using MatchLab.Singleton;
using UniRx;
using UniRx.Triggers;

namespace MatchLab.Audio.BGM
{
    [RequireComponent(typeof(AudioSource))]
    public class BGMManager : SingletonMonoBehaviour<BGMManager>
    {
        [SerializeField]
        protected AudioSource _audioSource;

        protected Dictionary<string, AudioClip> _audioClipByName = new Dictionary<string, AudioClip>();

        protected override void OnAwake()
        {
            AudioSettingController.Instance.Setup();

            this.OnDestroyAsObservable()
                .Subscribe(x => { _audioClipByName.Clear(); })
                .AddTo(this);
        }

        protected override void OnReset()
        {
            gameObject.TryGetComponent(out _audioSource);
        }

        private void OnDestroy()
        {
            _audioClipByName.Clear();
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

            _audioSource.clip = clip;
            _audioSource.Play();
        }

        public void Stop(bool release = true)
        {
            _audioSource.Stop();

            if (!release) return;

            Addressables.Release(_audioSource.clip);
            _audioSource.clip = null;
        }
    }
}
