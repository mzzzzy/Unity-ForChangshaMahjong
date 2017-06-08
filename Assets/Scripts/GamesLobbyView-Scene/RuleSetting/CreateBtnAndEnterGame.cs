using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBtnAndEnterGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	// 创建按钮回调方法

	public void CreatGame()
	{
		// 进入游戏开始界面


		if (NumberOfMJ.instant.isChoose && NumberOfPerson.instant.isChoose && PlayScore._instant.isChoose) {
		    
			Debug.Log ("创建游戏成功 进入游戏开始界面");
		} else
		{
			Debug.Log (PlayScore._instant.isChoose);
			Debug.Log ("游戏不是4局或者游戏人数不是4人");
		}

		//Debug.Log ("创建游戏成功 进入游戏开始界面");
	}
}
