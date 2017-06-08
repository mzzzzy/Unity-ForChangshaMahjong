using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI ;
using UnityEngine.UI ;
using UnityEngine.EventSystems ; // 比如鼠标进入与离开等系统方法需要导入这个
using UnityEngine.SceneManagement ; //跳转另一个场景时用到
using System.IO ; // 文件输入与输出 操作文件读取和写入时用到
using System.Xml ; // xml解析时要用到
using System ; // 要用到visual stadio 的语法的时候

public class HelpDocument : MonoBehaviour {

	/// <summary>
	/// 注意事项
	/// </summary>
	public void Atendion()
	{
		// 有些系统方法提供了某个组件的属性 实际情况中 只要拿到一个游戏对象的一个属性 其中的属性就都可以拿到
		// 比如：override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		// animator.GetComponent<AudioSource>().Play() ; 我就可以用animator.getComponent去拿到这个组件的其他属性


		// 判断两个游戏对象的距离 只要用两个游戏对象position相减 然后求模就行了

		// 属性的enable方法是设置组件属性可不可用

		// SceneManager.LoadScene(0); 重新加载场景  using UnityEngine.SceneManagement ;

		// 想让private的对象也能在面板上显示
		// 添加这行代码在priva的上一行[SerializeField] 

		// 如何限制一个对象的最大移动 比如 打弹球 左右移动 左边右边到了一定程度就不能移动
		// float xPos = gameObject.transform.position.x - 0.6f;
		// playerPos = new Vector3 (Mathf.Clamp(xPos,69,78),1.53f,0);
		// gameObject.transform.position = playerPos;

		//  重新加载场景的时候 灯光变暗 变色 解决办法？  window-lighting 取消勾选auto, 再点击旁边的build

		// GameObject currentMonster = Instantiate (monsterPrefab, begin.position, Quaternion.identity);加载预设体对象monsterPrefab代表预设体 这个加载预设体的方法有很多重栽
		// Input.mousePosition 实时获得鼠标的位置
		// Input.anyKeyDown 任意键被按下都会返回bool值
		//  Input.GetAxis("Mouse X") 获得鼠标x增量
		// 隐藏鼠标 Cursor.visible = false;

	}







	/// <summary>
	/// 生命周期 从上到下依次执行
	/// </summary>
	// Use this for initialization

	public void LifeCycle()
	{
		// 9个方法 从上到下 依次运行
		string Awake = "void Awake()--游戏开始时最先运行 不受组件可用性的影响";
		string OnEnable = "void OnEnable()--脚本组件可用时执行一次";
		string Start = "void Start()--在OnEnable()之后执行依次受组件可用性影响" ;
		string FixedUpdate = "void FixedUpdate()--每隔一定时间间隔执行一次通常用于物理更新";
		string Update = "void Update()--每帧执行一次";
		string LateUpdate = "void LateUpdate()--在Update之后运行";
		string OnGUI = "void OnGUI()--每帧执行两次";
		string OnDisable = "void OnDisable()--脚本不可用时执行一次";
		string OnDestory = "void OnDestory()--脚本组件销毁时执行一次";

	}

	public void SystemIMethiod()
	{
		// 当开始拖动一个物体的时候，执行这个方法
		// 一个拖拽，只会执行一次
		//public void OnBeginDrag (PointerEventData eventData) 

		// 在拖动的过程中调用, 持续调用
		// eventData.position 表示当前鼠标的位置
		// public void OnDrag (PointerEventData eventData) 

		// 拖拽完成后调用
		// public void OnEndDrag (PointerEventData eventData)

		// 当鼠标移动到这个物体内部的时候
		// public void OnPointerEnter (PointerEventData eventData) 

		// 当鼠标移移出物体范围的时候
		// public void OnPointerExit (PointerEventData eventData)

		// 鼠标点下的时候触发
		// public void OnPointerDown (PointerEventData eventData)

		// 鼠标抬起的时候触发
		// public void OnPointerUp (PointerEventData eventData)

		// 鼠标点击一次调用
		// public void OnPointerClick (PointerEventData eventData) 
	}

	/// <summary>
	/// Debug的方法
	/// </summary>

