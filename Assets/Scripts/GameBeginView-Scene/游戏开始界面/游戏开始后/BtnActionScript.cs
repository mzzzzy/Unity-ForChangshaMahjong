using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnActionScript : MonoBehaviour {


	// 五个按钮
	public GameObject huBtn ;
	public GameObject gangBtn ;
	public GameObject pengBtn;
	public GameObject chiBtn ;
	public GameObject passBtn ;
	public GameObject mianban  ; // 挂在 过 碰吃等 面板

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}


	#region 胡杠碰吃 pass 1. 在碰 吃 杠的时候 可能同时也可以胡牌

	public void ShowBtn(int type)
	{

		passBtn.SetActive (true);
		if (type == 1) 
		{
			huBtn.transform.localPosition = new Vector3 (341f,-115f);
			passBtn.SetActive (true);
			huBtn.SetActive(true);
			mianban.SetActive(true) ;
		}
		if (type == 2) {
			mianban.SetActive(true) ;
			gangBtn.SetActive (true);

			gangBtn.transform.localPosition = new Vector3 (370f,-128f);

			if (huBtn.activeSelf) {
				mianban.SetActive(true) ;
				huBtn.transform.localPosition = new Vector3 (254f, -115f);
			} 
			if (passBtn.activeSelf == false) {
				mianban.SetActive(true) ;
				passBtn.SetActive (true);
			}


		}
		if (type == 3) {

			Debug.Log ("碰");
			pengBtn.SetActive (true);
			mianban.SetActive(true) ;
			//pengBtn.transform.localPosition = new Vector3 (370f, -128f);

			if (gangBtn.activeSelf) {
				//gangBtn.transform.localPosition = new Vector3 (257f, -128f);
				gangBtn.SetActive(true);
				if (huBtn.activeSelf) {
					huBtn.transform.localPosition = new Vector3 (124f, -115f);
					mianban.SetActive(true) ;
				}
			} else {
				if (huBtn.activeSelf) {
					//huBtn.transform.localPosition = new Vector3 (240f, -115f);
					huBtn.SetActive(true);
					mianban.SetActive(true) ;
				}
			}
			if (passBtn.activeSelf == false) {
				passBtn.SetActive (true);
				mianban.SetActive(true) ;
			}
		}

		if(type == 4)
		{
			mianban.SetActive(true) ;
			chiBtn.SetActive (true);

		//	chiBtn.transform.localPosition = new Vector3 (370f, -128f);
			if (gangBtn.activeSelf) {
				mianban.SetActive(true) ;
				gangBtn.SetActive(true);
				//gangBtn.transform.localPosition = new Vector3 (257f, -128f);
				if (huBtn.activeSelf) {
					mianban.SetActive(true) ;
					huBtn.SetActive(true);
					huBtn.transform.localPosition = new Vector3 (124f, -115f);
				}
			} else {
				if (huBtn.activeSelf) {
					mianban.SetActive(true) ;
					huBtn.SetActive(true);
					//huBtn.transform.localPosition = new Vector3 (240f, -115f);
				}
			}
			if (passBtn.activeSelf == false) {
				mianban.SetActive(true) ;
				passBtn.SetActive (true);
			}

		}


	}

	#endregion

	// 游戏一开始不需要显示
	public void cleanBtnShow(){
		mianban.SetActive(false) ;
		huBtn.SetActive (false);
		gangBtn.SetActive (false);
		pengBtn.SetActive (false);
		chiBtn.SetActive (false);
		passBtn.SetActive (false);

	}

}
