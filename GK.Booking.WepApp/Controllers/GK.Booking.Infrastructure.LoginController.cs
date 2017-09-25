using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using GK.Booking.Infrastructure.Configuration;
using GK.Booking.AL;
using GK.Booking.Filters;

namespace GK.Booking.WebApp
{
	public class LoginController : Controller
	{
		private IAuthenticationManager AuthenticationManager
		{
			get
			{
				return HttpContext.GetOwinContext().Authentication;
			}
		}

		private readonly IConfig _config;

		private const string DEFAULT_REDIRECT_PUTH = "/link";
		private const string LOGIN_REDIRECT_PUTH = "/login";

		public LoginController(IConfig config)
		{
			if (config == null)
			{
				throw new ArgumentNullException(nameof(config));
			}

			_config = config;
		}

		[HttpPost]
		[HandleExceptions]
		//[ValidateAntiForgeryToken]
		public ActionResult Login(LoginDataModel model, string ReturnUrl)
		{
			if (_config.Credentials.User.Equals(model.Login, StringComparison.InvariantCultureIgnoreCase)
				&& _config.Credentials.Password.Equals(GetEncryptedString(model.Password), StringComparison.InvariantCulture))
			{
				ClaimsIdentity claim = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
				claim.AddClaim(new Claim(ClaimTypes.NameIdentifier, _config.Credentials.User, ClaimValueTypes.String));
				claim.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, _config.Credentials.User, ClaimValueTypes.String));
				claim.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
					"OWIN Provider", ClaimValueTypes.String));

				AuthenticationManager.SignOut();
				AuthenticationManager.SignIn(new AuthenticationProperties
				{
					IsPersistent = model.IsPersistent
				}, claim);

				//Get prev. path from Referrer
				if (!string.IsNullOrEmpty(Request.UrlReferrer.Query))
				{
					var queryParams = HttpUtility.ParseQueryString(Request.UrlReferrer.Query);

					string redirectPath = queryParams["ReturnUrl"];
					return Json(new { redirectTo = redirectPath });
				}

				return Json(new { redirectTo = DEFAULT_REDIRECT_PUTH });
			}
			else
			{
				return Json(new ErrorInfoDTO { ErrorCode = ErrorCodesConst.WRONG_USER_CREDENTIALS, ErrorMessage = "Login/Password didn't match" });
			}
		}

		[HandleExceptions]
		public ActionResult Logout()
		{
			AuthenticationManager.SignOut();
			return Json(new { redirectTo = LOGIN_REDIRECT_PUTH });
		}

		public class LoginDataModel
		{
			[Required]
			public string Login { get; set; }
			[Required]
			[DataType(DataType.Password)]
			public string Password { get; set; }
			public bool IsPersistent { get; set; }
		}

		private string GetEncryptedString(string inputString)
		{
			MD5 md5Hasher = MD5.Create();

			byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(inputString));

			StringBuilder sBuilder = new StringBuilder();

			for (int i = 0; i < data.Length; i++)
			{
				//in hex string is two characters long
				sBuilder.Append(data[i].ToString("x2"));
			}

			return sBuilder.ToString();
		}
	}
}