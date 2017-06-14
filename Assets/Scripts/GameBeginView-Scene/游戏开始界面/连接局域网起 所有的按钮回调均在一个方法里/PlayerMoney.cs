using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class PlayerMoney : MonoBehaviour {

	// 这是设置玩家初始金钱1000 image头像
	// 此脚本挂在显示玩家金钱text上

	public Image headImage ;
	private Text text ;

	// 给头像提供图片名称
	private List<string> imageNameList = new List<string>() ;

	void Awake()
	{
		text = gameObject.GetComponent<Text> ();
		addImageName ();


	}

	private void addImageName()
	{
		imageNameList.Add ("WechatIMG24");
		imageNameList.Add ("WechatIMG22");
		imageNameList.Add ("WechatIMG21");
		imageNameList.Add ("WechatIMG23");


	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// 设置金钱接口
	public void SetPlayerMoney(int currenMoney)
	{
		text.text = currenMoney.ToString ();
	}

	// 设置头像

	public void SetHeadImage(int headIndex)
	{
		switch (headIndex) 
		{
		case 0:
			headImage.GetComponent<Image> ().sprite = Resources.Load<Sprite> (imageNameList [0]);
			break;
		case 1:
			headImage.GetComponent<Image> ().sprite = Resources.Load<Sprite> (imageNameList [1]);
			break;
		case 2:	
			headImage.GetComponent<Image> ().sprite = Resources.Load<Sprite> (imageNameList [2]);
			break;
		case 3:
			headImage.GetComponent<Image> ().sprite = Resources.Load<Sprite> (imageNameList [3]);
			break;
		default :
			headImage.GetComponent<Image> ().sprite = Resources.Load<Sprite> (imageNameList [0]);
			break;
		}

	}


}
