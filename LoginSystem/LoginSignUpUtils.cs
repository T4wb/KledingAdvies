using System;
using Android.Widget;

using Android.Util;

namespace UI
{
    public static class LoginSignUpUtils
	{
		public static void ShowTextviewError (TextView textview, string errorMessage)
		{
			// shows error message in the corresponding textview
			textview.RequestFocus ();
			textview.Error = errorMessage;
		}

        public static Boolean IsValidEmail(string email)
        {
            return Patterns.EmailAddress.Matcher(email).Matches();
        }

        public static bool isValidPassword(string txtPasswordRegister)
        {
            return txtPasswordRegister.Length > 11;
        }
    }
}

