 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using CatsWebApp2.Models;
using CatsWebApp2.Utilities; 


namespace CatsWebApp2.Controllers
{

    public class HomeController : Controller
    {
        /// <summary>
        /// The landing page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Called by the UI to generate the results in JSON format
        /// </summary>
        /// <returns>the result/error in JSON</returns>
        public JsonResult GenerateResults()
        {
            try
            {
                // The web service URL is in the config
                var wsa = WebConfigurationManager.AppSettings["WebServiceAddress"];
                return GenerateResults(wsa);
            }
            catch (Exception exception)
            {
                // Exception handling (hide actual exception from end user)
                var msg = $"Exception {exception}";
                Logger.WriteLine(msg, Logger.LogLevel.Error);

                // Just send a generic error to the user
                var errobj = CreateErrorObject("Error extracting information");
                return Json(errobj, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// The internal method for processing
        /// </summary>
        /// <param name="wsa">the web service address</param>
        /// <returns>the json result</returns>
        /// <remarks>Refer to AssemblyInfo.cs for method visibility</remarks>
        internal JsonResult GenerateResults(string wsa)
        {
            try
            {
                // Extract the input json
                using (var data = DataExtractor.ExtractData(wsa))
                {
                    data.Wait();

                    // Deserialize it to a C# readable object
                    var obj = DataExtractor.DeserializeObject(data.Result).ToList();

                    // As required - get the cats allocated to male and female owners (if you need another animal add a pet type)
                    var males = DataExtractor.GetPetsByOwnerGender(obj, OwnerGender.Male);
                    var females = DataExtractor.GetPetsByOwnerGender(obj, OwnerGender.Female);

                    // Collect the result (no errors)
                    var result = new
                    {
                        Data = new List<Output> { males, females },
                        HasError = false,
                        ErrorMessage = string.Empty
                    };

                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception exception)
            {
                // Exception handling (hide actual exception from end user)
                var msg = $"Exception {exception}";
                System.Diagnostics.Debug.WriteLine(msg);
                Logger.WriteLine(msg, Logger.LogLevel.Error);

                // Just send a generic error to the user
                var errobj = CreateErrorObject("Error extracting information");
                return Json(errobj, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Create an error oject
        /// </summary>
        /// <param name="errorMessage">the error message</param>
        /// <returns>the object</returns>
        private static dynamic CreateErrorObject(string errorMessage)
        {
            return new
            {
                HasError = true,
                ErrorMessage = errorMessage
            };
        }
    }
}