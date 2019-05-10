using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Managers
{
    public class ContentManager : MonoBehaviour
    {
        public enum Status
        {
            Normal,
            Boss
        };
        public static Status status;

        public static Action<CompleteProject.EnemyManager.Status> ChangeEnemyStatus;

        private void Awake()
        {
            status = Status.Normal;
        }

        private void Start()
        {
            StartCoroutine(SetupEvents());
            StartCoroutine(StatusCheck(0.5f));
        }

        private IEnumerator SetupEvents()
        {
            yield return new WaitForSeconds(0.5f);

            // do something

            yield break;
        }

        private IEnumerator StatusCheck(float time = 1f)
        {
            while(true)
            {
                if (!status.Equals(Status.Boss) && CompleteProject.ScoreManager.score >= 100)
                {
                    status = Status.Boss;

                    ChangeEnemyStatus(CompleteProject.EnemyManager.Status.Boss);
                    CompleteProject.BgmManager.ChangeBGM(CompleteProject.BGM.Boss);
                }

                yield return new WaitForSeconds(time);
            }
        }

    }
}
