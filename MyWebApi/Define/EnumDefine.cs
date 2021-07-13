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

    public enum AttendanceStatus
    {
        Today = 1,
        SevenDays = 7,
        //ThirtyDays,
    }
}
