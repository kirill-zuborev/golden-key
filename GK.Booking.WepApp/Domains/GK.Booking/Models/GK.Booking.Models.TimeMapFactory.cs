using System;
using System.Linq;
using Microsoft.Practices.Unity;
using GK.Booking.Infrastructure.Configuration;
using GK.Booking.Infrastructure.Tools;


namespace GK.Booking.Models
{
	public class TimeMapFactory
	{
		private readonly IUnityContainer _container;

		private volatile TimeMap _timeMapInstance;
		private object _syncRoot = new Object();

		public TimeMapFactory(IUnityContainer container)
		{
			if (container == null)
			{
				throw new ArgumentNullException(nameof(container));
			}

			_container = container;
		}

		public TimeMap GetTimeMap()
		{
			if (_timeMapInstance == null || IsTimeMapExpired())
			{
				lock (_syncRoot)
				{
					if (_timeMapInstance == null || IsTimeMapExpired())
					{
						IOrderUnitOfWork database = _container.Resolve<IOrderUnitOfWork>();
						IConfig config = _container.Resolve<IConfig>();

						DateTime todayStartDate = DateTimeHelper.GetStartDateOfToday();
						DateTime todayEndDate = DateTimeHelper.GetEndDateOfToday();

						var todayOrdersMap = database.Orders.Find(o =>
							o.TargetStartDate >= todayStartDate
								&& o.TargetEndDate <= todayEndDate
								).ToList<Order>();

						var timeMapConfig = new TimeMapConfig(config.WorkingDayStartTime.ToTimeSpan(),
																config.WorkingDayEndTime.ToTimeSpan(),
																config.TimeMapTraySize.ToTimeSpan(),
																config.CookingDelayTime.ToTimeSpan(),
																config.OrderExpireTime.ToTimeSpan(),
																config.TimeMapRules.ToList<TimeMapRule>());

						_timeMapInstance = new TimeMap(todayOrdersMap, timeMapConfig);

						database.Dispose();
					}
				}
			}

			return _timeMapInstance;
		}

		private bool IsTimeMapExpired()
		{
			if ((DateTimeHelper.GetCurrentApplicationDateTimeValue() - _timeMapInstance.CreationDate).TotalDays >= 1)
			{
				return true;
			}

			return false;
		}
	}
}