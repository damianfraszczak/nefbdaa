using NEFBDAACommons.DynamicForms.Attributes;
using System;

namespace NEFBDAACommons.Shared.Models
{
    public class NoteViewModel : BaseViewModel<long>
    {
        [FormDisplay(DisplayNever = true, Filterable = false)]
        public Guid OwnerGuid { get; set; }
        [FormDisplay(PredefinedInputType = DynamicForms.Models.InputType.TextArea)]
        public string Note { get; set; }

        [FormOneToManyAssociationAttribute(AssociationType = typeof(BasicUserViewModel))]
        [FormDisplay(Editable = false, DisplayOnCreationForm = false, DisplayOnUpdateForm = false, FieldText = "Created by")]
        public long CreatorUserId { get; set; }
        [FormDisplay(Editable = false, DisplayOnCreationForm = false, DisplayOnUpdateForm = false, FieldText = "Created time")]
        public DateTime CreatedUtc { get; set; }

        public override string OptionText => Note;
    }
}
