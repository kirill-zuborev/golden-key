namespace GK.Booking.AL.Services
{
	public interface IMessageService
	{
		void Send(string designation, string sender, string message);
	}
}
