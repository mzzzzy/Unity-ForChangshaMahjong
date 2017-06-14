using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening ;

public class SoundToggleScript : MonoBehaviour {

	public GameObject openBtn ;
	public GameObject closeBtn ;
	public GameObject showFrameBtn;
	private bool isShowFrame = false ;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void openClick()
	{
		openBtn.SetActive (false);
		closeBtn.SetActive (true);

	}

	public void closeClick()
	{
		openBtn.SetActive (true);
		closeBtn.SetActive (false);

	}

	public void clickSettingBtn()
	{
		if (!isShowFrame) {

			showSettingframe ();
			isShowFrame = true;
			showFrameBtn.transform.Rotate (new Vector3 (0, 0, 90));

		} else 
		{
			hideSettingFrame ();
			isShowFrame = false;
			showFrameBtn.transform.Rotate (new Vector3(0,0,-90));

		}

	}

	private void showSettingframe()
	{
		gameObject.transform.DOLocalMove (new Vector3(-1,-5), 0.4f);


	}
	private void hideSettingFrame()
	{
		gameObject.transform.DOLocalMove (new Vector3(-1,93), 0.4f);
	}
}