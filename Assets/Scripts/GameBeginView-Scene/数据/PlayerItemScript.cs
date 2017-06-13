using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class PlayerItemScript : MonoBehaviour {

	// 玩家发出信息展示时间
	private float showTime ;
	// 展示聊天信息的text
	public Text chatMessage;

	public GameObject chatPaoPao;

	void Awake()
	{
		// 玩家没发消息时 不显示聊天气泡
		chatPaoPao.SetActive(false) ;

	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void showChatMessage(int index)
	{
		SoundCtrl.getInstance ().playMessageBoxSound (index);
		showTime = 2.5f;
		index = index - 1001;

		// 给消息内容给聊天面板text
		chatMessage.text = GlobalDataScript.getInstant().messageBoxcontent[index] ;



		// 这里把聊天气泡显示出来
		chatPaoPao.SetActive(true) ;
		// 2.5s后聊天气泡消失s
		Invoke("hideChatPaoPao",showTime) ;


	}

	public void hideChatPaoPao()
	{
		chatPaoPao.SetActive(false) ;

	}
}