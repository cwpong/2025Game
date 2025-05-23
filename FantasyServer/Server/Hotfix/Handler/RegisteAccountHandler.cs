using Fantasy;
using Fantasy.Async;
using Fantasy.Network;
using Fantasy.Network.Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotfix
{
    internal class RegisteAccountHandler : MessageRPC<C2A_RegisteAccountRequest, A2C_RegisteAccountResponse>
    {
        protected override async FTask Run(Session session, C2A_RegisteAccountRequest request, A2C_RegisteAccountResponse response, Action reply)
        {
            var accountCom = session.Scene.GetComponent<AccountRegistComponent>();

            // 注册账号
            var res = await accountCom.RegisteAccount(request.Account, request.PassWord);
            if (res == 10000)
            {
                Log.Info($"账号{request.Account} 注册成功");
            }

            response.ErrorCode = res;
            await FTask.CompletedTask;
        }
    }
}
