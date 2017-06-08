using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System ;
using Mono.Data.Sqlite;

public class SqliteMangeToCSharp : MonoBehaviour {

	//统一存在在Application.dataPath大文件夹下



	// sqlite单例
	public static SqliteMangeToCSharp _instance ;
	public static SqliteMangeToCSharp GetInstance()
	{
		if (_instance == null) {
		
			_instance = new SqliteMangeToCSharp ();
		}
		return _instance ;

	}

	// 数据库连接类
	SqliteConnection con ;

	// 创建表类
	public SqliteCommand command ;

	// 创建的数据库名和表名、存的数据
	public string DatabaseName ;




	bool isOpen = false;

	void Awake()
	{
	}

	void Start()
	{

		// 创数据库名 和表、存储数据类型
		//string path = "/Data/lzy.db";
		//string tableName = "LZY(age integer primary key autoincrement,name text,score integer)";
		//CreateDatabase(path,tableName);


		// 增加数据
		//string tableNameAndData = "LZY(name,score) values('张建楠',59)";
		//InsertData(tableNameAndData,path);

		// 查找数据
		// 2.1 查找所有数据
		// 2.2 根据条件查找数据

		// 2.1
		//string tableNameDataAll = "LZY";
		//selectTaleDataAll(tableNameDataAll,path);

		// 2.2
		//string tableNameDataAndCondition = "USER where name="+" '"+NameField.text + "'";
		//selectTableDataCondition(tableNameDataAndCondition,path);



		// 删除数据 
		// 1.1 删除数据库
		// 1.2 根据条件删除数据

		// 1.1
		//string dataName = "LZY" ;
		//dropTableData(dataName);

		// 1.2
		// TableName 是表名 
		// //string tableNameData ="LZY where name = '刘籽繇'";
		//deleteTableData(tableNameData);


		// 修改数据
		//string tableNmaeDataUpdate = "LZY set name = '小' where name='小龙'";
		//updateTableData(tableNmaeDataUpdate);



	}


	#region
	// 创建数据库 创建表
	// path 是创建数据库所在的路径和名字 如Assess文件夹下Data文件夹下数据库名为data.db
	// tableName 是创建表的名字和参数列表
	public void CreateDatabase(string path,string tableName)
	{
		con = new SqliteConnection("Data Source="+Application.dataPath+path);

		// 打开数据库
		con.Open();
		isOpen = true;
		// 创建表
		DatabaseName = "create table if not exists"+" "+tableName ;
		command = new SqliteCommand(DatabaseName,con);

		//执行命令
		Order();

	}
	#endregion

	#region
	// 增加数据
	// TableNameAndData 是表的名字和插入的具体数据 path是路径比如说 /Data/user.db
	public void InsertData(string tableNameAndData,string path)
	{
		con = new SqliteConnection("Data Source="+Application.dataPath+path);

		// 打开数据库
		con.Open();

		string sqlStr = "insert into"+" "+tableNameAndData;

		command = new SqliteCommand(sqlStr,con);

		Order();

	}
	#endregion


	#region
	// 2.2 
	// tableNameDataAndCondition是表明与查找的条件
	// path是数据库的路径
	// Example
	// string UserNameOne = "USER where name="+" '"+NameField.text + "'";
	// 开始查找 count =1 代表找到了一条相关的 代表已经注册 返回0代表没有注册
	//int count = SqliteManage.Instance.selectTableDataCondition(UserNameOne,path);

	public int selectTableDataCondition(string tableNameDataAndCondition,string path)
	{
		string sqlStr = "SELECT * FROM"+" "+tableNameDataAndCondition;

		con = new SqliteConnection ("Data Source=" + Application.dataPath + path);
		// 打开数据库 非常重要
		con.Open ();

		command = new SqliteCommand(sqlStr,con);
		int count = Convert.ToInt32(command.ExecuteScalar());
		Order();
		// 关闭数据库
		CloseDatabase ();
		// 根据count的返回值来判断当前账号存不存在 count等于1就代表有此账号
		return count ;
	}
	#endregion

	#region

	//string UserName = "USER where name="+" '"+NameField.text+"'";
	public SqliteDataReader selectTaleDataAll(string TableName,string path)
	{

		con = new SqliteConnection("Data Source="+Application.dataPath+path);
		// 打开数据库
		con.Open();
		string sqlStr = "SELECT * FROM"+" "+TableName;
		command = new SqliteCommand(sqlStr,con);
		SqliteDataReader reader = command.ExecuteReader ();

		// 在验证用户密码的时候用到
		//if (name == reader.GetString (1) && psw == reader.GetString (2))

		return reader;

	}
	#endregion

	#region

	// 1.1删除数据库
	// DataName 是数据库名
	public void DropTableData(string DataName)
	{
		string sqlStr= "drop database"+" "+DataName ;
		command = new SqliteCommand(sqlStr,con);
		Order();
	}


	#endregion


	#region
	// 修改指定的数据 比如说存了 age name 现在可以选择修改名字和年龄 
	// TableNameDataUpdate是表名 和要修改的数据 和新数据
	public void updateTableData(string TableNameDataUpdate)
	{

		string sqlStr = "update"+" "+TableNameDataUpdate ;
		command = new SqliteCommand(sqlStr,con);

		Order();
	}

	#endregion




	#region
	// 关闭数据库操作
	public void CloseDatabase()
	{
		if (con != null) {
			con.Close ();
		}
	}

	#endregion


	#region

	// 创 增 删 改 所要执行的命令
	public void Order()
	{
		command.ExecuteNonQuery();
		command.Dispose();
	}

	#endregion

}