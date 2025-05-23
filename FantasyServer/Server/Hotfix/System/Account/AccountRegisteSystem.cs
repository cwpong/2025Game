using Fantasy;
using Fantasy.Async;
using Fantasy.Entitas;
using Fantasy.Helper;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotfix
{
    /// <summary>
    /// 账号业务逻辑系统
    /// </summary>
    public static class AccountRegisteSystem
    {
        public static async FTask<uint> RegisteAccount(this AccountRegistComponent self, string account, string passWord)
        {
            // 1.账号密码格式判断
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(passWord))
            {
                Log.Error($"账号或密码格式错误");
                return 10001;
            }

            // 2.缓存是否是已存在的账号
            if (self.AccountCacheDic.ContainsKey(account))
            {
                Log.Error($"账号{account}已存在");
                return 10002;
            }

            // 3.二次校验数据库中是否有这个账号
            var db = self.Scene.World.DataBase;
            var isExist = await db.Exist<Account>(d => d.account == account);
            if (isExist)
            {
                Log.Error($"账号{account}已存在");
                return 10002;
            }

            // 4.写入缓存
            var accountData = Entity.Create<Account>(self.Scene, true, true);
            accountData.account = account;
            accountData.passWord = passWord;
            accountData.registeTime = TimeHelper.Now.ToString();
            self.AccountCacheDic[account] = accountData;

            // 5.写入数据库
            await db.Save(accountData);

            // TODO 后续加一个注册成功的错误码
            return 10000;
        }
    }
}
