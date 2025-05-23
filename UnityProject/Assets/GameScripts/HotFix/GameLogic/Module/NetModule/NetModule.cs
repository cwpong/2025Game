using Fantasy;
using Fantasy.Async;
using Fantasy.Network;
using Fantasy.Network.Interface;
using Fantasy.Platform.Unity;

namespace GameLogic
{
    public class NetModule : Singleton<NetModule>, IUpdate
    {
        private Scene m_Scene;
        private Session m_Session;

        public async void FantasyInit()
        {
            await Entry.Initialize(GetType().Assembly);
            m_Scene = await Entry.CreateScene();
            
            // 优化API
            m_Scene.Connect("127.0.0.1:20000", Fantasy.Network.NetworkProtocolType.KCP, 
                OnConnectSuccessResponse, OnConnectFailResponse, OnConnectDisConnectResponse, false, 6000);

            m_Session = m_Scene.Session;
            var com = m_Scene.Session.AddComponent<SessionHeartbeatComponent>();
            com.Start(1000);
        }


        private void OnConnectSuccessResponse()
        {
            Log.Debug("Fantasy 链接成功");
            Login();
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

        public async void Login()
        {
            TestLoginRequest req = new TestLoginRequest()
            {
                Account = "cwp",
                PassWord = "123456",
            };

            TestLoginResponse response = (TestLoginResponse)await m_Scene?.Session.Call(req);
            Log.Debug($"response = {response.ErrorCode}");
        }

        /// <summary>
        /// RPC请求
        /// </summary>
        /// <param name="req"></param>
        /// <param name="routeId"></param>
        /// <returns></returns>
        public async FTask<IResponse> SendCallMessage(IRequest req, long routeId = 0)
        {
            return await m_Session?.Call(req, routeId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="rpcId"></param>
        /// <param name="routeId"></param>
        public void Send(IMessage msg, uint rpcId = 0, long routeId = 0)
        {
            m_Session?.Send(msg, rpcId, routeId);
        }

        protected override void OnRelease()
        {
            base.OnRelease();

            m_Scene?.Dispose();
            m_Session?.Dispose();
        }
    }
}
