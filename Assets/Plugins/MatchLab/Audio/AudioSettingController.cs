using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Flame.Audio
{
    public class AudioSettingController
    {
        public static AudioSettingController Instance = new AudioSettingController();

        protected AudioSettings _audioVolume = null;

        public void Setup()
        {
            if (_audioVolume) return;

            _audioVolume = Addressables.LoadAssetAsync<AudioSettings>("SimpleAudioSettings").WaitForCompletion();

            _audioVolume.Setup();
        }
    }
}
