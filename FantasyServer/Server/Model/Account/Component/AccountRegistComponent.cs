using Fantasy.Entitas;


namespace Model
{
    public class AccountRegistComponent : Entity
    {
        /// <summary>
        /// 存储所有注册过的账号
        /// </summary>
        public readonly Dictionary<string, Account> AccountCacheDic = new Dictionary<string, Account>();
    }
}
