namespace MyWebApi.Definition
{
    public enum AccountStatus
    {
        Success,

        HadAccount,
        AccountTooShort,
        PasswordTooShort,

        InvalidAccount,
    }

    public enum PunchType
    { 
        PunchIn,
        PunchOut
    }

    public enum AttendanceStatus
    {
        Today = 1,
        ThreeDays = 3,
        SevenDays = 7,
    }
}
