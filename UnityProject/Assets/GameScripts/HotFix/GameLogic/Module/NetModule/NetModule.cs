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
        }

        public void OnUpdate()
        {

        }
    }
}
