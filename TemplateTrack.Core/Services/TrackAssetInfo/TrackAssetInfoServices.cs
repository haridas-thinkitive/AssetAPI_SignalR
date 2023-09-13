using Azure.Core.GeoJson;
using GeoCoordinatePortable;
using GoogleMaps.LocationServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateTrack.Core.Data;
using TemplateTrack.Core.Interface.TrackAssetInfo;
using TemplateTrack.DataAccess.Model.AssetManagement;
using TemplateTrack.DataAccess.Model.Hubs;
using TemplateTrack.DataAccess.Model.NewFolder;
using TemplateTrack.DataAccess.Model.TraceInfo;
using TemplateTrack.DataAccess.Model.TrackingInfo;


namespace TemplateTrack.Core.Services.TrackAssetInfo
{
    public class TrackAssetInfoServices : ITrackAssetInfo
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpClient _httpClient;
        private readonly IHubContext<TableDataHub> _hubContext;


        public TrackAssetInfoServices(ApplicationDbContext context, HttpClient httpClient,IHubContext<TableDataHub> hubContext)
        {
            _context = context;
            _httpClient = httpClient;
            _hubContext = hubContext;

        }


        public async Task<List<AssetTrackingInfo>> GetAllTrackInfo()
        {
            List<AssetTrackingInfo> result = null;
            try
            {
                result = await _context.assetTrackingInfos.ToListAsync();
                await _hubContext.Clients.All.SendAsync("ReceiveTrackInfo", result);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;

        }

        public async Task<AssetTrackingInfo> TrackSpecificAsset(int id)
        {
            var result = await _context.assetTrackingInfos.FirstOrDefaultAsync(x => x.TrackingId == id);

            if (result != null)
            {
                result.Latitude = GetUpdatedLatitude();
                result.Longitude = GetUpdatedLongitude();

                return result;
            }
            else
            {
                return null;
            }
        }


        //public async Task<string> GetLocation(double latitude, double longitude)
        //{
        //    try
        //    {
        //        string apiUrl = $"https://nominatim.openstreetmap.org/reverse?format=json&lat={latitude}&lon={longitude}";

        //        HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);


        //        if (response.IsSuccessStatusCode)
        //        {
        //            string jsonResponse = await response.Content.ReadAsStringAsync();
        //            var jsonObject = JObject.Parse(jsonResponse);

        //            // Extract location information (address) from the response
        //            string formattedAddress = jsonObject["display_name"]?.ToString();
        //            return formattedAddress ?? "Location not found";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return "Error: Unable to retrieve location";
        //    }

        //    return "Location not found";
        //}


        public async Task<string> GetLocation(double latitude, double longitude)
        {
            try
            {
                string apiUrl = $"https://nominatim.openstreetmap.org/reverse?format=json&lat={latitude}&lon={longitude}"; // API URL return the location


                _httpClient.DefaultRequestHeaders.Add("User-Agent", "YourAppName/1.0 (.NET Core; Windows 10; en-IN)");

                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);


                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var jsonObject = JObject.Parse(jsonResponse);

                    string formattedAddress = jsonObject["display_name"]?.ToString(); //extract address

                    string[] arr = formattedAddress.Split(",");


                    return formattedAddress ?? "Location not found";
                }
                else
                {
                    int statusCode = (int)response.StatusCode;
                    string responseContent = await response.Content.ReadAsStringAsync();
                    return "Location not found";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AssetTrackingInfo> PostAssetLocation(string barcode)
        {
            try
            {
                var ifExist = _context.assetTraceIfos.Where(x => x.BarCode == barcode);
                if (!ifExist.Any())
                {

                    double latitude;
                    double longitude;
                    if (!string.IsNullOrEmpty(barcode))
                    {
                        var result = await _context.assetTrackingInfos.FirstOrDefaultAsync(x => x.BarCode == barcode);
                        if (result != null)
                        {
                            latitude = result.Latitude;
                            longitude = result.Longitude;
                        }
                        else
                        {
                            return null;
                        }

                        string apiUrl = $"https://nominatim.openstreetmap.org/reverse?format=json&lat={latitude}&lon={longitude}"; // API URL return the location


                        _httpClient.DefaultRequestHeaders.Add("User-Agent", "YourAppName/1.0 (.NET Core; Windows 10; en-IN)");

                        HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);


                        if (response.IsSuccessStatusCode)
                        {
                            string jsonResponse = await response.Content.ReadAsStringAsync();
                            var jsonObject = JObject.Parse(jsonResponse);

                            string formattedAddress = jsonObject["display_name"]?.ToString(); //extract address

                            string[] arr = formattedAddress.Split(',');

                            //Added record from asset tracking info to assettraceinfo

                            if (arr.Length >= 5)
                            {
                                var res = new AssetTraceIfo
                                {
                                    BarCode = result.BarCode,
                                    Name = result.Name,
                                    Latitude = result.Latitude,
                                    Longitude = result.Longitude,



                                    landMark = "AB",
                                    District = arr[0],
                                    State = arr[1],
                                    Country = arr[2],
                                    PostalCode = arr[3],
                                    City = arr[4],

                                };
                                _context.assetTraceIfos.Add(res);
                                _context.SaveChanges();
                                return result;

                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            int statusCode = (int)response.StatusCode;
                            string responseContent = await response.Content.ReadAsStringAsync();
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }

                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AssetTraceIfo> GetAssetLocation(string barcode)
        {
            try
            {

                    if (!string.IsNullOrEmpty(barcode))
                    {
                        var result = await _context.assetTraceIfos.FirstOrDefaultAsync(x => x.BarCode == barcode);
                        return result;
                    }
                    else
                    {
                        return null;
                    }

            }catch (Exception ex)
            {
                throw ex;
            }
        }

        private double GetUpdatedLatitude()
        {
            return new Random().NextDouble() * 90;
        }

        private double GetUpdatedLongitude()
        {
            return new Random().NextDouble() * 180;
        }

        public async Task<int> DeleteTrackInfo(int id)
        {
            try
            {
                var assetToDelete = await _context.assetTrackingInfos.FindAsync(id);
                if (assetToDelete != null)
                {
                    _context.assetTrackingInfos.Remove(assetToDelete);
                    await _context.SaveChangesAsync();
                    await _hubContext.Clients.All.SendAsync("AssetDeleted", id);

                    return 1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

