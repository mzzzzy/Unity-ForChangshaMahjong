using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening ;
using DG.DemiLib ;
using UnityEngine.UI ;


public class MyMahjongScript : MonoBehaviour {




	 
	// 

	// 存储每个玩家牌的位置父类位置

	public List<Transform> parentList ; //在面板中直接拖


	// 剩余牌的张数
	public Text LeavedCastNumText ;

	// 庄家的索引

	private int bankerId ; // 方位索引
	private int curDirIndex ;
	private GameObject curCard;

	// 当前打出来的牌
	private GameObject putOutCard ;
	// 打出来的牌的数组
	public List<List<GameObject>> tableCardList;


	// 当前的方向字符串
	private string curDirString = @"B" ;

	// 普通胡牌算法
	// private NormalHuScript norHu ;

	// 碰 吃 杠 胡 提示面板
	// public ButtonActionScript btnActioonScript ;


	// 玩家手上的牌数组 0自己，1-右边。2-上边。3-左边
	// 庄家14张牌 
	public List<List<GameObject>> handerCardList;

	// 碰 吃 杠 胡 图片显示 显示在东南西北那 显示时 东南西北消失 2s后东南西北显示
	public GameObject pengEffectGame ;
	public GameObject gangEffectGame ; 
	public GameObject huEffectGame ;
	public GameObject chiEffectGame ;

	// 玩家碰 吃 杠 完后 牌所放在的位置
	public Transform pengGangParenTransformB ;
	public Transform pengGangParenTransformL ;
	public Transform pengGangParenTransformR ;
	public Transform pengGangParenTransformT ;

	// 存储东南西北 红色箭头的数组 哪个出牌 哪个方位就显示红色箭头
	public List<GameObject> dirGameList ;

	// 出牌的玩家的时间 倒计时 游戏开始的时候设置为16s
	public float timer = 0 ;


	// 游戏是否开始
	private bool isbegin = true;

	// 出牌时间倒计时
	public Text timeCount ;
	// 控制时间是否开始计时 发完牌蔡计时
	public bool isTimeSub =false;



	// Use this for initialization
	void Start () {

		// 初始化所创建的数组
		initArratList ();

		// 将面板显示 不要出现的 全部setAction(false) 比如说 
		// 碰啊 吃啊 没有碰 吃的时候不需要显示 
		initPanel() ;

		// 出实现胡牌类

		// 
	}

	#region 游戏一开始 清理面板还不需要出现的对象
	public void initPanel()
	{
		//1. 碰 吃等 
		// btnActionScript.cleanBtnShow() ;



	}

	#endregion

	#region  初始化创建的属性
	public void initArratList()
	{
		//dirGameList = new List<GameObject>() ;
		handerCardList = new List<List<GameObject>> ();
		tableCardList = new List<List<GameObject>> ();
		// 一个数组 存着四个数组 每个数组存着Gameobje 也就是按钮 一个按钮就是一个牌
		for (int i = 0; i < 4; i++)
		{
			handerCardList.Add (new List<GameObject>());
			tableCardList.Add (new List<GameObject>());
		}


	}
	#endregion

	#region 游戏开始

	public void startGame()
	{
		// 清理调游戏未开始显示 和游戏开始后 不需要再显示的对象 比如说：微牌 准备等
		cleanGamePlayUI() ;

		// 当前东南西北 指向箭头-> 为庄家 要知道庄家所在的方位
		// curDirString = 

		// 设置要显示的红色箭头
		SetDirGameObjectAction ();

		// 每个玩家的出牌时间为16s 超过后系统要作出回应 在这里 我们不做回应 超过了就超过了

		timer = 16.0f ;
		timeCount.text = timer.ToString() ;

		#region 初始化玩家的牌
		initMyCardListAndOtherCard (13,13,13);


		#endregion


	}

