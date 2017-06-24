using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening ;
using DG.DemiLib ;
using UnityEngine.UI ;


public class MyMahjongScript : MonoBehaviour {


	#region 玩家pass键监听 参数为当前出牌方位
	public  delegate void PassButtonOnClick (string curDirString);
	public PassButtonOnClick onOkClickListener;//确认键监听

	#endregion
	public static MyMahjongScript instant ;

	 
	// 打出来的牌 image的索引
	private int putOutCardPoint ;

	private int SelfAndOtherPutoutCard;


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

    // 
	public string outDir ;

	// 当前的方向字符串
	private string curDirString = @"B" ;

	// 普通胡牌算法
	// private NormalHuScript norHu ;

	// 碰 吃 杠 胡 提示面板
	public BtnActionScript btnActioonScript ;


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
	// 控制时间是否开始计时 发完牌倒计时
	public bool isTimeSub =false;


	// 大厅消息功能 跑马灯效果
	public Text lightText ;

	public int cardPoint; // 记录当前出在桌面上的牌


	#region  当前是谁出牌 针对其他三个玩家  1-r 2-t 3-l

	bool isChuPaiR = false ;

	bool isChuPaiT = false ;

	bool isChuPaiL = false ;


	#endregion


	// 玩家出的牌所在的父类 所放在的位置

	public List<Transform> OutPardTransform;


	void Awake()
	{

		instant = this ;
	}

