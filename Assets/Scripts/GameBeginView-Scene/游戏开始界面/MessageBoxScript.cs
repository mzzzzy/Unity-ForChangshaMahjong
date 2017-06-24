using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening ;

public class MessageBoxScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void btnClick(int index)
	{
		hidePanel ();

	}

	public void showPanel()
	{
		gameObject.transform.DOLocalMove (new Vector3 (376, -45), 0.4f);

		gameObject.transform.SetSiblingIndex(gameObject.transform.parent.childCount-1) ;


	}

	public void hidePanel()
	{
		gameObject.transform.DOLocalMove (new Vector3(376,533), 0.4f);
	}



}

