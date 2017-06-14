using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class playerName : MonoBehaviour {




	[Tooltip("玩家名字")]
	// 此脚本挂在玩家显示名字的框上

	private Text nameText;

	void Awake()
	{
		
		nameText = gameObject.GetComponent<Text> ();

	}



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setPlayerName(string name)
	{
		nameText.text = name; 

	}
}
