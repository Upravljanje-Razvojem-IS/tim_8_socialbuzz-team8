using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Models
{
    public class AuthenticationResponse
    {

        //User Registration and Controller Auth | ASP.NET Core 5 REST API Tutorial 10 15:00
        //Used in AuthService, AuthFailResponse and AuthSuccessResponse are used in Controller

        /// <summary>
        /// A generated token after successful authentication
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Bool value which indicates if autentication is successful
        /// </summary>
        public bool Succes { get; set; }

        /// <summary>
        /// List of authentication errors 
        /// </summary>
        public string Error { get; set; }
    }
}
