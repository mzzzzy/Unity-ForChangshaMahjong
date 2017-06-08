using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
public class Example : MonoBehaviour {
	private string str = "result";
    //分享本地纹理
    public Texture2D texture;
	void Start () {
        //设置Umeng Appkey
	}

	void Update ()
	{
		//点击手机返回键关闭应用程序
		if (Input.GetKeyDown(KeyCode.Escape)  )
		{
			Application.Quit();
		}
	}
	void onShareback(string result){
		str = result;
		Debug.Log("onShareback");
	}
    void OnGUI()
    {
		int startheight = 200;
		int heightspace = 200;

		GUIStyle labelstyle=new GUIStyle();
		labelstyle.normal.background = null;    
		labelstyle.normal.textColor=new Color(1,1,1);  
		labelstyle.fontSize = 120;      
		GUI.Label(new Rect(Screen.width/2-20,0,Screen.width,30),"U-Share",labelstyle);  
		GUIStyle resultstyle=new GUIStyle();
		resultstyle.normal.background = null;  
		resultstyle.normal.textColor=new Color(1,1,1);   
		resultstyle.fontSize = 40;     
		GUI.Label(new Rect(0,100,Screen.width,60),str,resultstyle);  
		GUIStyle buttonstyle=new GUIStyle();
		buttonstyle.normal.background = null;    
		buttonstyle.normal.textColor=new Color(1,1,1);  
		buttonstyle.fontSize = 80;      
		
		Social.ShareBoardDismissDelegate dismisscallback =
		                delegate()
		                {
			Debug.Log("xxxxxx  dismiss"); 
		                };
		Social.ShareDelegate sharecallback =
			delegate(Platform platform, int stCode, string errorMsg)
		{
			if (stCode == Social.SUCCESS)
			{
				Debug.Log("xxxxxx  share success");
				str = "success";
				Debug.Log("直接分享");
			}
			else
			{

				str = "fail"+errorMsg;
			};
		};
		Social.AuthDelegate authcallback =
			delegate(Platform platform, int stCode,Dictionary<string,string> message)
		                {
		                    if (stCode == Social.SUCCESS)
		                    {
				Debug.Log("xxxxxx message="+message.Count);
				str = "success";
				foreach (KeyValuePair<string, string> kv in message)  
				{  
					string n = kv.Key;  
					string s = kv.Value;  
					str = str+"   "+n+":"+s;
				} 

			
		                    }
		                    else
		                    {
				str = "fail=";
				foreach (KeyValuePair<string, string> kv in message)  
				{  
					string n = kv.Key;  
					string s = kv.Value;  
					str = str+"   "+n+":"+s;
				} 
		                    };
		                };

		if (GUI.Button (new Rect (0, startheight+heightspace*2, Screen.width/2, heightspace), "微信登录", buttonstyle)) {
			Social.Authorize (Platform.WEIXIN, authcallback);

		}	
		if (GUI.Button (new Rect (Screen.width/2, startheight+heightspace*2, Screen.width/2, heightspace), "微信分享", buttonstyle)) {
			Application.CaptureScreenshot("Sceenshot.png");
			Social.DirectShare(Platform.WEIXIN, "Hello World", Application.persistentDataPath + "/Sceenshot.png","","", sharecallback);

		}
	

		if (GUI.Button (new Rect (Screen.width/2-30, startheight+heightspace*5, Screen.width, heightspace), "打开分享面板", buttonstyle)) {
			Platform[] platforms = { Platform.QQ,Platform.QZONE,Platform.SINA,Platform.WEIXIN,Platform.WEIXIN_CIRCLE};
			Social.setDismissDelegate (dismisscallback);
			Social.OpenShareWithImagePath (platforms,"Hello World", Application.persistentDataPath + "/Sceenshot.png","umeng","http://www.umeng.com/", sharecallback);
		}
//		GUI.color = Color.blue;  
//		GUI.backgroundColor = Color.black; 
//        if (GUI.Button(new Rect(150, 300, 500, 100), "授权111"))
//        {
//            //接入QQ
//            Social.SetQQAppIdAndAppKey("100424468", "c7394704798a158208a74ab60104f0ba");
//            //授权
//            Social.Authorize(Platform.QQ, delegate(Platform platform, int stCode, string usid, string token)
//            {
//                if (stCode == Social.SUCCESS)
//                {
//                    Debug.Log("授权成功" + "usid:" + usid + "token:" + token);
//                }
//                else
//                {
//                    Debug.Log("授权失败");
//                }
//            });
//
//        }
//
//        if (GUI.Button(new Rect(150, 500, 500, 100), "直接分享"))
//        {
//            //接入QQ
//            Social.SetQQAppIdAndAppKey("100424468", "c7394704798a158208a74ab60104f0ba");
//
//            Social.ShareDelegate callback =
//                delegate(Platform platform, int stCode, string errorMsg)
//                {
//                    if (stCode == Social.SUCCESS)
//                    {
//                        Debug.Log("直接分享");
//                    }
//                    else
//                    {
//                        Debug.Log("直接分享" + errorMsg);
//                    };
//                };
//            //截屏
//            Application.CaptureScreenshot("Sceenshot.png");
//            //分享
//            Social.DirectShare(Platform.QQ, "Hello World", Application.persistentDataPath + "/Sceenshot.png", callback);
//        }
    }



}
