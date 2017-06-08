using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic ;
using System ;
using UnityEngine.UI ;
using Mono.Data.Sqlite;

public class RegistereBg : MonoBehaviour {

	public static RegistereBg _instance;
	public static RegistereBg getInstance()
	{
	  	        if (_instance == null)
		      {
		 	    _instance = new RegistereBg ();
			        
		       }
		        return _instance;
    } 




	#region 验证所需属性

	/// <summary>
	/// 正则表达式邮箱验证规则
	/// </summary>
	public  string regularEmail = "[\\w!#$%&'*+/=?^_`{|}~-]+(?:\\.[\\w!#$%&'*+/=?^_`{|}~-]+)*@(?:[\\w](?:[\\w-]*[\\w])?\\.)+[\\w](?:[\\w-]*[\\w])?";

    /// <summary>
	/// 正则表达式手机验证规则 13是13开头 然后后面是0-9任意数字 一共除了13后面还有9位数字
    /// </summary>
	public  string regularPhone = "13[0-9]{9}";

	/// <summary>
	/// 正则表达式密码验证规则  要有字母与数组结合 最小8位 最大15位
	/// </summary>
	public  string regularPwd = "^[0-9A-Za-z]{6,15}$";

	[Tooltip("是不是邮箱或者手机号码 密码规则对不对")]
	public  bool userNameIsRegular = false;
	public  bool pwdIsRegular = false;

	#endregion 


	#region 所需拿到的ui控件

	public InputField nameField ;
	public InputField pwdField ;

	#endregion

	#region

	// 创建数据库的路径以及名字
	public string path = "/SqlData/user.db" ;

	// 创建表 表的名字 以及参数
	string tableName = "USER(uid integer primary key autoincrement,name text,password text)";


	#endregion

	#region 注册成功要重新回到登入界面

	public GameObject login_bg ;

	#endregion


	void Awake()
	{
		// 游戏运行 注册界面不显示
		gameObject.SetActive(false) ;

		login_bg = GameObject.FindWithTag ("login_bg");


	}

	// Use this for initialization
	void Start () 
	{
		// 创建数据库
		SqliteMangeToCSharp.GetInstance ().CreateDatabase (path,tableName);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	#region 验证输入合不合法的回调

	//先验证是不是手机 不是接着再验证是不是邮箱
	public string BeginRegularPlayer(string sender,string regularPhone,string regularEmail)
	{
		
		if (System.Text.RegularExpressions.Regex.IsMatch(sender, regularPhone))
		{
			Debug.Log ("验证手机");
			return "isPhone" ;
		}
		else
		{
			
			if (System.Text.RegularExpressions.Regex.IsMatch (sender, regularEmail)) {
				return "isEmail";
			} else 
			{
				return "NULL";
			}
		}

	}

	//密码验证
	public  bool BeginRegularPwd(string sender,string regularPwd){
		if (System.Text.RegularExpressions.Regex.IsMatch (sender, regularPwd)) 
		{
			return true;
		} 
		else
		{
			return false;
		}
	}

	#endregion

	#region 注册按钮的调用方法

	public void RegisBtn()
	{
		// 1. 先判断输入的账号合不合法 
		// 2. 合法就判断该账号存不存在 不存在就注册

		string isLegal = BeginRegularPlayer (nameField.text,regularPhone,regularEmail) ;
		if (isLegal == "isPhone" || isLegal == "isEmail") {
			Debug.Log ("账号输入合法 可以注册");
		
		} else 
		{

			Debug.Log ("不合法 重新输入 也没有必接下来的步骤了");
			return;
		}

		// 2.
		string UserNameOne = "USER where name="+" '"+nameField.text + "'";
		int count = SqliteMangeToCSharp.GetInstance ().selectTableDataCondition (UserNameOne,path);
		if (count > 0) {

			// 已经被注册 提示出现
			Debug.Log ("注册失败 已经被注册");

		} else
		{
			
			SqliteMangeToCSharp.GetInstance ().InsertData (string.Format("USER(name,password) values('{0}','{1}')",nameField.text,pwdField.text),path);
		
			Debug.Log("注册成功");
			gameObject.SetActive(false) ;
//			login_bg.SetActive (true);


		}


	}


	#endregion

}
