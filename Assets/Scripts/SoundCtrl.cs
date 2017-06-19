using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 点击系统自定义消息 停止背景音乐委托
public delegate void  StopBGMMusic() ;
public class SoundCtrl  {



	// 哈希表
	private Hashtable soudHash = new Hashtable() ;
	// 声音播放组件 - 控制系统定义的声音 比如 快点出 我等到花儿都谢了
	private static AudioSource audioSouce ;
	// 播放背景音乐
	public static AudioSource audioBGM ;
	// 单例
	private static SoundCtrl _instant ;
	public static SoundCtrl getInstance()
	{
		if (_instant == null) 
		{
			_instant = new SoundCtrl ();
			audioSouce = GameObject.FindWithTag("SoundManager").GetComponent<AudioSource>() ;
			audioBGM = GameObject.FindWithTag("BGMaudioManage").GetComponent<AudioSource>() ;
		}
		return _instant;
	}



	// 找到挂在声音组件的空物体 - 控制系统定义的声音 比如 快点出 我等到花儿都谢了
	public GameObject soundManage ;




	void Awake()
	{
		// 注意查找组件不一定要放在Awake方法里面
		// 比如下面语句 我放在16行下面也行


	}




	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	// 系统聊天消息声音 如：快点出 我等到花儿都谢了
	public void playMessageBoxSound(int codeIndex)
	{

		// 绑定事件 
		PlayerItemScript.instant.stopBGMDelege = StopBGM ;


		if (GlobalDataScript.getInstant().soundToggle) 
		{
			Debug.Log (codeIndex);
			// 声音资源所在的目录
			string path = "Sounds/other/"+codeIndex.ToString();

			AudioClip temp = (AudioClip)soudHash [path];
			if (temp == null) {

				 temp = GameObject.Instantiate(Resources.Load (path)) as AudioClip;
				soudHash.Add (path, temp);
			}	

			audioSouce.clip = temp;

			audioSouce.Play ();
		}

	}

	// 游戏背景音乐 一直播放

	public void playBGM()
	{
		string path = "Sounds/mjBGM" ;
		AudioClip temp = (AudioClip)soudHash[path] ;

		if (temp == null)
		{
			temp = GameObject.Instantiate(Resources.Load (path)) as AudioClip;
			soudHash.Add (path,temp);
		     
		}

		audioBGM.clip = temp;
		audioBGM.loop = true;
		audioBGM.Play ();
	}

    public void StopBGM()
	{
		audioBGM.mute = true;

	}







}