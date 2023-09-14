using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    }
}
