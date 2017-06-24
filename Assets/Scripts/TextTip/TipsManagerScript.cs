using UnityEngine;
using System.Collections;
using DG.Tweening;


public class TipsManagerScript {

	private static TipsManagerScript _instance;
	public Transform parent;
	public GameObject MainView;
	public TipsManagerScript()
	{

	}

	public static TipsManagerScript getInstance(){
		if (_instance == null) {
			_instance = new TipsManagerScript ();
		
		}
		return _instance;
	}

	public void setTips(string str){
	
		MainView = GameObject.FindWithTag ("MainView");

		parent = MainView.transform;

		GameObject temp = GameObject.Instantiate (Resources.Load ("Prefab/TipPanel") as GameObject);
		temp.name = "aaa";
		temp.transform.parent = parent;
		temp.transform.localScale = Vector3.one;
		temp.transform.localPosition =new Vector3 (0,120);
		temp.GetComponent<TipPanelScript> ().setText (str);
		temp.GetComponent<TipPanelScript> ().move();

	}


	public void loadDialog(string titlestr,string msgstr,DialogPanelScript.ButtonOnClick yesCallBack,DialogPanelScript.ButtonOnClick noCallBack){
		MainView = GameObject.FindWithTag ("MainView");

		parent = MainView.transform;


		GameObject temp = GameObject.Instantiate (Resources.Load ("Prefab/Image_Base_Dialog") as GameObject);
		temp.transform.parent = parent.parent;
		temp.transform.localScale = Vector3.one;
		temp.transform.localPosition = new Vector3 (0,60);
		temp.GetComponent<DialogPanelScript> ().setContent (titlestr,msgstr,true,yesCallBack,noCallBack);
		temp.transform.SetSiblingIndex (parent.childCount-1);
	}







}