using System.Collections.Generic;

namespace GK.Booking.AL.Services
{
	public interface IOrderService
	{
		OrderDTO PushOrder(PushOrderCommand order);
		bool ConfirmOrder(int orderId, string customerSecretCode);
		void ConfirmOrderToReady(int orderId);
		void ReadyOrderToConfirm(int orderId);
		void CompleteOrder(int orderId);
		void DenyOrder(int orderId);
		void InvalidateOrder(int orderId);
		OrderDTO GetOrder(int id);
		IReadOnlyCollection<OrderDTO> GetTodayActiveOrders();
		void Dispose();
	}
}
