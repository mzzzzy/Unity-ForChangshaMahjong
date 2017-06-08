using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;





public class ToggleController : MonoBehaviour {

	/// <summary>
	/// 单例
	/// </summary>
	public static ToggleController instant ;

	[Tooltip("拿到复选框的bool值 只能为true时才能微信和游客登入")]
	public Toggle tog ;

	[Tooltip("拿到状态的bool值")]
	public bool isRead = false ; 

	void Awake()
	{
		instant = this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		isReadSuccess ();

	}

	#region

	public void isReadSuccess()
	{
		if (tog.isOn) {
			isRead = true;
		} else
		{
			isRead = false;
		}

	}

	#endregion
}
