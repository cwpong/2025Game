using Fantasy.Entitas;
using Fantasy.Entitas.Interface;

namespace Model
{
    public class Account : Entity, ISupportedDataBase
    {
        public string account;
        public string passWord;
        public string lastLoginTime;
        public string registeTime;
    }
}
