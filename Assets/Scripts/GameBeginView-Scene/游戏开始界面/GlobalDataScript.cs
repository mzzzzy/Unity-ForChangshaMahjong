using UnityEngine;
using System ;
using System.Text ;
using System.Collections.Generic;
public class GlobalDataScript  {

	private static GlobalDataScript _instant ;
	public static GlobalDataScript getInstant()
	{
		if(_instant == null)
		{
			_instant = new GlobalDataScript ();
		}
		return _instant;
	}

	// 游戏自定义消息 
	public   List<String> messageBoxcontent = new List<string>();

	// 游戏声音控制 存在的意义：用户静音 就为false 就不再播放声音了
	public bool soundToggle = true ;


	// Use this for initialsization
	void Start () {

	}



	// Update is called once per frame
	void Update () {

	}

	// 游戏自定义聊天 数据
	public void initMessageBox()
	{
		messageBoxcontent.Add ("不要吵了 陈小明专心玩游戏");
		messageBoxcontent.Add ("不要走 方小茹决战到天亮");
		messageBoxcontent.Add ("大家好，我叫王小倩 很高兴见到各位");
		messageBoxcontent.Add ("刘小芳 和你合作很愉快哦");
		messageBoxcontent.Add ("廖小轩 快点啊我等到花儿都谢了");
		messageBoxcontent.Add ("黄小琼 你的牌打的也特好了");
		messageBoxcontent.Add ("下次再玩吧 刘籽繇要走了");

		Debug.Log (messageBoxcontent.Count);
	}

	// 构造方法 实例化时就调用 数组添加
	public GlobalDataScript()
	{
		init ();

	}

	void init()
	{
		initMessageBox ();
	}
}