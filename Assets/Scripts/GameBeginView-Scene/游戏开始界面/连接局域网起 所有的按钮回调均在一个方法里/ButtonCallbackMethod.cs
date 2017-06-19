using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ButtonCallbackMethod : MonoBehaviour {

	private NetworkClient client ;

	// 游戏开始 不必要的ui消失
	public GameObject PlayerChatBtn;
	// 东南西北出现
	public GameObject dir ;
    // 开始发牌
	public bool isBegin = false ;

	public static ButtonCallbackMethod instant;

	void Awake()
	{
		
		instant = this ;
		dir.SetActive (false);

	}

	// Use this for initialization
	void Start () 
	{
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	#region 游戏准备按钮回调方法

	public void playBtnMethod()
	{
		PlayerChatBtn.SetActive (false);

		// 开始发牌
		isBegin = true ;

		dir.SetActive (true);
	}



	#endregion



	#region  host按钮回调方法
	// host服务器按钮回调
	// 开启主机 
	public void InitHost()
	{
		// 开启监听
		NetworkServer.Listen(ButtonCallbackMethodData._instant.GetPort()) ;
		// 添加服务器监听到客服端的回调
		NetworkServer.RegisterHandler(MsgType.Connect,OnServerListenClient) ;
		// 添加服务器监听到客户端创建玩家的回调
		NetworkServer.RegisterHandler(MsgType.AddPlayer,OnServerListenAddPlayer);
		// 添加服务器监听到客户端断开连接的回调
		NetworkServer.RegisterHandler(MsgType.Disconnect,OnServerListenClientDisConnect) ;

		// 创建客服端 连接本地 从而形成主机
		client = ClientScene.ConnectLocalServer() ;
		// 客服端绑定回调
		ClientBindFunction ();

	}

	// 服务器监听客服端回调
	private void OnServerListenClient(NetworkMessage msg)
	{
		// 准备
		ClientScene.Ready(msg.conn) ;
		// 注册预设体

		// 麻将预设体？ 每个牌家的牌不一样？
		//ClientScene.RegisterPrefab() ;


		// 出牌定时器预设体？



		// 出牌光标指示器预设体？



		// 添加玩家
		ClientScene.AddPlayer(0) ;


	}

	// 服务器监听客服端创建玩家回调
	private void OnServerListenAddPlayer(NetworkMessage msg)
	{
		// 生成玩家麻将牌

	}

	// 服务器监听客服端断开连接回调
	private void  OnServerListenClientDisConnect(NetworkMessage msg)
	{
		NetworkServer.DestroyPlayersForConnection (msg.conn);
	}

	// 客服端连接回调
	private void ClientBindFunction()
	{
		client.RegisterHandler (MsgType.Connect, OnClientConnected);
	}

	private void OnClientConnected(NetworkMessage msg)
	{
		
	}

	#endregion


}
