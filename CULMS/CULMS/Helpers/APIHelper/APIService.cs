using CULMS.Model.RequestModel;
using CULMS.Model.ResponseModel;
using CULMS.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CULMS.Helpers.APIHelper
{
    public class APIService : APICall
    {
        public async Task<AuthResponseObject<TokenValidateData>> TokenValidateAsync(string Token)
        {
            return await GetRequestAsync<AuthResponseObject<TokenValidateData>>(StringConstant.ApeKeyValidateToken + Token, false);
        }
    }
}
