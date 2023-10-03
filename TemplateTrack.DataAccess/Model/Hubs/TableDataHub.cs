using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateTrack.DataAccess.Model.BatchAsset;
using TemplateTrack.DataAccess.Model.NewFolder;
using TemplateTrack.DataAccess.Model.TrackingInfo;

namespace TemplateTrack.DataAccess.Model.Hubs
{
    public class TableDataHub : Hub
    {

        public async Task DeleteRecord(int id)
        {
            await Clients.All.SendAsync("ReceiveUpdate", id);
        }

        public async Task Getdata()
        {
            await Clients.All.SendAsync("ReceiveTrackInfo");
        }

        public async Task InsertRecord(AssetTrackingInfo assetTrackingInfo)
        {
            await Clients.All.SendAsync("ReceiveTableData", assetTrackingInfo);
        }
        /// <summary>
        /// SignalR for assetbatch Controller
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// 
        public async Task SendGetAllBatchUpdate()
        {
            await Clients.All.SendAsync("ReceiveGetAllBatchUpdate");
        }

        public async Task SendUpdateForSubmit(List<BatchAssetInfo> data)
        {
            await Clients.All.SendAsync("ReceiveUpdateForSubmit", data);
        }

        public async Task SendUpdateForSave(List<BatchAssetInfo> data)
        {
            await Clients.All.SendAsync("ReceiveUpdateForSave", data);
        }

        public async Task SendUpdateForSaveWithBatchCode(List<BatchAssetInfo> data)
        {
            await Clients.All.SendAsync("ReceiveUpdateForSaveWithBatchCode", data);
        }

        public async Task SendUpdateForAddBatch(List<BatchAssetInfo> data)
        {
            await Clients.All.SendAsync("ReceiveUpdateForAddBatch", data);
        }

    }
}
