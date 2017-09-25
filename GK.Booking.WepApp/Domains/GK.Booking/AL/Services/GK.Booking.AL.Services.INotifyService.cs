using System.Threading.Tasks;

namespace GK.Booking.AL.Services
{
	public interface INotifyService
	{
		void SendMessage(string subject, string message, string destAddress);
		void SendMessage(string subject, string message, string[] destAddressCollection);
		void Dispose();

	}
}
