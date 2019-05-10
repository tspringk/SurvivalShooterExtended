using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace CompleteProject
{

    public enum BGM
    {
        Nomal,
        Paused,
        UnPaused,
        PlayerFatal,
    }

    public class BgmManager : MonoBehaviour
    {

        [SerializeField]
        AudioSource bgmAudioSource;
        [SerializeField]
        AudioMixerSnapshot nomalSnap;
        [SerializeField]
        AudioMixerSnapshot pauseSnap;
        [SerializeField]
        AudioMixerSnapshot fatalDamagedSnap;

        static BgmManager instance;

        AudioMixerSnapshot currentBgmSnap;

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

        void Start()
        {
            currentBgmSnap = nomalSnap;
        }

        /// <summary>
        ///  BGM変更
        /// </summary>
        /// <param name="bgm"></param>
        /// <param name="during"></param>
        public static void ChangeBGM(BGM bgm, float during = 0.01f)
        {
            instance._ChangeBGM(bgm, during);
        }

        void _ChangeBGM(BGM bgm, float during)
        {
            AudioMixerSnapshot snap;
            switch (bgm)
            {
                case BGM.Paused:
                    snap = pauseSnap;
                    break;
                case BGM.UnPaused:
                    snap = currentBgmSnap;
                    break;
                case BGM.PlayerFatal:
                    snap = fatalDamagedSnap;
                    currentBgmSnap = snap;
                    break;
                //case BGM.Nomal:
                default:
                    snap = nomalSnap;
                    currentBgmSnap = snap;
                    break;
            }
            snap.TransitionTo(during);

        }
    }

}