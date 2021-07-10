using MyWebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi
{
    public class Method
    {
        public static AccountModelDTO AccountToDTO(AccountModel model)
        {
            var dto = new AccountModelDTO()
            {
                Account = model.Account,
                Name = model.Name
            };

            return dto;
        }
    }
}
