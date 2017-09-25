using System;
using GK.Booking.AL.Services;
using GK.Booking.Infrastructure.Tools;

namespace GK.Booking.AL
{
    public class TimeQueueApplication
    {
        private readonly ITimeMapService _timeMapService;

        public TimeQueueApplication(ITimeMapService timeMapService)
        {
            if (timeMapService == null)
            {
                throw new ArgumentNullException("Time map services is not initialized");
            }

            _timeMapService = timeMapService;
        }

        public TimeMapDTO GetTimeMap()
        {
            try
            {
                var timeMap = _timeMapService.GetTimeMap();

                return timeMap;
            }
            catch(ApplicationException ex)
            {
                throw new ApplicationException("Exception while creating Time Map", ex);
            }
        }

        public bool IsTimeMapTrayEmpty(DateTime startDate, DateTime endDate)
        {
            startDate = DateTimeHelper.ToApplicationDateTime(startDate);
            endDate = DateTimeHelper.ToApplicationDateTime(endDate);

            try
            {
                return _timeMapService.IsTimeMapTrayEmpty(startDate, endDate);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new ApplicationException("Requested Start Date value greather than End Date value", ex);
            }
            catch(ApplicationException ex)
            {
                throw new ApplicationException("Exception while checking Time Map Tray state.", ex);
            }
        }

        public void Dispose()
        {
            _timeMapService.Dispose();
        }
    }
}