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

    public enum AvatarStatus
    {
        Success,
        InvalidAccount,
        AvatarIsFull,
        AvatarIsEmpty,
        InvalidAvatar,
        RepeatAvatarName,
    }

    public enum Job
    {
        Warrior,
        Mage
    }

    public enum ItemType
    {
        HpItem,
        MpItem
    }
}
