using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems ;
using System ;
using UnityEngine.UI ;

public class TimeCurrent : MonoBehaviour {

	// 获取实时时间 

	public Text text ;

	public char[] ch =  new char[1] ;



	void Awake()
	{
		ch[0] = ' ' ;

	}

	// Use this for initialization
	void Start () {
	   
	}
	
	// Update is called once per frame
	void Update () {
		


		string timeCurrent = DateTime.Now.ToString ();
		string[] arr = timeCurrent.Split (ch);

		text.text = arr[1] ;
		Debug.Log (arr[1]);
	}
}
