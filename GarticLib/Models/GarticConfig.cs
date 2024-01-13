// ReSharper disable IdentifierTypo

namespace GarticLib.Models
{
	/// <summary>
	/// 首页配置
	/// </summary>
	public class GarticConfig
	{
		public Props Props { get; set; }
	}

	/// <summary>
	/// 配置数据
	/// </summary>
	public class Props
	{
		public Data Data { get; set; }
	}

	/// <summary>
	/// 数据内容
	/// </summary>
	public class Data
	{
		public Language[] Languages { get; set; }
		public Subject[] Subjects { get; set; }
		public UserInfo userInfo { get; set; }
	}

	/// <summary>
	/// 用户信息
	/// </summary>
	public class UserInfo
	{
	    public int Id { get; set; }
	    public string Code { get; set; }
	    public string Nome { get; set; }
	    public int Language { get; set; }
	    public string Versao { get; set; }
	    public bool Logado { get; set; }
	    public string Avatar { get; set; }
	}
	
	/// <summary>
	/// 语言信息
	/// </summary>
	public class Language
	{
	    public int Id { get; set; }
	    public string Name { get; set; }
	    public string Iso { get; set; }
	    public int Active { get; set; }
	    public int?[] Subjects { get; set; }
	}
	
	/// <summary>
	/// 主题信息
	/// </summary>
	public class Subject
	{
	    public int Id { get; set; }
	    public string Name { get; set; }
	}
}