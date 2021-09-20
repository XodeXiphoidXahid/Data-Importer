using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Models.ReCaptcha
{
    public interface IGoogleReCaptchaService
    {
        bool ReCaptchaPassed(string stringValues);
    }
}
