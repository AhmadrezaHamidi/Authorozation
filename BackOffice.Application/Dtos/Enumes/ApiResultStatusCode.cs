﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BackOffice.Application.Dtos.Enumes
{
    public enum ApiResultStatusCode
    {
        [Display(Name = "Success")]
        Success = 200,

        [Display(Name = "Server Error")]
        ServerError = 500,

        [Display(Name = "Bad Request Error")]
        BadRequest = 400,

        [Display(Name = "Forbidden Error")]
        Forbidden = 403,

        [Display(Name = "Not Found")]
        NotFound = 404,

        [Display(Name = "Empty Error")]
        ListEmpty = 404,

        [Display(Name = "Process Error")]
        LogicError = 500,

        [Display(Name = "Authentication Error")]
        UnAuthorized = 401,

        [Display(Name = "Not Acceptable")]
        NotAcceptable = 406,

        [Display(Name = "Failed Dependency")]
        FailedDependency = 424
    }
}
