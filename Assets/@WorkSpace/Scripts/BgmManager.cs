using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{

    public class BgmManager : MonoBehaviour
    {

        public enum BGM
        {
            Nomal       = 0,
            PlayerFatal = 1,
        }

        [SerializeField]
        AudioSource bgmAudioSource;
        [SerializeField]
        AudioClip[] bgmClips;

        static BgmManager instance;

        void Awake()
        {
            if(instance != null)
            {
                Debug.LogError("BgmManager already exists");
                this.enabled = false;
                Destroy(this);
                return;
            }

            instance = this;
        }

        // BGM変更
        public static void ChangeBGM(BGM bgm)
        {
            instance._ChangeBGM(bgm);
        }

        void _ChangeBGM(BGM bgm)
        {
            switch (bgm)
            {
                case BGM.PlayerFatal:
                    bgmAudioSource.pitch = 1.3f;
                    Debug.Log("BGM pitch change");
                    break;
                default:
                    break;
            }
        }

    }

}