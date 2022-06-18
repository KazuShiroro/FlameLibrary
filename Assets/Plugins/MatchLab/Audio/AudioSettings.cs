using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Audio;
using UniRx;
using UniRx.Triggers;

namespace Flame.Audio
{
    [CreateAssetMenu(menuName = "MatchLabLib/Audio/AudioSettings")]
    public class AudioSettings : ScriptableObject
    {
        [SerializeField]
        protected AudioMixer _audioMixer;

        [SerializeField]
        protected AudioGroupProperties _masterVolume;
        public AudioGroupProperties MasterVolume { get => _masterVolume; }

        [SerializeField]
        protected AudioGroupProperties _bgmVolume;
        public AudioGroupProperties BgmVolume { get => _bgmVolume; }

        [SerializeField]
        protected AudioGroupProperties _seVolume;
        public AudioGroupProperties SeVolume { get => _seVolume; }

        private void OnDisable()
        {
            DisposeVolumeEvent(_masterVolume);
            DisposeVolumeEvent(_bgmVolume);
            DisposeVolumeEvent(_seVolume);
        }

        static public float ConvertVolume2db(float volume)
        {
            volume = Mathf.Clamp(volume, 0f, 1f);
            return Mathf.Clamp(20f * Mathf.Log10(volume), -80, 0f);
        }

        public void Setup()
        {
            SubscribeVolumeEvent(_masterVolume);
            SubscribeVolumeEvent(_bgmVolume);
            SubscribeVolumeEvent(_seVolume);

            _masterVolume.Set(_masterVolume.Get());
            _bgmVolume.Set(_bgmVolume.Get());
            _seVolume.Set(_seVolume.Get());
        }

        private void SubscribeVolumeEvent(AudioGroupProperties properties)
        {
            properties.Volume
                .Subscribe(v =>
                {
                    _audioMixer.SetFloat(properties.Name, ConvertVolume2db(v));
                    Debug.Log($"_audioMixer.SetFloat({properties.Name},{v.ToString("0.0")})");
                });
        }

        private void DisposeVolumeEvent(AudioGroupProperties properties)
        {
            properties.Dispose();
        }
    }

    [Serializable]
    public class AudioGroupProperties : ISerializationCallbackReceiver, IDisposable
    {
        [SerializeField]
        protected string _name;
        public string Name { get => _name; }

        [SerializeField, Range(0f, 1f)]
        protected float _volume = 1f;
        protected ReactiveProperty<float> _volumeProperty = new ReactiveProperty<float>();
        public IReadOnlyReactiveProperty<float> Volume { get => _volumeProperty; }

        public float Get() => _volume;
        public void Set(float v) => _volumeProperty.Value = _volume = v;

        public void OnBeforeSerialize()
        {
            Set(_volume);
        }

        public void OnAfterDeserialize()
        {
        }

        public void Dispose()
        {
            _volumeProperty.Dispose();
        }
    }
}
