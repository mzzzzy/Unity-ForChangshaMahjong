using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TopAndBottomCardScript : MonoBehaviour {
	private int cardPoint;

	//=========================================
	private Image cardImg;
	// Use this for initialization
	void Awake () {
		if (transform.childCount == 2) {
			cardImg = transform.GetChild (1).GetComponent<Image> ();
			Debug.Log ("我是自己出的牌");
		} else {
		
			cardImg = transform.GetChild (0).GetComponent<Image> ();
			Debug.Log ("我是右边出的牌");
		}
	}

	// Update is called once per frame
	void Update () {

	}

	public void setPoint(int _cardPoint)
	{
		cardPoint = _cardPoint;//设置所有牌指针
		Debug.Log("11111 " + cardImg);

		Debug.Log(Resources.Load("Cards/Small/s"+cardPoint,typeof(Sprite)));
		cardImg.sprite = Resources.Load("Cards/Small/s"+cardPoint,typeof(Sprite)) as Sprite;

	}

	public void setLefAndRightPoint(int _cardPoint){
		cardPoint = _cardPoint;//设置所有牌指针
		cardImg.sprite = Resources.Load("Cards/Left&Right/lr"+cardPoint,typeof(Sprite)) as Sprite;

	}

	public int getPoint()
	{
		return cardPoint;
	}
}
