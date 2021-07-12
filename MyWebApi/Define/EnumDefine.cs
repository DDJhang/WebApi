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

    public enum PunchStatus
    { 
        PunchIn,
        PunchOut
    }

    public enum AttendanceStatus
    {
        Today,
        SevenDays,
        //ThirtyDays,
    }
}
