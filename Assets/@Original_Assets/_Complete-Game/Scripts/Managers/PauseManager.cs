﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseManager : MonoBehaviour {
	
	public AudioMixerSnapshot paused;
	public AudioMixerSnapshot unpaused;
	
	Canvas canvas;
    	
	void Start()
	{
		canvas = GetComponent<Canvas>();
        Managers.InputManager.InvokeContentPause += SwapPause;
    }
	
	void Update()
	{
		//if (Input.GetKeyDown(KeyCode.Escape))
		//{
		//	canvas.enabled = !canvas.enabled;
		//	Pause();
		//}
	}

    private void SwapPause()
    {
        canvas.enabled = !canvas.enabled;
        Pause();
    }

    public void Pause()
	{
		Time.timeScale = Time.timeScale == 0 ? 1 : 0;
		Lowpass ();
		
	}
	
	void Lowpass()
	{
		if (Time.timeScale == 0)
		{
            //paused.TransitionTo(.01f);
            CompleteProject.BgmManager.ChangeBGM(CompleteProject.BGM.Paused);
        }

        else
			
		{
            //unpaused.TransitionTo(.01f);
            CompleteProject.BgmManager.ChangeBGM(CompleteProject.BGM.UnPaused);
        }
    }
	
	public void Quit()
	{
		#if UNITY_EDITOR 
		EditorApplication.isPlaying = false;
		#else 
		Application.Quit();
		#endif
	}
}
