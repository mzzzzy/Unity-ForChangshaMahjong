using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;
public class NumberOfPerson : MonoBehaviour {

	public Toggle tog  ;


	public ToggleGroup toggleGroup;

	public static NumberOfPerson instant ;

	//  布尔值 在外面要知道tog是不是选中状态
	public bool isChoose = false;

	void Awake()
	{
		instant = this;

		toggleGroup = gameObject.transform.GetComponent<ToggleGroup> ();

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		PickThreeOrFour ();

	  

		
	}

	public void PickThreeOrFour()
	{
	

		IEnumerable<Toggle> ts = toggleGroup.ActiveToggles ();
		foreach(var item in ts)
		{
			tog = item;
		}

		// 只有当选择玩家人物为四人时才能创建牌局

		if (tog.name == "FourToggle") {

			isChoose = true;
		} else {

			isChoose = false;
		}

	}
}
