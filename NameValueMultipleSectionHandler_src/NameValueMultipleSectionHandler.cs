using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Xml;

namespace DigBang.Configuration
{
	public class NameValueMultipleSectionHandler: IConfigurationSectionHandler
	{
		static Type readOnlyNameValueCollectionType = null;
		static ConstructorInfo readOnlyNameValueCollectionConstructor1 = null;
		static ConstructorInfo readOnlyNameValueCollectionConstructor2 = null;

		static NameValueMultipleSectionHandler()
		{
			readOnlyNameValueCollectionType = typeof(NameValueCollection).
				Assembly.GetType("System.Configuration.ReadOnlyNameValueCollection");
			if (readOnlyNameValueCollectionType != null)
			{
				readOnlyNameValueCollectionConstructor1 = 
					readOnlyNameValueCollectionType.GetConstructor(
					new Type[] {readOnlyNameValueCollectionType});

				readOnlyNameValueCollectionConstructor2 = 
					readOnlyNameValueCollectionType.GetConstructor(
					new Type[] {typeof(IHashCodeProvider), typeof(IComparer)});
			}
		}

		static NameValueCollection CreateCollection(object parent)
		{
			if (parent == null)
			{
				return 
					(NameValueCollection)readOnlyNameValueCollectionConstructor2.Invoke(
					new object[] {
									 new CaseInsensitiveHashCodeProvider(
									 CultureInfo.InvariantCulture),
									 new CaseInsensitiveComparer(
									 CultureInfo.InvariantCulture)});
			}
			else
			{
				return 
					(NameValueCollection)readOnlyNameValueCollectionConstructor1.Invoke(
					new object[] {parent});

			}
		}

		static NameValueCollection GetConfig(string sectionName)
		{
			return (NameValueCollection)ConfigurationSettings.GetConfig(sectionName);
		}

		public object Create(object parent, object context, XmlNode section)
		{
			NameValueCollection collection = null;

			if (readOnlyNameValueCollectionType != null)
			{
				collection = CreateCollection(parent);
				foreach (XmlNode xmlNode in section.ChildNodes)
				{
					if (xmlNode.NodeType == XmlNodeType.Element)
					{
						switch (xmlNode.Name)
						{
							case "add":
								collection.Add(xmlNode.Attributes["key"].Value,
									xmlNode.Attributes["value"].Value);
								break;
							case "remove":
								collection.Remove(xmlNode.Attributes["key"].Value);
								break;
							case "clear":
								collection.Clear();
								break;
						}
					}
				}
			}
			return collection;
		}
	}
}