	private void initMyCardListAndOtherCard(int topCount,int leftCount,int rightCount)
	{
		// 四个玩家都在这里初始化

		// 先初始化自己 
		for (int b = 0; b < RandomAssignment.instant.PlayerBankerBrandList.Count; b++) 
		{

			GameObject gob = Instantiate (Resources.Load ("prefab/card/Bottom_B")) as GameObject;
		    
			if (gob != null) {
				gob.transform.SetParent (parentList [0]);
				gob.transform.localScale = new Vector3 (1.1f, 1.1f, 1f);
				gob.GetComponent<bottomScript> ().setPoint (int.Parse(RandomAssignment.instant.PlayerBankerBrandList[b]));// 设置指针
				SetPosition (false);
				handerCardList [0].Add (gob); // 把牌存到大数组里面去
			} else
			{

				Debug.Log ("--> 从Resources加载资源失败");
			}

		}

		initOtherCardList (DirectionEnum.Left,leftCount);
		initOtherCardList (DirectionEnum.Right,rightCount);
		initOtherCardList (DirectionEnum.Top,topCount);


	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="initDirection"></param>
	private void initOtherCardList(string initDiretion,int count) //初始化
	{
		for (int i = 0; i < count; i++)
		{
			GameObject temp = Instantiate(Resources.Load("Prefab/card/Bottom_" + initDiretion)) as GameObject; //实例化当前牌
			if (temp != null) //有可能没牌了
			{
				temp.transform.SetParent(parentList[getIndexByDir(initDiretion)]); //父节点
				temp.transform.localScale=Vector3.one;
				switch (initDiretion)
				{

				case DirectionEnum.Top: //上
					temp.transform.localPosition = new Vector3(-204+ 38*i, 0); //位置   
					handerCardList[2].Add(temp);
					temp.transform.localScale = Vector3.one; //原大小
					break;
				case DirectionEnum.Left: //左
					temp.transform.localPosition = new Vector3(0, -105 + i*30); //位置   
					temp.transform.SetSiblingIndex(0);
					handerCardList[3].Add(temp);
					break;
				case DirectionEnum.Right: //右
					temp.transform.localPosition = new Vector3(0, 119 - i*30); //位置     
					handerCardList[1].Add(temp);
					break;
				}
			}

		}
	}






	public void SetPosition(bool flag)//设置位置
	{
		int count = handerCardList[0].Count;
		//int startX = 594 - count*79;
		int startX = 594 - count*80;
		if (flag) 
		{
			for (int i = 0; i < count-1; i++) 
			{
				handerCardList[0] [i].transform.localPosition = new Vector3 (startX + i * 80f, -292f); //从左到右依次对齐
			}
			handerCardList[0] [count-1].transform.localPosition = new Vector3 (580f, -292f); //从左到右依次对齐

		} else 
		{
			
			for (int i = 0; i <count; i++) {



				handerCardList[0] [i].transform.localPosition = new Vector3 (-6, 2); //从左到右依次对齐
				Invoke("fapai",2.0f) ;
			    
			}





			Transform MainView = gameObject.transform.GetChild (0);

			// 拿到庄家最后一张牌
			Transform btnMax =  MainView.GetChild (MainView.childCount-1);
			btnMax.transform.localPosition = new Vector3 (-6, 2); //从左到右依次对齐

	}
	}

	public void fapai()
	{
		int count = handerCardList[0].Count;
		//int startX = 594 - count*79;
		int startX = 594 - count*80;


		for (int i = 0; i <count; i++) {



			handerCardList[0][i].transform.DOLocalMove(new Vector3(startX + i * 80f +10f, -200f), 0.3f);

		}
		Transform MainView = gameObject.transform.GetChild (0);

		// 拿到庄家最后一张牌
		Transform btnMax =  MainView.GetChild (MainView.childCount-1);
		btnMax.transform.localPosition = new Vector3 (529, -200); //从左到右依次对齐

		// 设置东南西北红色提示小框
		SetDirGameObjectAction();

	}





	/// <summary>
	/// Gets the index by dir.
	/// </summary>
	/// <returns>The index by dir.</returns>
	/// <param name="dir">Dir.</param>
	private int getIndexByDir(string dir){
		int result = 0;
		switch (dir)
		{
		case "T": //上
			result = 2;
			break;
		case "L" : //左
			result = 3;
			break;
		case "R": //右
			result = 1;
			break;
		case "B": //下
			result = 0;
			break;
		}
		return result;
	}



	//  在这里清理掉不需要显示的东西
	public void cleanGamePlayUI()
	{




	}

	/// <summary>
	/// 根据一个人在数组里的索引，得到这个人所在的方位，L-左，T-上,R-右，B-下（自己的方位永远都是在下方）
	/// </summary>
	/// <returns>The direction.</returns>
	/// <param name="avatarIndex">Avatar index.</param>
	private string getDirection(int avatarIndex)
	{

		return "以后完成";

	}

	// 设置红色箭头的显示方向
	public void SetDirGameObjectAction()
	{
		// 先全部不显示
		for (int i = 0; i < dirGameList.Count; i++) {

			dirGameList [i].SetActive (false);
		}
		// 在把要显示的打开 第一次打开下方的自己
		int a = getIndexByDir(curDirString) ;
		dirGameList[a].SetActive(true) ;
		isTimeSub = true  ;



	}



	#endregion


	// Update is called once per frame
	void Update () {

			if (ButtonCallbackMethod.instant.isBegin && isbegin) {
		
			isbegin = false;
			startGame ();
		}

	}

	// 修改帧率为1s调用一次 那么 可以把出牌倒计时放在这里
	void FixedUpdate()
	{



		if (timer == 0 && isTimeSub)
		{
			isTimeSub = false  ;
			timeCount.text = 0.ToString()  ;
			Debug.Log("你出牌时间超时了 请快点出牌 ")  ;
		 
		}
		if(isTimeSub)
		{
		     // 每秒递减
		     timer-- ;
			 timeCount.text = timer.ToString() ;
        
		}


	}
}