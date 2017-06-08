using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;
public class NumberOfPerson : MonoBehaviour {

	public Toggle togFour  ;
	public Toggle togThree  ;

	public static NumberOfPerson instant ;

	//  布尔值 在外面要知道tog是不是选中状态
	public bool isChoose = false;

	void Awake()
	{
		instant = this;

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		PickOneOfTwo ();

		
	}

	public void PickOneOfTwo()
	{



	}
}
