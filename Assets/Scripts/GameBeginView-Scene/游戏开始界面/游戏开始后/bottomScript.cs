using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class bottomScript : MonoBehaviour {


	// Resource文件夹下 牌的指针
	private int cardPoint;

	// 给牌设置image image的图片就是确定牌的类型

	public Image image ;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void setPoint(int _cardPoint)
	{
		cardPoint = _cardPoint;
		image.sprite = Resources.Load ("Cards/Big/b"+cardPoint,typeof(Sprite)) as Sprite;


	}
}
