using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;



public class PlayerItemScript : MonoBehaviour {

	public static PlayerItemScript instant ;

	// 声明停止背景音乐委托
	public StopBGMMusic stopBGMDelege ;

	public AudioSource BGMAudio ;


	// 玩家发出信息展示时间
	private float showTime ;
	// 展示聊天信息的text
	public Text chatMessage;

	public GameObject chatPaoPao;

	void Awake()
	{
		// 玩家没发消息时 不显示聊天气泡
		chatPaoPao.SetActive(false) ;

		instant = this;
		BGMAudio = GameObject.FindWithTag("BGMaudioManage").GetComponent<AudioSource>() ;

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

		// 执行委托事件 停止bgm
		stopBGMDelege ();


		showTime = 2.5f;
		index = index - 1001;

		// 给消息内容给聊天面板text
		chatMessage.text = GlobalDataScript.getInstant().messageBoxcontent[index] ;



		// 这里把聊天气泡显示出来
		chatPaoPao.SetActive(true) ;
		// 2.5s后聊天气泡消失s
		Invoke("hideChatPaoPao",showTime) ;

		// 2s后 背景音乐重新开始
		Invoke("bgmBegin",3.5f) ;


	}

	public void hideChatPaoPao()
	{
		chatPaoPao.SetActive(false) ;

	}

	public void bgmBegin()
	{

		BGMAudio.mute = false;
	}



}