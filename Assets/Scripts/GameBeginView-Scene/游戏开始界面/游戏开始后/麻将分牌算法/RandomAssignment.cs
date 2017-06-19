using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RandomAssignment : MonoBehaviour {

	/*
       1. 长沙麻将中 有 万 筒 索三种牌型
       2. 每种牌型分别有1-9 共计108张
       3. 1-9 每个都有4个一样的
    */

	/*
	1. 发牌 四个玩家 庄家14张牌 其他玩家13张牌
	*/

	/*
	1. 创建存储所有牌的数组 Majianglist
	2. 利用随机数函数将list全部打乱排序
	3. 创建四个数组PlayerList 代表四个玩家的牌
	4. 庄家拿Majianglist前面14张牌 玩家2接着拿后面13张牌。。。
	5. 数组里面存储的数字 其中 0-8 代表一万-九万
	9-17 代表一条-九条
	18-26 代表一筒-九筒
	6. Majianglist数组将已经分出去的牌所在的下标删除 
	7. 剩下存储的所有 每次只取一个出来发给正在出牌的玩家
	8. 以面向电脑屏幕 分为 上北下南 左西右东 up-down left-right 玩家位置用数组表示
	*/

	#region  数组
	[HideInInspector]   // 所有的牌以及四个玩家是手上的牌
	public List<string> MajiangList = new List<string> ();
	// 庄家的牌 14张
	public List<string> PlayerBankerBrandList = new List<string> ();
	// 玩家1的牌 13张
	public List<string> PlayerOneBrandList = new List<string> ();
	// 玩家2的牌
	public List<string> PlayerTwoBrandList = new List<string> ();
	// 玩家3的牌
	public List<string> PlayerThreeBrandList = new List<string> ();
	#endregion


	#region  用来打乱牌的数组 

	public List<string> BrandList = new List<string> ();

	#endregion


	#region 玩家增加1张牌

	// 增加1张牌 
	private string addBrandString ;

	#endregion

	#region 单例

	public static RandomAssignment instant ;

	#endregion

	/*
	private List<int> allBrands;
	private Dictionary<int,List<int>> evenyOneBrand;
	*/


	void Awake()
	{

		/*
		allBrands = new List<int>();
		evenyOneBrand = new Dictionary<int, List<int>>();
		*/
		allBrand ();
		instant = this;

	}

	// Use this for initialization
	void Start () {

		/* 测试数组存值
		foreach (var item in MajiangList)
		{
			//Debug.Log (MajiangList.Count);
			Debug.Log (item);
		}
		*/

		//  测试分牌是否正确
		playerBrand() ;


	}

	// Update is called once per frame
	void Update () {

	}

	/*
	public void GetAllBrand()
	{
		for (int i = 0; i < 4; i++) {
			for (int j = 0; j <= 26; j++) {
				allBrands.Add(j);
			}
		}

		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 13; j++) {
				evenyOneBrand[i].Add(GetRandomIndex());
			}
		}
		evenyOneBrand[0].Add(GetRandomIndex());
	}
	*/


	/*
	private int GetRandomIndex()
	{
		if(allBrands.Count == 0)
			return -1;

		int rand = Random.Range(0,allBrands.Count);
		int result = allBrands[rand];
		allBrands.Remove(rand);
		return result;
	}
	*/


	#region 存储所有的牌 随机存储
	public void allBrand()
	{
		// 存储
		for (int j = 0; j < 4; j++)
		{
			for(int k=0;k<=8;k++)
			{ 
				string g = k.ToString ();
				MajiangList.Add (g);
			}

			for (int h = 9; h <= 17; h++) 
			{ 
				string g = h.ToString ();
				MajiangList.Add (g);
			}

			for (int m = 18; m <= 26; m++) 
			{
				string g = m.ToString ();
				MajiangList.Add (g);
			}
		}



		// 随机发牌 打乱数组排序
		for (int i = 0; i < MajiangList.Count; i++)
		{

			int rand = Random.Range(0,MajiangList.Count);

			string temp  = MajiangList[rand] ;
			MajiangList[rand] = MajiangList[i] ;
			MajiangList [i] = temp;



		}

	}
	#endregion


	#region 洗牌算法




	#endregion




	#region 四个玩家分牌算法
	public void playerBrand()
	{
		// 这里直接在MajiangList数组里取14个牌 13 13 13牌给玩家了 就没有在设置随机规律分牌了
		// 玩家拿到牌 整理 从小到大排列

		// 庄家的牌
		for (int i = 0; i < 14; i++)
		{
			PlayerBankerBrandList.Add (MajiangList[i]); 
			Debug.Log (PlayerBankerBrandList[i]);
		}
		// 庄家的牌分配完了 MajiangList数组就可以删除它的牌了
		 SortList(PlayerBankerBrandList) ;
		MajiangList.RemoveRange(0,14) ;

		// 玩家1的牌
		for (int i = 0; i < 13; i++)
		{
			PlayerOneBrandList.Add (MajiangList[i]); 		
		}
		SortList(PlayerOneBrandList) ;
		MajiangList.RemoveRange (0,13);

		// 玩家2的牌
		for (int i = 0; i < 13; i++)
		{
			PlayerTwoBrandList.Add (MajiangList[i]); 		
		}
		SortList(PlayerTwoBrandList) ;
		MajiangList.RemoveRange (0,13);

		// 玩家3的牌
		for (int i = 0; i < 13; i++)
		{
			PlayerThreeBrandList.Add (MajiangList[i]); 		
		}
		SortList(PlayerThreeBrandList) ;
		MajiangList.RemoveRange (0,13);

		//测试分完牌后总牌数对不对
		//Debug.Log (MajiangList.Count);

		//测试玩家的牌从小到大排列对不对
	//	foreach (var item in PlayerBankerBrandList) 
	//	{
	//		Debug.Log (item);

	//	}

	}

	#endregion

	#region 游戏开始 由庄家先出牌 之后不是因为吃和碰和杠 而要出牌的玩家 都要增加一张牌 此方法要传一个参数 是哪个玩家要增加牌
	public void playerAddBrand()
	{
		// 此方法在玩家需要抓牌的时候调用

		// 每次从数组取出一张 数组第0张牌 取出后MajiangList数组要删除调
		addBrandString = MajiangList[0] ;
		MajiangList.RemoveAt (0);

	}

	#endregion

	#region 玩家的牌整理方法 从小到大排列

	public void SortList(List<string> list)
	{

		for (int i = 0; i < list.Count-1; i++) {

			for (int j = 0; j < list.Count - 1; j++) {

				if (int.Parse (list [j]) > int.Parse (list [j + 1])) 
				{

					string temp = list[j] ;
					list[j] = list[j+1] ;
					list[j+1] = temp ;
				}
			}

		}

	}

	#endregion
}
