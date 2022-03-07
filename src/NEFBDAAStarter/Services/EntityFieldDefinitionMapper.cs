using NEFBDAACommons.DI.Attributes;
using NEFBDAACommons.DynamicForms.Services;
using NEFBDAACommons.Shared.Models;
using NEFBDAAStarter.Models.Entities;
using NEFBDAAStarter.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NEFBDAAStarter.Services
{
  [ScopeService]
  public class EntityFieldDefinitionMapper : DefaultEntityFormFieldDefinitionModelMapperService
  {
    private static Dictionary<Type, ModelTypes> MODEL_MAPPINGS = new Dictionary<Type, ModelTypes>() {
      { typeof(AppUserViewModel),  ModelTypes.USER},
      { typeof(DocumentViewModel),  ModelTypes.DOCUMENT},
      { typeof(ConfigViewModel),  ModelTypes.CONFIG},
      { typeof(EmailViewModel),  ModelTypes.EMAIL},
      { typeof(CompanyViewModel),  ModelTypes.COMPANY},
       { typeof(NoteViewModel),  ModelTypes.NOTE},
      { typeof(LanguageViewModel),  ModelTypes.LANGUAGE},
    };


    protected override Dictionary<Type, string> GetModelMappings()
    {
      return MODEL_MAPPINGS.ToDictionary(x => x.Key, x => x.Value.ToString().ToLower());
    }
  }
}