	// Use this for initialization
	void Start () {
		
		// 初始化所创建的数组
		initArratList ();

		// 将面板显示 不要出现的 全部setAction(false) 比如说 
		// 碰啊 吃啊 没有碰 吃的时候不需要显示 
		initPanel() ;

		// 出实现胡牌类

		// 


		FlowLight (); // 系统广播消息 跑马灯
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
		btnActioonScript = gameObject.transform.GetComponent<BtnActionScript> ();

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
				gob.GetComponent<bottomScript>().onSendMessage += cardChange;//发送消息fd
				gob.GetComponent<bottomScript>().reSetPoisiton += cardSelect;
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

	#region 自己打出来的牌
	/// <summary>
	/// 自己打出来的牌
	/// </summary>
	/// <param name="obj">Object.</param>
	public void cardChange(GameObject obj)//
	{
		int putOutCardPointTemp = obj.GetComponent<bottomScript>().getPoint();//将当期打出牌的点数传出


		SoundCtrl.getInstance ().playSound (putOutCardPointTemp,1);


		int handCardCount = handerCardList [0].Count - 1 ;
		if (handCardCount == 13 || handCardCount == 10 || handCardCount == 7 || handCardCount == 4 || handCardCount == 1) {
			Debug.Log ("进来了");
		
			obj.GetComponent<bottomScript> ().onSendMessage -= cardChange;
			obj.GetComponent<bottomScript> ().reSetPoisiton -= cardSelect;

			//int putOutCardPointTemp = obj.GetComponent<bottomScript>().getPoint();//将当期打出牌的点数传出
			//将牌的索引从minelist里面去掉
			Debug.Log ("putOutCardPointTemp" + putOutCardPointTemp);

			handerCardList [0].Remove (obj);

		
			Destroy (obj);
			SetPosition (true);
			createPutOutCardAndPlayAction (putOutCardPointTemp, 1);//讲拖出牌进行第一段动画的播放
			outDir = DirectionEnum.Bottom;
			//========================================================================
		
		}

		// 如果上面的if进不去 说明此时此刻不是玩家出牌 点击一下 牌上升 再点击 牌下降
		else
		{
			
			Vector3 v3 = obj.transform.localPosition;
			obj.transform.localPosition = new Vector3 (v3.x,-200.0f,v3.z);
			bottomScript.instant.isChupai = false;
			TipsManagerScript.getInstance ().setTips ("当前不是你出牌");
		}

	}

	#endregion
	/// <summary>
	/// Cards the select.
	/// </summary>
	/// <param name="obj">Object.</param>
	public void cardSelect(GameObject obj)
	{
		for (int i = 0; i < handerCardList[0].Count; i++)
		{
			if (handerCardList[0] [i] == null) {
				handerCardList[0].RemoveAt (i);
				i--;
			} 
			else
			{
				
			//	handerCardList[0] [i].transform.localPosition = new Vector3 (handerCardList[0] [i].transform.localPosition.x, -280f); //从右到左依次对齐
				handerCardList[0] [i].transform.GetComponent<bottomScript> ().selected = false;
			}
		}
		if (obj != null&& bottomScript.instant.isChupai)
		{
			// 这是控制 点击一个牌 一个牌的y上升一点 再点击牌就出去了
			obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, -150f);
			Debug.Log ("是我");
			obj.transform.GetComponent<bottomScript>().selected = true;
		}
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
		Debug.Log ("COUNT"+count);

		int startX = 594 - count*80;
		if (flag) 
		{
			for (int i = 0; i < count; i++) 
			{













			
			


			



	handerCardList[0][i].transform.DOLocalMove(new Vector3(startX + i * 80f +10f, -200f), 0.3f);









				//handerCardList[0] [i].transform.localPosition = new Vector3(startX + i * 80f-50f, -200f); //从左到右依次对齐

			}
		//	handerCardList[0] [count-1].transform.localPosition = new Vector3 (580f, -292f); //从左到右依次对齐

		} else 
		{
			
			for (int i = 0; i <count; i++) {



				handerCardList[0] [i].transform.localPosition = new Vector3 (-6, 2); //从左到右依次对齐
				Invoke("fapai",2.0f) ;
			    
			}




		//	Transform MainView = gameObject.transform.GetChild (0);

			// 拿到庄家最后一张牌
		//	Transform btnMax =  MainView.GetChild (MainView.childCount-1);


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
	//	Transform MainView = gameObject.transform.GetChild (0);

		// 拿到庄家最后一张牌
    //		Transform btnMax =  MainView.GetChild (MainView.childCount-1);
	//	btnMax.transform.localPosition = new Vector3 (529, -200); //从左到右依次对齐

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


	#region 将碰 吃 胡等面板 隐藏
	//  在这里清理掉不需要显示的东西
	public void cleanGamePlayUI()
	{

		if (isbegin) {
			btnActioonScript.cleanBtnShow ();
		}


	}
	#endregion

	/// <summary>
	/// 根据一个人在数组里的索引，得到这个人所在的方位，L-左，T-上,R-右，B-下（自己的方位永远都是在下方）
	/// </summary>
	/// <returns>The direction.</returns>
	/// <param name="avatarIndex">Avatar index.</param>
	private string getDirection(int avatarIndex)
	{
		string dir = "";
		switch (avatarIndex)
		{
		case 3: //上
			dir = "T" ;
			break;
		case 4: //左
			dir = "L";
			break;
		case 2: //右
			dir = "R";
			break;
		case 1:
			dir = "B";
			break;
		}


		return dir;

	}


	#region 设置红色箭头的显示方向
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

	#endregion

	#region 设置跑马灯效果


	public void FlowLight()
	{
		//lightText.text = "hahahahahahahahaha";
		lightText.DOText ("欢迎大家来到刘籽繇的麻将间 希望大家玩的开心", 2).SetEase (Ease.Linear).SetAutoKill (false).Pause ();
		lightText.DOText(" - This text will be added to the existing one", 2).SetRelative().SetEase(Ease.Linear).SetAutoKill(false).Pause();
		lightText.DOText("This text will appear from scrambled chars", 2, true).SetEase(Ease.Linear).SetAutoKill(false).Pause();

	}

	//flowLightText.DOText("This text will replace the existing one", 2).SetEase(Ease.Linear).SetAutoKill(false).Pause();




	#endregion


	#region  自己出牌

	/// <summary>
	/// 创建打来的的牌对象，并且开始播放动画
	/// </summary>
	/// <param name="cardPoint">Card point.</param>
	/// <param name="curAvatarIndex">Current avatar index.</param>
	private void createPutOutCardAndPlayAction(int cardPoint, int curAvatarIndex)
	{
		
		//SoundCtrl.getInstance ().playSound (cardPoint,avatarList [curAvatarIndex].account.sex);
		Vector3 tempVector3 = new Vector3(0, 0);

		outDir = getDirection(curAvatarIndex);
		switch (outDir)
		{
		case DirectionEnum.Top: //上
			tempVector3 = new Vector3 (0, 130f);
			Debug.Log ("我的方位t");
			break;
		case DirectionEnum.Left: //左
			tempVector3 = new Vector3(-370, 0f);
			Debug.Log ("我的方位l");
			break;
		case DirectionEnum.Right: //右
			tempVector3 = new Vector3(420f, 0f);
			Debug.Log ("我的方位r");
			break;
		case DirectionEnum.Bottom:
			tempVector3 = new Vector3 (0, -100f);
			Debug.Log ("我的方位b");

			break;
		}


	


		GameObject tempGameObject = createGameObjectAndReturn ("Prefab/ThrowCard/TopAndBottomCard",OutPardTransform[0],tempVector3);
		//tempGameObject.name = "putOutCard";
		tempGameObject.transform.localScale = Vector3.one;
		tempGameObject.GetComponent<TopAndBottomCardScript>().setPoint(cardPoint);
		Debug.Log ("到底是什么牌"+cardPoint);

		putOutCardPoint = cardPoint;
		SelfAndOtherPutoutCard = cardPoint;
		putOutCard = tempGameObject;


		putOutCard.transform.localPosition = new Vector3(0, 0); //位置   
		putOutCard.transform.localScale = Vector3.one; //原大小


	   
	   

		destroyPutOutCard(cardPoint);
		if (putOutCard != null)
		{
			//Destroy(putOutCard, 1f);
		}

	    // 自己数组要删掉出的这个牌


		// 打开右边
		isChuPaiR= true ;
		// 玩家出完牌了 轮到下一个玩家

		CurrentPutCardPlayerRight("R") ;


	}


	#region 右边玩家出牌
	private void CurrentPutCardPlayerRight(string currentPlayer)
	{

		if (isChuPaiR) {
			isChuPaiR = false;
			curDirString = currentPlayer;
			// 时间重置16s
			timer = 16;

			timeCount.text = timer.ToString ();
			isTimeSub = true;
			SetDirGameObjectAction ();

			Invoke("CreatPutCardPlayerR",4.0f) ;

		}


	}
	// 创建出的牌
	private void CreatPutCardPlayerR()
	{

		// 出牌前 先抓一张牌 

		cardPoint =int.Parse(RandomAssignment.instant.MajiangList [0]) ;

		RandomAssignment.instant.MajiangList.RemoveAt (0);


		GameObject tempGameObject = createGameObjectAndReturn ("Prefab/ThrowCard/ThrowCard_R",OutPardTransform[1], new Vector3(0f, 0f));
		tempGameObject.name = "right" ;
		tempGameObject.transform.localScale = Vector3.one;
		tempGameObject.GetComponent<TopAndBottomCardScript>().setLefAndRightPoint(cardPoint);
		SoundCtrl.getInstance ().playSound (cardPoint,0);

		//  出完牌后 1s后 去检测自己 可不可以碰 吃 胡 杠 注意： 吃 只有左边玩家打出来牌 才去检测
		Invoke("DiscoverPCHG",1.0f) ;

	}
		

	#endregion

	#region 检测玩家是否 碰 吃 胡 杠 

	private void DiscoverPCHG()
	{
		// 记录玩家手里的牌有几个等于最新桌面上出的牌
		int sum=0;
		// 遍历自己的牌 

		for (int i = 0; i < handerCardList [0].Count; i++) {
		
		    // 检测 碰吃胡
			if ( handerCardList[0][i].GetComponent<bottomScript>().getPoint() == cardPoint) 
			{
			    // 如果有三个等于 就是杠 如果两个等于就是碰

				sum = sum + 1;
			      
			}
		    
		}

		if (sum == 3) {
			// 碰 杠全部打开

			btnActioonScript.ShowBtn (2);
			btnActioonScript.ShowBtn (3);

			Debug.Log ("当前可以碰杠");
		     
		} else if (sum == 2) {
	         	
			// 碰打开
			btnActioonScript.ShowBtn (3);
			Debug.Log ("当前可以碰");
		} 
		else if (sum == 0 || sum == 1) {
		
			TipsManagerScript.getInstance ().setTips ("当前牌碰不到杠不到胡不到");

			// 不可以碰 吃 杠 胡 那么就下位玩家出牌
			// 上家top玩家出牌
			isChuPaiT = true ;
			CurrentPutCardPlayerTop ("T");
			// 时间重置16s
			timer = 16;

			timeCount.text = timer.ToString ();
			isTimeSub = true;
		}

		// 检测是否胡
//		else if()
//		{
//
//
//		}



			




    






	}


	#region 上家出牌专用检测碰杠胡吃
	public void DiscoverPCHGTop ()
	{

		// 记录玩家手里的牌有几个等于最新桌面上出的牌
		int sum=0;
		// 遍历自己的牌 

		for (int i = 0; i < handerCardList [0].Count; i++) {

			// 检测 碰吃胡
			if ( handerCardList[0][i].GetComponent<bottomScript>().getPoint() == cardPoint) 
			{
				// 如果有三个等于 就是杠 如果两个等于就是碰

				sum = sum + 1;

			}

		}

		if (sum == 3) {
			// 碰 杠全部打开

			btnActioonScript.ShowBtn (2);
			btnActioonScript.ShowBtn (3);
			Debug.Log ("当前可以碰杠");
			return;

		} else if (sum == 2) {

			// 碰打开
			btnActioonScript.ShowBtn (3);
			Debug.Log ("当前可以碰");
		} 
		else if (sum == 0 || sum == 1) {

			TipsManagerScript.getInstance ().setTips ("当前牌碰不到杠不到胡不到");

			// 不可以碰 吃 杠 胡 那么就下位玩家出牌
			// 上家top玩家出牌
			isChuPaiL = true ;
			CurrentPutCardPlayerLeft ("L");
		}
	}



	#endregion





	#region 左边出牌专用检测碰杠胡吃
	public void DiscoverPCHGLeft ()
	{

		// 记录玩家手里的牌有几个等于最新桌面上出的牌
		int sum=0;
		// 遍历自己的牌 

		for (int i = 0; i < handerCardList [0].Count; i++) {

			// 检测 碰吃胡
			if ( handerCardList[0][i].GetComponent<bottomScript>().getPoint() == cardPoint) 
			{
				// 如果有三个等于 就是杠 如果两个等于就是碰

				sum = sum + 1;

			}

		}

		if (sum == 3) {
			// 碰 杠全部打开

			btnActioonScript.ShowBtn (2);
			btnActioonScript.ShowBtn (3);
			Debug.Log ("当前可以碰杠");

		} else if (sum == 2) {

			// 碰打开
			btnActioonScript.ShowBtn (3);
			Debug.Log ("当前可以碰");
		} 
		else if (sum == 0 || sum == 1) {

			TipsManagerScript.getInstance ().setTips ("当前牌碰不到杠不到胡不到");

			// 不可以碰 吃 杠 胡 那么就下位玩家出牌
			// 自己出牌

			bottomScript.instant.isChupai = true;
			curDirString = "B" ;
			SetDirGameObjectAction ();

			// 玩家加一个牌




			GameObject gob = Instantiate (Resources.Load ("prefab/card/Bottom_B")) as GameObject;

			if (gob != null) {
				gob.transform.SetParent (parentList [0]);
				gob.transform.localScale = new Vector3 (1.1f, 1.1f, 1f);
				gob.GetComponent<bottomScript>().onSendMessage += cardChange;//发送消息fd
				gob.GetComponent<bottomScript>().reSetPoisiton += cardSelect;
				gob.GetComponent<bottomScript> ().setPoint (int.Parse(RandomAssignment.instant.MajiangList[0]));// 设置指针
				RandomAssignment.instant.MajiangList.RemoveAt (0);

				handerCardList [0].Add (gob); // 把牌存到大数组里面去
				SetPosition (true);
			}














		     
		}

	}

	#endregion




	#endregion
	private void CurrentPutCardPlayerTop(string currentPlayer)
	{
		if (isChuPaiT) 
		{
			isChuPaiT = false;
			curDirString = currentPlayer;
			// 时间重置16s
			timer = 16;

			timeCount.text = timer.ToString ();
			isTimeSub = true;
			SetDirGameObjectAction ();

			Invoke ("CreatPutCardPlayerT", 4.0f);
		}

	}

	public void CreatPutCardPlayerT()
	{
		// 出牌前 先抓一张牌 

		cardPoint =int.Parse(RandomAssignment.instant.MajiangList [0]) ;

		RandomAssignment.instant.MajiangList.RemoveAt (0);


		GameObject tempGameObject = createGameObjectAndReturn ("Prefab/ThrowCard/TopAndBottomCard",OutPardTransform[2], new Vector3(0f, 0f));
		tempGameObject.name = "Top" ;
		tempGameObject.transform.localScale = Vector3.one;
		tempGameObject.GetComponent<TopAndBottomCardScript>().setPoint(cardPoint);
		SoundCtrl.getInstance ().playSound (cardPoint,0);

		//  出完牌后 1s后 去检测自己 可不可以碰 吃 胡 杠 注意： 吃 只有左边玩家打出来牌 才去检测 
		//  因为第一个检测 里面打开了上家出牌的权利 所以检测不能用DiscoverPCHG方法
		Invoke("DiscoverPCHGTop",1.0f) ;


	}

	#endregion

	#region 左边玩家出牌

	private void CurrentPutCardPlayerLeft(string currentPlayer)
	{


		if (isChuPaiL) 
		{
			isChuPaiL = false;
			curDirString = currentPlayer;
			// 时间重置16s
			timer = 16;

			timeCount.text = timer.ToString ();
			isTimeSub = true;
			SetDirGameObjectAction ();

			Invoke ("CreatPutCardPlayerL", 4.0f);
		}





	}

	public void CreatPutCardPlayerL()
	{
		// 出牌前 先抓一张牌 

		cardPoint =int.Parse(RandomAssignment.instant.MajiangList [0]) ;

		RandomAssignment.instant.MajiangList.RemoveAt (0);


		GameObject tempGameObject = createGameObjectAndReturn ("Prefab/ThrowCard/ThrowCard_L",OutPardTransform[3], new Vector3(0f, 0f));
		tempGameObject.name = "Left" ;
		tempGameObject.transform.localScale = Vector3.one;
		tempGameObject.GetComponent<TopAndBottomCardScript>().setLefAndRightPoint(cardPoint);
		SoundCtrl.getInstance ().playSound (cardPoint,0);

		//  出完牌后 1s后 去检测自己 可不可以碰 吃 胡 杠 注意： 吃 只有左边玩家打出来牌 才去检测 
		//  因为第一个检测 里面打开了上家出牌的权利 所以检测不能用DiscoverPCHG方法
		Invoke("DiscoverPCHGLeft",1.0f) ;

	}





	// 别人打出来的牌 销毁 其实假现象 就是界面看到其他玩家的牌少了一张 
	private void destroyPutOutCard(int cardPoint)
	{


	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="path"></param>
	/// <param name="parent"></param>
	/// <param name="position"></param>
	/// <returns></returns>
	private GameObject createGameObjectAndReturn(string path,Transform parent,Vector3 position)
	{
		GameObject obj = Instantiate(Resources.Load(path)) as GameObject;
		obj.transform.SetParent (parent);
	    obj.transform.parent = parent;
		obj.transform.localScale = Vector3.one;
		obj.transform.localPosition = position;
		return obj;
	}


	#endregion



	// Update is called once per frame
	void Update () {

		if (ButtonCallbackMethod.instant.isBegin && isbegin) {
			

			startGame ();
			isbegin = false;

			TipsManagerScript.getInstance ().loadDialog ("说明", "这是一款单机版的长沙麻将 旨在为做联机版麻将做铺垫 电脑没有ai 只是傻瓜式把拿到的牌出出去 我是新手 对框架等理解的不好 想到什么写什么 所以代码非常的乱 但是基本逻辑都在 欢迎大家和我讨论vx:mzzy1029zy", doDissoliveRoomRequest, cancle);
		}

	}

	public void doDissoliveRoomRequest()
	{  

	}

	public void cancle()
	{
		Debug.Log("点击了取消") ;

	}

	// 修改帧率为1s调用一次 那么 可以把出牌倒计时放在这里
	void FixedUpdate()
	{



		if (timer == 0 && isTimeSub)
		{
			isTimeSub = false  ;
			timeCount.text = 0.ToString()  ;
			TipsManagerScript.getInstance ().setTips ("出牌超时1次 超过三次将挂机");
		 
		}
		if(isTimeSub)
		{
		     // 每秒递减
		     timer-- ;
			 timeCount.text = timer.ToString() ;
        
		}


	}


	#region 当玩家可以 碰／吃／胡／杠时 pass按钮 
	public void PassBtn()
	{
		// pass按钮的功能
		// 1. 清理调pass面板
		// 2. 出牌继续回到下一位玩家
		Debug.Log ("当前出牌玩家索引"+curDirString);
		btnActioonScript.cleanBtnShow ();

		if (curDirString == "R") {
			// 上家继续出牌
			isChuPaiT = true ;
			CurrentPutCardPlayerTop ("T");
		         
		} else if (curDirString == "T") {
			isChuPaiL = true ;
			CurrentPutCardPlayerLeft ("L");
		} else if (curDirString == "L") {
			
			bottomScript.instant.isChupai = true;
			curDirString = "B";
			SetDirGameObjectAction ();

			GameObject gob = Instantiate (Resources.Load ("prefab/card/Bottom_B")) as GameObject;

			if (gob != null) {
				gob.transform.SetParent (parentList [0]);
				gob.transform.localScale = new Vector3 (1.1f, 1.1f, 1f);
				gob.GetComponent<bottomScript>().onSendMessage += cardChange;//发送消息fd
				gob.GetComponent<bottomScript>().reSetPoisiton += cardSelect;
				gob.GetComponent<bottomScript> ().setPoint (int.Parse(RandomAssignment.instant.MajiangList[0]));// 设置指针
				RandomAssignment.instant.MajiangList.RemoveAt (0);

				handerCardList [0].Add (gob); // 把牌存到大数组里面去
				SetPosition (true);
			}
		} 
	


	}

	#endregion
}
