using System;
using System.Configuration;

namespace GK.Booking.Infrastructure.Configuration
{
	public class TimeMapRulesConfigSection : ConfigurationSection
	{
		[ConfigurationProperty("rules")]
		public TimeMapRulesCollection TimeMapRules
		{
			get
			{
				return this["rules"] as TimeMapRulesCollection;
			}
		}
	}

	public class TimeMapRulesCollection : ConfigurationElementCollection
	{
		protected override ConfigurationElement CreateNewElement()
		{
			return new TimeMapRule();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((TimeMapRule)element).Id;
		}
	}

	public class TimeMapRule : ConfigurationElement
	{
		[ConfigurationProperty("id", IsRequired = true, IsKey = true)]
		public int Id
		{
			get
			{
				try
				{
					return Int32.Parse(this["id"].ToString());
				}
				catch (FormatException ex)
				{
					throw new FormatException("Invalid format data in Id config parameter in Time Map Rules.", ex);
				}
				catch (OverflowException ex)
				{
					throw new OverflowException("Incorrect MaxOrders value in Id config parameter in Time Map Rules.", ex);
				}
			}

			set
			{
				this["id"] = value;
			}
		}
		[ConfigurationProperty("startTime", IsRequired = true)]
		public TimeSpan StartTime
		{
			get
			{
				try
				{
					return TimeSpan.Parse(this["startTime"].ToString());
				}
				catch (FormatException ex)
				{
					throw new FormatException("Invalid format data in Start Time config parameter in Time Map Rules.", ex);
				}
				catch (OverflowException ex)
				{
					throw new OverflowException("Incorrect minutes/seconds value in Start Time config parameter. in Time Map Rules", ex);
				}
			}

			set
			{
				this["startTime"] = value;
			}
		}

		[ConfigurationProperty("endTime", IsRequired = true)]
		public TimeSpan EndTime
		{
			get
			{
				try
				{
					return TimeSpan.Parse(this["endTime"].ToString());
				}
				catch (FormatException ex)
				{
					throw new FormatException("Invalid format data in End Time config parameter in Time Map Rules.", ex);
				}
				catch (OverflowException ex)
				{
					throw new OverflowException("Incorrect minutes/seconds value in End Time config parameter in Time Map Rules.", ex);
				}
			}

			set
			{
				this["endTime"] = value;
			}
		}

		[ConfigurationProperty("maxOrders", IsRequired = true)]
		public int MaxOrders
		{
			get
			{
				try
				{
					return Int32.Parse(this["maxOrders"].ToString());
				}
				catch (FormatException ex)
				{
					throw new FormatException("Invalid format data in Max Orders config parameter in Time Map Rules.", ex);
				}
				catch (OverflowException ex)
				{
					throw new OverflowException("Incorrect MaxOrders value in MaxOrders config parameter in Time Map Rules.", ex);
				}
			}
			set
			{
				this["maxOrders"] = value;
			}
		}
	}
}