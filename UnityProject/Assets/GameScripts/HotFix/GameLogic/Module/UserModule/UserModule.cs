using Fantasy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    public class UserModule : Singleton<UserModule>
    {
        // 1.账号注册
        public async void RegisteAccount(string account, string passWord)
        {
            C2A_RegisteAccountRequest req = new C2A_RegisteAccountRequest()
            {
                Account = account,
                PassWord = passWord,
            };

            var response = (A2C_RegisteAccountResponse)await GameModule.NET.SendCallMessage(req);
            if (response.ErrorCode == 10000)
            {

            }
            else
            {
                Log.Error($"账号[{account}]注册失败");
            }
        }
    }
}
