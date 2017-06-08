using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;
using Mono.Data.Sqlite;
using UnityEngine.SceneManagement ;

public class LoginBg : MonoBehaviour {

	public GameObject registere_bg ;
	public GameObject LoadWaitingView ;

	#region
	public InputField nameField;
	public InputField pwdField;

	/// <summary>
	/// 正则表达式邮箱验证规则
	/// </summary>
	public  string regularEmail = "[\\w!#$%&'*+/=?^_`{|}~-]+(?:\\.[\\w!#$%&'*+/=?^_`{|}~-]+)*@(?:[\\w](?:[\\w-]*[\\w])?\\.)+[\\w](?:[\\w-]*[\\w])?";

	/// <summary>
	/// 正则表达式手机验证规则 13是13开头 然后后面是0-9任意数字 一共除了13后面还有9位数字
	/// </summary>
	public  string regularPhone = "13[0-9]{9}";

	// 创建数据库的路径以及名字
	public string path = "/SqlData/user.db" ;

	#endregion

	void Awake()
	{
		gameObject.SetActive (false);
		//registere_bg = GameObject.FindWithTag ("registereBtn");

		LoadWaitingView = GameObject.FindWithTag ("LoadWaitingView");
		LoadWaitingView.SetActive (false);

	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// 登入界面注册按钮的回调

	public void RegisBtn()
	{

		//gameObject.SetActive (false);
		registere_bg.SetActive (true);



	}

	// 登入按钮的回调

	public void LoginBtn()
	{ 
		
		string isLegal = RegistereBg.getInstance().BeginRegularPlayer (nameField.text,regularPhone,regularEmail) ;
		if (isLegal == "isPhone" || isLegal == "isEmail") {
			Debug.Log ("账号输入合法");

		} else 
		{

			Debug.Log ("不合法 重新输入 ");
			return;
		}

		// 2.
		string UserNameOne = "USER where name="+" '"+nameField.text + "'";
		int count = SqliteMangeToCSharp.GetInstance ().selectTableDataCondition (UserNameOne,path);

		// 账号存在 去验证密码
		if (count > 0) 
		{
			// 查找是否有这个账号 且密码是否一致
			string UserName = "USER where name="+" '"+nameField.text+"'";
			Debug.Log (count);
			SqliteDataReader reader = SqliteMangeToCSharp.GetInstance().selectTaleDataAll (UserName,path);
			Debug.Log (count);
			while (reader.Read ()) {


				string name = nameField.text;
				string psw = pwdField.text;

				if (name == reader.GetString (1) && psw == reader.GetString (2)) {

					Debug.Log ("登入成功");

					gameObject.SetActive (false);
					LoadWaitingView.SetActive (true);
					Invoke ("LoadGamesLobby",2.0f);



				} else {

					Debug.Log ("密码错误");
				}


			}


		}
		else {

			Debug.Log ("账号未注册");

		}
		}

	 
	// 登入成功后 2s后加载另一场景

	public void LoadGamesLobby()
	{
		LoadWaitingView.SetActive (false);
		SceneManager.LoadScene (1);

	}








}