	public void DebugOut()
	{

		// 画线方法    参数是开始   结束位置
		Debug.DrawLine(Vector3.up,Vector3.forward);
		Debug.DrawRay (Vector3.up,Vector3.forward);

		//Debug.DrawLine (Vector3.zero,Vector3.right,Color.red,10);

	}

	/// <summary>
	/// GameObject的方法和属性
	/// </summary>

	public void GameObjectMethod()
	{
		// 当前物体对象的名字／层／tag值等 用点语法点出来
		string name = gameObject.name;
		// transform组件
		Transform transform = gameObject.transform ;
		// 根据物体名字／tag值找到物体
		GameObject gameObjectFind =  GameObject.Find("物体在控制面板的名字") ;
		GameObject gameObjectTag = GameObject.FindWithTag ("物体的tag值");
		// 找到所有tags值物体
		GameObject[] gameObjectTags = GameObject.FindGameObjectsWithTag("tag值") ;
		// 从子物体上找组件
		//gameObject.GetComponentInChildren<> ();

		// 直接用方法判断当前tag值所代表的物体是否和我拿来比较的物体一致
		// 返回bool值
		bool isTrue = gameObject.CompareTag ("");

		//  创建一个基础平面对象 如 球 cube 等
		//  创建一个cube
		GameObject.CreatePrimitive (PrimitiveType.Cube);

		// 设置该游戏对象是否可用
		gameObject.SetActive(true) ;

	}

	/// <summary>
	/// Transform的属性和方法
	/// </summary>

	public void TransformMethod()
	{
		// 拿到当前对象所在的世界坐标位置 Localposition是本地坐标
		Vector3 v3 = gameObject.transform.position ;

		// eulerAngles是旋转的角度 
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		//  求出转向的夹角 并转为角度
		float degress = Mathf.Atan2 (h,v)*Mathf.Rad2Deg ;
		gameObject.transform.eulerAngles= new Vector3 (0, degress, 0);


		// 物体转身
		if(Mathf.Abs(h)>0.3f  || Mathf.Abs(v)>0.3f)
		{
			gameObject.transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(new Vector3(h,0,v)),Time.deltaTime*10);
		}

		// 自转
		gameObject.transform.Rotate(Vector3.up * Time.fixedDeltaTime * 20) ;


		// 重新设置该游戏对象父类
		// transform.SetParent ();

		// 根据下标找到该游戏对象的子类
		// transform.GetChild ();

		// 获取自己在父类下的下标
		int index = transform.GetSiblingIndex() ;

		// 重新设置自己在父类下的下标
		// transform.SetSiblingIndex(0) ;

		// 获取当前游戏对象的子类个数
		int chileCount = transform.childCount ;

		// 找到该游戏对象的根父类 也是最上面那层父类
		Transform transformRoot = transform.root ;

		// 和GameObject有着一样的方法 但是这个方法可以找到未激活的对象
		Transform transformFind = transform.Find("") ;

		// Translate 让物体移动
		transform.Translate(Vector3.forward*Time.deltaTime) ;

		// 公转 第一个参数是围绕哪个点转 第二个参数是围绕哪个轴转 第三个是转的速度
		transform.RotateAround(new Vector3(0,12,3),new Vector3(0,1,0),Time.deltaTime*5.0f) ;

