using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class login_youke : MonoBehaviour {

	public GameObject login_bg ;

	void Awake()
	{
		login_bg = GameObject.FindWithTag ("login_bg");

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// 游客登入的回调方法

	public void youKeLogin()
	{
		if (ToggleController.instant.isRead) {
			login_bg.SetActive (true);
			gameObject.SetActive (false);
		} else
		{
			Debug.Log ("请先阅读用户条例 并勾选");
		}

	}
}
