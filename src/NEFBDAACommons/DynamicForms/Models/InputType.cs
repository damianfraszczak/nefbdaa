namespace NEFBDAACommons.DynamicForms.Models
{
    public enum InputType
    {
        Default,
        Number,
        Text,
        Email,
        Url,
        Tel,
        Password,
        Select,
        Radio,
        SelectMultiple,
        TextArea,
        Boolean,
        BooleanAsSelect,
        Date,
        Time,
        DateTime,
        Integer,
        Rating,
        Heart,
        Color,
        // only for rendering, not use for editing
        Image,
        File,
        DualBox,
        Geolocation,
        Html,
    }
}
