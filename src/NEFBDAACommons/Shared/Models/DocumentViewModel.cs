using NEFBDAACommons.DynamicForms.Attributes;
using System;

namespace NEFBDAACommons.Shared.Models
{
    public class DocumentViewModel : BaseViewModel<long>
    {
        [FormDisplay(DisplayNever = true)]
        public Guid OwnerGuid { get; set; }
        public string Name { get; set; }
        [FormDisplay(DisplayNever = true)]
        public string OwnerDocumentType { get; set; }
        [FormDisplay(PredefinedInputType = DynamicForms.Models.InputType.File, DisplayNever = true)]
        public string FilePath { get; set; }
        public string OriginalFilename { get; set; }
        [FormDisplay(DisplayNever = true)]
        public string ContentType { get; set; }
        [FormDisplay(Editable = false)]
        public string FileSize { get; set; }
        [FormDisplay(Editable = false)]
        public DateTime? LastUsedDate { get; set; }
        [FormDisplay(Editable = false)]
        public int Hits { get; set; }
        public override string OptionText => OriginalFilename;

        [FormOneToManyAssociationAttribute(AssociationType = typeof(BasicUserViewModel))]
        [FormDisplay(Editable = false, DisplayOnCreationForm = false, DisplayOnUpdateForm = false)]
        public long CreatorUserId { get; set; }
        [FormDisplay(Editable = false, DisplayOnCreationForm = false, DisplayOnUpdateForm = false)]
        public DateTime CreatedUtc { get; set; }
    }
}
