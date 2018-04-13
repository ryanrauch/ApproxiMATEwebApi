﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi
{
    public enum AccountType
    {
        Regular = 0,
        Administrative = 1,
        MockedData = 2
    };
    public enum AccountGender
    {
        Male = 0,
        Female = 1
    };
    public enum RegionType
    {
        Neighborhood = 0,
        SocialDistrict = 1
    };
    public enum FriendRequestType
    {
        Normal = 0,
        Blocked = 1
    };
}
