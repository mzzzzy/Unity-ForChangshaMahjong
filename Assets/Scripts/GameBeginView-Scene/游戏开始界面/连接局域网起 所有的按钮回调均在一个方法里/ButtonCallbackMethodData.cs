using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class ButtonCallbackMethodData: MonoBehaviour {


	[Tooltip("这个类给按钮方法类提供数据源")]


	public InputField ipInput;
	public InputField portInput;
	public InputField nameInput ;
	public static ButtonCallbackMethodData _instant ;


	void Awake()
	{
		_instant = this ;

	}

	/// <summary>
	/// 获取端口号
	/// </summary>
	/// <returns>The port.</returns>
	public int GetPort()
	{
		try 
		{
			if(portInput !=null)
			{
				return int.Parse(portInput.text) ;

			}
			
		} catch (System.Exception ex) 
		{
			
			Debug.Log ("得到端口号方法出错");
		}
		return 23456 ;
	}



	// 获取ip地址

public string GetIP()
	{
		try {

			if(ipInput !=null)
			{
				return ipInput.text ;

			}
		} catch (System.Exception ex) 
		{
			Debug.Log ("得到ip地址方法出错");
			
		}

		return "127.0.0.1" ;

	}




}
