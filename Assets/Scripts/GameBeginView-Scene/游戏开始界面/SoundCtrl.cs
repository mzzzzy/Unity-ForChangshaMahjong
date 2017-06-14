using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundCtrl  {

	// 哈希表
	private Hashtable soudHash = new Hashtable() ;

	// 单例
	private static SoundCtrl _instant ;
	public static SoundCtrl getInstance()
	{
		if (_instant == null) 
		{
			_instant = new SoundCtrl ();
			audioSouce = GameObject.FindWithTag("SoundManager").GetComponent<AudioSource>() ;
		}
		return _instant;
	}

	// 声音播放组件
	private static AudioSource audioSouce ;

	// 找到挂在声音组件的空物体
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
			Debug.Log (audioSouce);
			audioSouce.clip = temp;
			audioSouce.Play ();
		}

	}
}