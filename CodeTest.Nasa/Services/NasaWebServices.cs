using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Threading.Tasks;
using CodeTest.Nasa.Communication;
using CodeTest.Nasa.Models;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace CodeTest.Nasa.Services
{
    public class NasaWebServices
    {
        private readonly string _nasaBaseUrl = "https://api.nasa.gov/mars-photos/api/v1/rovers/{0}/";
        private readonly string _nasaApiKey = "C8LPzICScFCD5DtuQBhrVj5bm5Qs8ApNN7ysQqxe";
        public CallResponse GetImagesOnDate(string dateTime, string rover)
        {
            var httpCaller = new HttpCaller();
            var url = string.Format(_nasaBaseUrl, rover);
            var parameters = new Dictionary<string, string>()
                {
                    {"earth_date",dateTime},
                    {"api_key", _nasaApiKey},
                    {"page", "1"},
                    {"sol", "1"},
                };
            var response = httpCaller.Call(url, "photos", parameters);
            return response;
        }

        public void DownloadImages(List<Photo> photos, string destinationFolder)
        {
            if (!Directory.Exists(destinationFolder))
                Directory.CreateDirectory(destinationFolder);

            Task.Run(() =>
            {
                using (WebClient client = new WebClient())
                {
                    foreach (var photo in photos)
                    {
                        var fileName = Path.GetFileName(photo.img_src);
                        var destinationFilePath = Path.Join(destinationFolder, fileName);
                        if (!File.Exists(destinationFilePath))
                            client.DownloadFile(new Uri(photo.img_src), destinationFilePath);
                    }
                }
            });


        }
    }
}
