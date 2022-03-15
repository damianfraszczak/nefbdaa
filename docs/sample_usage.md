# NEFBDAA - .NET Environment for Building Dynamic Angular Applications

## Sample usage
1. Firslty create your domain model that is just an Entity Framework entity.
``` c#
[Table("GuideBookings")]
public class GuideBooking : BaseAuditableEntity<long>, WithOpsDocumentEntity
{
    public DateTime Date { get; set; }
    public GuideBookingStatus Status { get; set; }
    public string Notes { get; set; }
    [StoreAsStringAttribute]
    public IEnumerable<string> Duration { get; set; }
    public long? GuideId { get; set; }
    public Guid GuideGuid { get; set; }
    public virtual AppUser Guide { get; set; }
    public long OpsDocumentId { get; set; }
    public OpsDocument OpsDocument { get; set; }
}
```
In general, this model looks like a normal Entity Framework entity to be stored in a database. This model extends `BaseAuditableEntity` utility class to store entity modification times automatically. 

2. Secondly you need to create a ViewModel class that is a model representation used to communicate with frontend. On this level all default UI configuration is applied.
``` c#
[TableConfig(DisplayedFields = new string[]
    {
        "Date","OpsDocumentPortId", "OpsDocumentClientId", "OpsDocumentShipId",
        "GuideId", "Status"
    },
    FilterableFields = new string[]
    {
        "Date", "GuideId", "OpsDocumentPortId",
        "OpsDocumentClientId", "OpsDocumentShipId", "OpsDocumentPortId", "Duration",
        "Status", "OpsDocumentPoNumber",
        "OpsDocumentPortCoordinatorsId",
    }, ExportableFields = new string[]
    {
        "Date", "GuideId", "OpsDocumentPortId",
        "OpsDocumentClientId", "OpsDocumentShipId", "OpsDocumentPortId", "Duration",
        "Status", "OpsDocumentPoNumber",
        "PlannerId",
    }
)]
[FormConfig(FormGroupLayoutComponent = FormGroupLayoutComponent.CARD, HideDefaultFormGroup = true,
    DefaultFieldLayout = FormLayout.THREE)]
[FormGroupConfig(Name = "Ops document",
    EditFields = new string[]
    {
        "GuideId",
        "OpsDocumentDate","OpsDocumentEndDate", "OpsDocumentPortId", "OpsDocumentClientId", "OpsDocumentShipId", 
        "OpsDocumentPoNumber",
        "OpsDocumentPlannerId", "OpsDocumentPortCoordinatorsId"
    })]
[FormGroupConfig(Name = "Requirements",
    EditFields = new string[]
        {
        "Date", "Status", "Notes", "Duration",
    })]

public class GuideBookingViewModel : WithOpsDocumentDetailsViewModel
{
    [FormOneToManyAssociationAttribute(AssociationType = typeof(AppUserViewModel),
        DefaultFilterFieldNames = new string[] {"RolesString"},
        DefaultFilterFieldOperators = new QueryOperatorEnum[] {QueryOperatorEnum.Like},
        DefaultFilterFieldValues = new string[] {"Guide"}
        )]
    [FormDisplay(GroupName = "Guide name")]
    public long GuideId { get; set; }

    [FormDisplay(PredefinedInputType = InputType.Date, GroupName = "Booking Details",
        Max = "opsDocumentEndDate", Min = "opsDocumentDate")]
    public DateTime Date { get; set; }
    
    public GuideBookingStatus Status { get; set; }

    [FormDisplay(GroupName = "Booking Details", PredefinedInputType = InputType.TextArea)]
    public string Notes { get; set; }

    [FormManyToManyAssociationAttribute(StaticOptions = new string[] {"AM", "PM", "EVE", "FULL DAY"})]
    [FormDisplay(GroupName = "Booking Details", PredefinedInputType = InputType.SelectMultiple)]
    public IEnumerable<string> Duration { get; set; }

    public GuideViewModel Guide { get; set; }
    public override string OptionText => $"{Guide}";
}

```
The listing above presents the table and form configuration using data attributes for the `GuideBookingViewModel` object. There are specified fields that should be visible in the table, which should be used to filter data, and those that should be in the export results in the selected format.
A more complicated configuration is presented for dynamic form generation as it provides much more options. Utilizing FormConfig attribute form was configured to be displayed in CARD layout (FormGroupLayoutComponent), and in each row, there should be placed three fields (DefaultFieldLayout = FormLayout.THREE). Moreover, a default form group (without a name) should not be visible. This group provides a simple way to show all or hide fields on the form. Each FormGroupConfig attribute provides properties for each defined form group visible on the form. Here only fields included in each form group are provided.

3. You should configure service layer: repositry, service for business logic and http controller.
- If you don't need any custom logic in accessing database - repository object - you can use a generic one that provides the default implementation of all actions.
- business logic service
``` c#
public interface IGuideBookingCrudService : ICrudService<GuideBookingViewModel>
{
}
[ScopeService]
public class GuideBookingCrudService :
    WithOpsDocumentCrudService<GuideBooking, GuideBookingViewModel, IGenericRepository<GuideBooking>>,
    IGuideBookingCrudService
{
    private readonly NoteCrudService noteService;

    public GuideBookingCrudService(
        IDatabaseUnitOfWork uow,
        IEntityFormFieldDefinitionsService entityFormFieldDefinitionsService,
        NoteCrudService noteService
    ) : base(uow, entityFormFieldDefinitionsService)
    {
        this.noteService = noteService;
    }
}
```
- http api controller
``` c#
public class ApiGuideBookingController :
    ApiReadWriteController<long, GuideBookingViewModel, ICrudService<GuideBookingViewModel>>
{
    public ApiGuideBookingController(IMemoryCache cache, ICrudServiceUnitOfWork unitOfWork) :
        base(cache, unitOfWork)
    {
    }
}
```
4. Consume it on frontend, for table component usage:
- add it in template
```
<ngx-config-based-table-component [tableConfig]="guideBookingsConfig">
</ngx-config-based-table-component>
```
- configure table
```
	guideBookingsConfig: ITableConfig = {
  title: 'Guide bookings',
  modelType: ModelTypes.GUIDE_BOOKING,
  referencedProperties: [{field: 'opsDocumentId', type: 'integer', value: () => this.formModel.object.id}],
  showBulkUpdate: false,
  filterType: 'basic',
  editModes: ['INLINE', 'PAGE'],
  queryParamsFunction: () => {
    return {'date': this.formModel.object.date};
  },
};
```
and you will get the following result:
![image](https://user-images.githubusercontent.com/19403500/157186610-eda40ece-3a58-4362-bed0-2800e018bc73.png)
