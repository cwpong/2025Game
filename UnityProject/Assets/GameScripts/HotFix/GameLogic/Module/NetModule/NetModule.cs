using Fantasy;
using Fantasy.Platform.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class NetModule : Singleton<NetModule>, IUpdate
    {
        public Scene NetScene;

        public async void FantasyInit()
        {
            await Entry.Initialize();
            NetScene = await Entry.CreateScene();
            NetScene.Connect("127.0.0.1:20000", Fantasy.Network.NetworkProtocolType.KCP, OnConnectSuccessResponse, OnConnectFailResponse, OnConnectDisConnectResponse, false);
        }

        private void OnConnectSuccessResponse()
        {
            Log.Debug("Fantasy 链接成功");
        }

        private void OnConnectFailResponse()
        {
            Log.Debug("Fantasy 链接失败");
        }

        private void OnConnectDisConnectResponse()
        {
            Log.Debug("Fantasy 断开链接");
        }

        public void OnUpdate()
        {

        }
    }
}
