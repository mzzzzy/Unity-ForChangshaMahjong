using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PlayBGM ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayBGM()
	{
		SoundCtrl.getInstance ().playBGM ();

	}


	}
