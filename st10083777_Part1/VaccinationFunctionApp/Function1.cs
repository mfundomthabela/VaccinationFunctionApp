using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Web.Http;

namespace Vaccination_status_Query
{
    public static class Function1
    {
        [FunctionName("ST10083777")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string vacID = req.Query["ID"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            vacID = vacID ?? data?.vacID;


            int vacIDint;
            if (int.TryParse(vacID, out vacIDint))
            {
                string Vac = VaccinationIDNumbers(vacIDint);
                string responseMesssage = $"Hello, your vaccination status is:  " + Vac;
                return new OkObjectResult(responseMesssage);
            }
            else
            {
                string resposneMessage = "Good day, Enter the vaccination ID on the above address tab";
                return new BadRequestObjectResult(resposneMessage);
            }

        }
        public static string VaccinationIDNumbers(int ID)
        {
            int vcID001 = 1234567891;
            int vcID002 = 234567892;
            int vcID003 = 1123456789;
            if (ID == vcID001) return "ID number: ID1 \n" +
                    "vaccination status: vaccinated\n" +
                    "Vaccination place: netbay hospital\n";
            if (ID == vcID002) return "ID number: ID2 \n" +
                    "vaccination status: has not vaccinated\n" +
                    "Vaccination place: edward hospital\n";
            if (ID == vcID003) return "\nID number: ID3 \n" +
                    "vaccination status: has to vaccinate\n" +
                    "Vaccination place: king shaka hospital\n";
           

            return "Enter vaccination ID number";
        }
    }
}
