using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class TipPanelScript : MonoBehaviour {

	public Text label;

    


	void Awake()
	{
		


	}
	// Use this for initialization
	void Start () {

	}

	public void setText( string str){
		label.text = str;
	}

	public void startAction(){
		Invoke ("TipsMoveCompleted",4f);
	}

	public void move(){
		gameObject.transform.DOLocalMove (new Vector3(0,75),3f).OnComplete(TipsMoveCompleted);
	}

	public void TipsMoveCompleted(){
		Destroy (gameObject);
	}
	// Update is called once per frame
	void Update () {

	}
}

