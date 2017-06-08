using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;
using System ;

public class PlayScore : MonoBehaviour {

	public static PlayScore _instant;

	/*
	public static  PlayScore GetInstant()
	{
		if (_instant == null)
		{

			_instant = new PlayScore ();

		}
		return _instant;

	}
	private  PlayScore()
	{
		
	}
	*/

	// 庄家得分必须要勾选

	public Toggle tog;

	public bool isChoose = false ;


	void Awake()
	{
		_instant = this ;

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (tog.isOn);
		if (tog.isOn)
		{
			Debug.Log ("as");
			isChoose = true;
		
		} else 
		{
			isChoose = false;
		}

	}
}
