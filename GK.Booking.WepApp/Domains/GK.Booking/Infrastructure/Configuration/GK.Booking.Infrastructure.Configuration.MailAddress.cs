using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace GK.Booking.Infrastructure.Configuration
{
	public class MailAddressesConfigSection : ConfigurationSection
	{
		[ConfigurationProperty("devGroup")]
		public AddressGroupCollection DevGroup
		{
			get
			{
				return this["devGroup"] as AddressGroupCollection;
			}
		}

		[ConfigurationProperty("adminGroup")]
		public AddressGroupCollection AdminGroup
		{
			get
			{
				return this["adminGroup"] as AddressGroupCollection;
			}
		}
	}

	public class AddressGroupCollection : ConfigurationElementCollection
	{
		protected override ConfigurationElement CreateNewElement()
		{
			return new MailAddress();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((MailAddress)element).Name;
		}
	}

	public class MailAddress : ConfigurationElement
	{
		[ConfigurationProperty("name", IsRequired = true, IsKey = true)]
		public string Name
		{
			get
			{
				return this["name"] as string;
			}
		}

		[ConfigurationProperty("address", IsRequired = true)]
		public string Address
		{
			get
			{
				return this["address"] as string;
			}
		}
	}
}