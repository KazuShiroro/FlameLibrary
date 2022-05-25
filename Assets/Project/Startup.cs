using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.AddressableAssets;
using MatchLab.Audio;

namespace Ex.Unity2019.Common
{
    public class Startup
    {
        //[RuntimeInitializeOnLoadMethod]
        static void Start()
        {
            var audioMixer = Addressables.LoadAssetAsync<AudioMixer>("SimpleAudioMixer").WaitForCompletion();
            audioMixer.SetFloat("Master", 1f);
            audioMixer.SetFloat("BGM", 0.1f);
            audioMixer.SetFloat("SE", 0.5f);
        }
    }
}
