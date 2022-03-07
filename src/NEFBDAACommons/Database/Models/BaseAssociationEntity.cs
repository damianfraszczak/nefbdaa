namespace NEFBDAACommons.Database.Models
{
    public interface BaseAssociationEntity<T, V>
    {
        T A1 { get; set; }
        V A2 { get; set; }
    }
}
