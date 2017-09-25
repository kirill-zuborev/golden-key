using System;
using System.Collections.Generic;
using GK.Booking.AL.Services;
using GK.Booking.Infrastructure.Tools;
using GK.Booking.Models.Exceptions;
using GK.Booking.Infrastructure.Configuration;
using GK.Booking.WepApp.Domains.GK.Booking.Resources;

namespace GK.Booking.AL
{
	public class BookingApplication
	{
		private readonly IOrderService _orderService;
		private readonly IMessageService _messageService;
		private readonly IConfigService _configService;
		private readonly IConfig _config;

		public BookingApplication(IOrderService orderService, IMessageService smsService, IConfigService configService, IConfig config)
		{
			if (orderService == null)
			{
				throw new ArgumentNullException(nameof(orderService));
			}

			_orderService = orderService;

			if (smsService == null)
			{
				throw new ArgumentNullException(nameof(smsService));
			}

			_messageService = smsService;

			if (configService == null)
			{
				throw new ArgumentNullException(nameof(configService));
			}

			_configService = configService;

			if (config == null)
			{
				throw new ArgumentNullException(nameof(config));
			}

			_config = config;
		}

		public OrderDTO PushOrder(PushOrderCommandDTO pushOrderCommandDTO)
		{
			if (pushOrderCommandDTO == null)
			{
				throw new ArgumentNullException(nameof(pushOrderCommandDTO));
			}

			OrderDTO createdOrder;

			var pushOrderCommand = new PushOrderCommand(PhoneNumberHelper.GetNumberWithCountryCode(pushOrderCommandDTO.PhoneNumber),
														DateTimeHelper.GetAsApplicationTime(pushOrderCommandDTO.TargetStartTime, _configService.GetClientTimeShiftMinutes()),
														DateTimeHelper.GetAsApplicationTime(pushOrderCommandDTO.TargetEndTime, _configService.GetClientTimeShiftMinutes()),
														pushOrderCommandDTO.MenuItemsNames);

			try
			{
				createdOrder = _orderService.PushOrder(pushOrderCommand);
			}
			catch (PhoneIsLockedException ex)
			{
				throw new BookingDomainValidationException(ErrorCodesConst.PHONE_NUMBER_LOCKED, Messages_RU.PHONE_NUMBER_LOCKED, ex);
			}
			catch (IncorrectPhoneNumberException ex)
			{
				throw new BookingDomainValidationException(ErrorCodesConst.INVALID_PHONE_NUMBER, Messages_RU.INVALID_PHONE_NUMBER, ex);
			}
			catch (MaximumValueExceededException ex)
			{
				throw new BookingDomainValidationException(ErrorCodesConst.ORDER_MAXIMUM_PRICE_EXCEEDED, Messages_RU.ORDER_MAXIMUM_PRICE_EXCEEDED, ex);
			}
			catch (TimeMapTrayFullException ex)
			{
				throw new BookingDomainValidationException(ErrorCodesConst.TARGET_TIME_MAP_TRAY_IS_FULL, Messages_RU.TARGET_TIME_MAP_TRAY_IS_FULL, ex);
			}
			catch (ApplicationException ex)
			{
				throw new ApplicationException("Order not been created.", ex);
			}

			try
			{
				_messageService.Send(createdOrder.CustomerPhoneNumber, _config.SmsSettings.Sender,
					String.Format(_config.SmsSettings.TextTemplate, createdOrder.CustomerSecretCode));
			}
			catch (LowBalanceException)
			{
				_orderService.InvalidateOrder(createdOrder.OrderID);
				FatalErrorNotifier.NotifyOnError(Messages_RU.SMS_LOW_BALANCE);
				throw new ApplicationException("Error while sending sms. Low balance. The order was canceled.");
			}
			catch (ClientConfigurationException ex)
			{
				_orderService.InvalidateOrder(createdOrder.OrderID);
				FatalErrorNotifier.NotifyOnError(string.Format(Messages_RU.SMS_INVALID_CONFIGURATION_PARAMETER, ex.InvalidConfigurationParameterName));
				throw new ApplicationException(string.Format("Error while sending sms. Invalid client configuration parameter: {0}. The order was canceled.", ex.InvalidConfigurationParameterName));
			}
			catch (IncorrectDestinationNumberException)
			{
				_orderService.InvalidateOrder(createdOrder.OrderID);
				throw new ApplicationException("Error while sending sms. Incorrect destination phone number. The order was canceled.");
			}
			catch (OtherServiceException)
			{
				_orderService.InvalidateOrder(createdOrder.OrderID);
				throw new ApplicationException("Error while sending sms. Other exception. The order was canceled.");
			}
			catch (ApplicationException ex)
			{
				_orderService.InvalidateOrder(createdOrder.OrderID);
				throw new ApplicationException("Error while sending sms. The order was canceled.", ex);
			}

			createdOrder.CustomerSecretCode = null;	//Because a secret
			return createdOrder;
		}

