using System;
using System.IO;
using System.Xml;
using System.Reflection;
using System.Collections;
using System.Globalization;
using System.Configuration;
using System.Collections.Specialized;

namespace Configuration
{

	#region ConfigSettingsSectionHandler class
	public class ConfigSettingsSectionHandler : IConfigurationSectionHandler
	{
		static ConfigSettingsSectionHandler(){}

		public object Create(object parent, object configContext, System.Xml.XmlNode section)
		{
			NameValueCollection col = new NameValueCollection(
				new CaseInsensitiveHashCodeProvider(CultureInfo.InvariantCulture), 
				new CaseInsensitiveComparer(CultureInfo.InvariantCulture));

			foreach(XmlNode node in section.ChildNodes)
			{
				switch(node.Name)
				{	
					case "add":
						col.Add(node.Attributes["key"].Value, 
							node.Attributes["value"].Value);
						break;
				}
			}
			return col;
		}
	}
	#endregion

	public class ConfigSettings	
	{
		#region Private member variables
		private string _configfilename, _query, _sectionName;
		private XmlDocument _doc;
		private XmlTextWriter _writer;
		private XmlNodeList _nodes;
		private XmlElement  _appsettings, _node;
		private XmlAttribute _attr1, _attr2;
		private bool _bFileExists;
		#endregion

		#region Constructors
		public ConfigSettings()
		{
			if(File.Exists(Assembly.GetExecutingAssembly().ToString() + ".exe.config"))
			{
				this.ConfigFileName = Assembly.GetExecutingAssembly().ToString() + ".exe.config";
				_bFileExists = true;
			}
			else
			{
				_bFileExists = false;
			}
		}

		public ConfigSettings(string ConfigFileName)
		{
			if(File.Exists(ConfigFileName))
			{
				this.ConfigFileName = ConfigFileName;
				_bFileExists = true;
			}
			else
			{
				_bFileExists = false;
			}
		}
		#endregion
		
		#region Properties
		public string ConfigFileName
		{
			get{ return _configfilename;}
			set{ _configfilename = value;}
		}
		public XmlDocument XmlDocument
		{
			get{ return _doc;}
			set{ _doc = value;}
		}
		public XmlTextWriter Writer
		{
			get{ return _writer;}
			set{ _writer = value;}
		}
		public XmlNodeList XmlNodeList
		{
			get{ return _nodes;}
			set{ _nodes = value;}
		}
		public XmlElement AppSettingsNode
		{
			get{ return _appsettings;}
			set{ _appsettings = value;}
		}
		public XmlElement Node
		{
			get{ return _node;}
			set{ _node = value;}
		}
		public string Query
		{
			get{ return _query;}
			set{ _query = value;}
		}
		public XmlAttribute XmlAttribute1
		{
			get{ return _attr1;}
			set{ _attr1 = value;}
		}
		public XmlAttribute XmlAttribute2
		{
			get{ return _attr2;}
			set{ _attr2 = value;}
		}
		public string SectionName
		{
			get{ return _sectionName;}
			set{ _sectionName = value;}
		}
		public bool FileExists
		{
			get{return _bFileExists;}
			set{_bFileExists = value;}
		}
		#endregion

		#region Methods
		public NameValueCollection GetConfig()
		{
			return (NameValueCollection)ConfigurationSettings.GetConfig(SectionName);
		}

		public NameValueCollection GetConfig(string SectionName)
		{
			return (NameValueCollection)ConfigurationSettings.GetConfig(SectionName);
		}

		public string GetValue(string AttributeName)
		{
			NameValueCollection col = this.GetConfig();
			if(col[AttributeName] != null)
				return Convert.ToString(col[AttributeName].ToString());
			else
				return String.Empty;
	}

	public void SetValue(string AttributeName, string Value)
		{
			XmlDocument = new XmlDocument();
			XmlDocument.Load(this.ConfigFileName);
			Query = "configuration/" + SectionName;
			AppSettingsNode = (XmlElement)this.XmlDocument.SelectSingleNode(this.Query);
			if(AppSettingsNode == null)
				return;
			Query += "/add[@key='" + AttributeName.ToString() + "']";
			XmlNodeList = this.XmlDocument.SelectNodes(this.Query);
			if(XmlNodeList.Count > 0)
				Node = (XmlElement)XmlNodeList[0];
			else
			{
				Node = this.XmlDocument.CreateElement("add");
				XmlAttribute1 = this.XmlDocument.CreateAttribute("key");
				XmlAttribute1.Value = AttributeName.ToString();
				Node.Attributes.SetNamedItem(XmlAttribute1);
				XmlAttribute2 = this.XmlDocument.CreateAttribute("value");
				Node.Attributes.SetNamedItem(XmlAttribute2);
				AppSettingsNode.AppendChild(Node);
			}
			Node.Attributes["value"].Value = Value.ToString();
			this.XmlDocument.Save(this.ConfigFileName);
		}


		public void CreateConfigFile(string ConfigFileName, string sectionName)
		{
			FileStream file = new FileStream(ConfigFileName, System.IO.FileMode.Create);
			file.Close();
			Writer = new XmlTextWriter(ConfigFileName, System.Text.Encoding.Unicode);
			Writer.Formatting = Formatting.Indented;
			Writer.WriteRaw("<?xml version=\"1.0\" ?>\n");
			Writer.WriteRaw("<configuration>\n");
			Writer.WriteRaw("<configSections>\n");
			string str = String.Format("<section name={0} type={1} />\n", "\"" + sectionName.ToString() + "\"",
									  "\"Configuration.ConfigSettingsSectionHandler, Configuration\"");
			Writer.WriteRaw(str.ToString());
			Writer.WriteRaw("</configSections>\n");
			Writer.WriteRaw("<" + sectionName.ToString() + ">\n");
			Writer.WriteRaw("</" + sectionName.ToString() + ">\n");
			Writer.WriteRaw("</configuration>\n");
			Writer.Flush();
			Writer.Close();
		}
		#endregion

	}
}
