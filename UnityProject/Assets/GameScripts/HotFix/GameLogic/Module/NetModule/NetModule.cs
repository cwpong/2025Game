using Fantasy;
using Fantasy.Platform.Unity;

namespace GameLogic
{
    public class NetModule : Singleton<NetModule>, IUpdate
    {
        public Scene NetScene;

        public async void FantasyInit()
        {
            await Entry.Initialize();
            NetScene = await Entry.CreateScene();
            NetScene.Connect("127.0.0.1:20000", Fantasy.Network.NetworkProtocolType.KCP, 
                OnConnectSuccessResponse, OnConnectFailResponse, OnConnectDisConnectResponse, false, 6000);
        }

        private void OnConnectSuccessResponse()
        {
            Log.Debug("Fantasy 链接成功");
        }

        private void OnConnectFailResponse()
        {
            Log.Error("Fantasy 链接失败");
        }

        private void OnConnectDisConnectResponse()
        {
            Log.Error("Fantasy 断开链接");
        }

        public void OnUpdate()
        {

        }
    }
}
