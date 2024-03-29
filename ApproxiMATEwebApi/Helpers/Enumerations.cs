﻿
namespace ApproxiMATEwebApi.Helpers
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
    public enum NotificationType
    {
        General = 0,
        FriendRequest = 1
    }
    //public enum FriendStatus
    //{
    //    NotRegistered = 0,
    //    Initiated = 1,
    //    Mutual = 2,
    //    PendingRequest = 3,
    //    Available = 4,
    //    Blocked = 5
    //};
    public enum PolygonOrientation
    {
        Colinear = 0,
        Clockwise = 1,
        CounterClockwise = 2
    };
}