		public IEnumerable<OrderDTO> GetTodayActiveOrders()
		{
			try
			{
				return _orderService.GetTodayActiveOrders();
			}
			catch (ApplicationException ex)
			{
				throw new ApplicationException("Can not get orders from database.", ex);
			}
		}

		public bool ConfirmOrder(int orderId, string customerSecretCode)
		{
			try
			{
				return _orderService.ConfirmOrder(orderId, customerSecretCode);
			}
			catch (OrderExpiredException ex)
			{
				throw new BookingDomainValidationException(ErrorCodesConst.ORDER_EXPIRED, Messages_RU.ORDER_EXPIRED, ex);
			}
			catch (IncorrectOrderStatusException ex)
			{
				throw new ApplicationException("Order status is an incorrect.", ex);
			}
			catch (DataNotFoundException ex)
			{
				throw new ApplicationException("Requested order not been found.", ex);
			}
			catch (ApplicationException ex)
			{
				throw new ApplicationException("Order status not been updated.", ex);
			}
		}

		public void ConfirmOrderToReady(int orderId)
		{
			try
			{
				_orderService.ConfirmOrderToReady(orderId);
			}
			catch (IncorrectOrderStatusException ex)
			{
				throw new BookingDomainValidationException(ErrorCodesConst.CURRENT_STATUS_NOT_CORRECT_FOR_OPERATION, Messages_RU.CURRENT_STATUS_NOT_CORRECT_FOR_OPERATION, ex);
			}
			catch (DataNotFoundException ex)
			{
				throw new ApplicationException("Requested order not been found.", ex);
			}
			catch (ApplicationException ex)
			{
				throw new ApplicationException("Order status not been updated.", ex);
			}
		}

		public void ReadyOrderToConfirm(int orderId)
		{
			try
			{
				_orderService.ReadyOrderToConfirm(orderId);
			}
			catch (IncorrectOrderStatusException ex)
			{
				throw new BookingDomainValidationException(ErrorCodesConst.CURRENT_STATUS_NOT_CORRECT_FOR_OPERATION, Messages_RU.CURRENT_STATUS_NOT_CORRECT_FOR_OPERATION, ex);
			}
			catch (DataNotFoundException ex)
			{
				throw new ApplicationException("Requested order not been found.", ex);
			}
			catch (ApplicationException ex)
			{
				throw new ApplicationException("Order status not been updated.", ex);
			}
		}

		public void CompleteOrder(int orderId)
		{
			try
			{
				_orderService.CompleteOrder(orderId);
			}
			catch (IncorrectOrderStatusException ex)
			{
				throw new BookingDomainValidationException(ErrorCodesConst.CURRENT_STATUS_NOT_CORRECT_FOR_OPERATION, Messages_RU.CURRENT_STATUS_NOT_CORRECT_FOR_OPERATION, ex);
			}
			catch (DataNotFoundException ex)
			{
				throw new ApplicationException("Requested order not been found.", ex);
			}
			catch (ApplicationException ex)
			{
				throw new ApplicationException("Order status not been updated.", ex);
			}
		}

		public void DenyOrder(int orderId)
		{
			try
			{
				_orderService.DenyOrder(orderId);
			}
			catch (DataNotFoundException ex)
			{
				throw new ApplicationException("Requested order not been found.", ex);
			}
			catch (ApplicationException ex)
			{
				throw new ApplicationException("Order status not been updated.", ex);
			}
		}

		public void Dispose()
		{
			_orderService.Dispose();
		}
	}
}