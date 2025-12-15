using System;

namespace proj_API_Unlock
{
    public class TtlockTokenResponse
    {
        public string access_token { get; set; }
        public string uid { get; set; }
        public int expires_in { get; set; }
        public string functionality { get; set; } 
    }
}
