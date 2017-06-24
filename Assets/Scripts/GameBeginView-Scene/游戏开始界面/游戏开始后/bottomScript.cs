using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

using UnityEngine.EventSystems ;

public class bottomScript : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IDragHandler {

	public static bottomScript instant ;
	// Resource文件夹下 牌的指针
	private int cardPoint;

	// 给牌设置image image的图片就是确定牌的类型

	public Image image ;

	// 
	public bool selected = false ;

	private Vector3 oldPosition;

	public delegate void EventHandler(GameObject obj);
	public event EventHandler onSendMessage;
	public event EventHandler reSetPoisiton;

	private bool drag =false;


	// 控制当前不是自己出牌 点击牌上去 再点击 牌要下去
	public bool isChupai = true ;


	void Awake()
	{
		instant = this;

	}
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	// 拖拽

	public void OnDrag(PointerEventData eventData)
	{
		GetComponent<RectTransform> ().pivot.Set (0, 0);
		transform.SetSiblingIndex (gameObject.transform.parent.childCount-1);
		drag = true;
		//transform.position = Input.mousePosition;

	}
	// 按下
	public void OnPointerDown(PointerEventData eventData)
	{
		if (selected == false) {
			selected = true;
			oldPosition = transform.localPosition;
		
			Debug.Log ("按下");
		} else {
			
			sendObjectToCallBack ();
			Debug.Log ("再按下");

		}

	}

	public void OnPointerUp(PointerEventData eventData)
	{





		Vector3 tempTwo = new Vector3 (0,0,0);

		for (int i = 0; i < MyMahjongScript.instant.handerCardList [0].Count; i++) 
		{
		 


			Vector3 temp = MyMahjongScript.instant.handerCardList [0] [i].transform.localPosition;

			MyMahjongScript.instant.handerCardList [0] [i].transform.localPosition = new Vector3(temp.x,-200,temp.z);

			// 游戏一开始 就是自己出牌 所以isChupai初始值为true
			if (MyMahjongScript.instant.handerCardList [0] [i] == gameObject) {
				Debug.Log ("能进来吗");
				gameObject.transform.localPosition = new Vector3 (temp.x, -200, temp.z);

			}
		    
		}
			
		    

	
			reSetPoisitonCallBack ();


	

	
	}

	public void setPoint(int _cardPoint)
	{
		cardPoint = _cardPoint;
		image.sprite = Resources.Load ("Cards/Big/b"+cardPoint,typeof(Sprite)) as Sprite;

	}

	private void sendObjectToCallBack(){
		if (onSendMessage != null)     //发送消息
		{
			onSendMessage(gameObject);//发送当前游戏物体消息
		}
	}

	private void reSetPoisitonCallBack(){
		if (reSetPoisiton != null) {
			reSetPoisiton (gameObject);
		}
	}

	public int getPoint()
	{
		return cardPoint;
	}
}
