using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountCenter.Api.Dto.Request
{
    public class CreateUserByLoginNameDto
    {
        public string loginName { get; set; } //	登陆名
        
        public string pwd { get; set; } //	登陆密码

        public string nickName { get; set; }

        public string realName { get; set; }

        public string mobile { get; set; }

        public string email { get; set; }

        public string portrait { get; set; }
        
    }
}