		// 旋转物体使z轴指向目标物体。
		transform.LookAt(Vector3.zero) ;

	}

	/// <summary>
	/// Vector3的方法
	/// </summary>

	public void Vector3Method()
	{
		Vector3 v3 = new Vector3 ();
		// 返回模为1的向量
		Vector3 vNormal = v3.normalized;
		// 求模
		float magnitude = v3.magnitude ;

		//  插值 
		//Vector3.Lerp

		// 求单位向量
		Vector3 vNormalized = Vector3.Normalize (new Vector3(0,0,0));

		// 求两点的距离
		float distance = Vector3.Distance (new Vector3(0,0,0),new Vector3(0,0,1));

		// 求一个向量在一个方向上的投影向量
		Vector3.Project(new Vector3(0,2,5),Vector3.up) ;

		// 求两个向量的夹角
		float angle = Vector3.Angle(new Vector3(0,2,6),new Vector3(2,4,5)) ;

		// 求两个向量的叉乘 可以求出此时两个向量法向量
		// 计算方法为 向量1的模*向量2的模*sina（1,2的夹角）
		Vector3 vF = Vector3.Cross (Vector3.up,Vector3.forward);

		// 两个向量的点乘
		// 计算方法为 向量1的模*向量2的模*cos（1,2的夹角）

		float Dotcount = Vector3.Dot (Vector3.up,Vector3.forward);

		// 
		// Vector3.up ==  new Vector3(0,1,0) ;
		// Vector3.forward == new Vector3(0,0,1) ;
		// Vector3.right == new Vector3 (1, 0, 0);

	}

	public void MathMethod()
	{
		// 叉值 第一个参数是现在的值 第二个是目标值 
		Mathf.Lerp(0,1,Time.deltaTime) ;

		// 限定值 第一个数是要比较的参数值 第二个是最小值 第三个是最大值 
		// 如果a在最大值和最小值之间 就去a 如果大于最大 就等于3 如果小于最小 就等于1
		int a = 0 ;
		Mathf.Clamp(a,1,3) ;

		// 绝对值
		int abs = Mathf.Abs(-5) ;

		// 返回两者中的最大值
		int max = Mathf.Max (6,8);

		// 弧度转角度 Mathf.Rad2Deg
		float degress = Mathf.Atan2 (0,0)*Mathf.Rad2Deg ;
		//角度转弧度
		degress *= Mathf.Deg2Rad;

	}
	/// <summary>
	/// 关于时间的方法
	/// </summary>

	public void TimeMethod()
	{







	}

	/// <summary>
	/// 触发器属性
	/// </summary>

	public void ColliderMehod()
	{
		// 两个物体要碰撞的条件：两个必须都有碰撞体 其中至少有一个刚体 保持相对运动

		// 回调的三个函数 碰撞 
		// OnCollisionEnter(Collision other)
		// OnCollisionStay(Collision other)
		// OnCollisionExit(Collision other)

		// 条件:两个物体都有碰撞体组件，其中1个是触发器 其中至少有一个刚体 且有一个是刚体的对象必须是运动的
		// 勾选isTrigger后的回调函数 三个 
		// OnTriggerEnter(Collider other)
		// OnTriggerStay(Collider other)
		// OnTriggerExit(Collider other)
	}

	/// <summary>
	/// 刚体属性
	/// </summary>

	public void RigidBodyMethod()
	{
		// 速度向量 可以使物体向前移动
		GetComponent<Rigidbody> ().velocity= new Vector3 (1,0,0);

		// 向刚体添加一个力 让游戏对象往前移动
		GetComponent<Rigidbody> ().AddForce(new Vector3 (1,0,0));

		// 爆炸力效果 第一个参数爆炸的力量大小 第二个参数爆炸产生影响的球体的中心
		// 第三个参数是爆炸影响球体的半径
		GetComponent<Rigidbody>().AddExplosionForce(100,new Vector3(1,1,1),10) ;

		// 组件都是可以在脚本中拿到的
		// 组件
		// 物理学是否够影响这个刚体 勾选isKinematic后 物体将不受力的控制 移动就不能用AddFore添加力的方式了  

		// 射线检测

		// Physics.Raycast ();

	}

	/// <summary>
	/// 人机交互
	/// </summary>

	public void MouseButton()
	{
		// 鼠标按下 有按下就有抬起 0是左 1是右键 2是中间
		Input.GetMouseButtonDown(0) ;
		// 鼠标抬起
		Input.GetMouseButtonUp (0);

		//直接是鼠标按下
		Input.GetMouseButton(0) ;

		// 得到当前鼠标position
		Vector3 vMousePosion = Input.mousePosition;

		//键盘
		//键盘按下Q键
		if (Input.GetKeyDown (KeyCode.Q)) {


		}

		// 检测持续按住
		if (Input.GetKey (KeyCode.W)) {

			Debug.Log ("w被持续按住");
		}

		// 用户自定按键 在工程成面板Edit-Project Setting- Input 自定义按键 并取个名字
		if (Input.GetButtonDown ("myZ")) {


		}

		// 虚拟按键
		float v = Input.GetAxis ("Vertical") ;
		float h = Input.GetAxis ("Horizontal");
	}

	/// <summary>
	/// 播放声音的属性
	/// </summary>
	public void AudioSourceMethod()
	{
		// 播放声音
		AudioSource audioSource = new AudioSource ();
		audioSource.Play ();

		// 播放声音片段 在某一个位置播放
		AudioClip audioClip = new AudioClip() ;
		AudioSource.PlayClipAtPoint (audioClip, transform.position);


	}

	/// <summary>
	/// 射线
	/// </summary>
	/// <param name="orgin">Orgin.</param>
	/// <param name="dir">Dir.</param>
	public void RaycastMethod(Vector3 orgin,Vector3 dir)
	{
		RaycastHit hit ;
		// 某个位置（origin)朝某个方向(direction)的一条射线 第二个参数射线到达的最大距离 hit是射线射到的对象携带的信息
		if(Physics.Raycast(orgin+Vector3.up,dir,out hit))
		{
			// 射线射到的对象是不是tag值为player 的对象
			if (hit.collider.CompareTag ("player")) 
			{

			}

		}

		// RayCast 很常用 从相机发出的一条射线
		Ray r = Camera.main.ScreenPointToRay (Input.mousePosition); //返回一条射线Ray从摄像机到屏幕指定一个点(由鼠标控制)

		// 100是射线 射向的最大距离
		if (Physics.Raycast (r, out hit,100)) {

			//     输出射线鼠标射到的物体名称
			Debug.Log (hit.transform.name);

			if (hit.transform.name == "Plane")
			{
				// .point可以获取到射线射到的点的世界坐标
				gameObject.transform.position = hit.point +new Vector3(0,0.5f,0);


			}

		}
	}

	/// <summary>
	/// 导航方法
	/// </summary>
	public  void NavMeshAgentMethod()
	{
		// 导航组件
		NavMeshAgent nav  = new NavMeshAgent() ;

		// 导航路径
		NavMeshPath path = new NavMeshPath();

		// 设置导航目的地
		nav.SetDestination(new Vector3(0,0,0)) ;

		// path.status 导航状态
		// corners 导航过程中 所有的拐点坐标


	}

	/// <summary>
	/// 加载预设体 所有的要加载的资源 预设体 要放在Resources
	/// </summary>

	public void ResourcesMethod()
	{
		// 弄成预设体后 如何做？ 1.先将这个预设体加载到内存中 要加载的预设体必须放在文件名为Resources下
		// 2.这一步加载得到的是一个object的类型的 要转换为GameObject类型

		// Sphere是预设体的名字
		//Object prefab = Resources.Load ("Sphere");
		// as 是强转的意思
		//  GameObject obj = GameObject.Instantiate (prefab) as GameObject;

	}

	/// <summary>
	/// 鼠标点击事件
	/// </summary>

	public void ButtonMethod()
	{
		Button component =     gameObject.GetComponent<Button> ();

		//     component.onClick.AddListener (); // 添加事件监听 括号里是按钮事件方法的名称


	}

	/// <summary>
	/// slider滑块事件
	/// </summary>

	public void SliderMethod()
	{
		//gameObject.GetComponent<Slider> ().onValueChanged.AddListener() 括号里是方法名称

	}

	/// <summary>
	/// 系统单例 类似于ios中的NSUserDefaults
	/// </summary>

	public void PlayerPrabsMethod()
	{

		// 系统单例类 可以存储string float int 三种类型的数据 

		// 存
		PlayerPrefs.SetString ("name","刘籽繇");
		// 取
		string Name = PlayerPrefs.GetString("name") ; 
		//	数据被我们从本地文件中读取出来了，完成了一次简单的存档操作。
		//	在PlayerPrefs 类中还提供了PlayerPrefs.DeleteKey (key : string)删除指定数据；PlayerPrefs.DeleteAll() 删除全部键 ;PlayerPrefs.HasKey (key : string)判断数据是否存在的方法；

	}

	/// <summary>
	/// 例子效果
	/// </summary>

	public void ParticleSystemMethod()
	{
		//例子效果特效 就是ParticleSystem类型的 
		// 
		////粒子效果
		//private ParticleSystem ps;
		//ps = transform.GetComponentInChildren<ParticleSystem>();
		//


	}









	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}

