using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        public static Action InvokeContentPause;
        public static Action InvokePlayerFire;
                                
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                InvokeContentPause();
            }

            if (Input.GetButton("Fire1"))
            {
                InvokePlayerFire();
            }
        }
    }

}
