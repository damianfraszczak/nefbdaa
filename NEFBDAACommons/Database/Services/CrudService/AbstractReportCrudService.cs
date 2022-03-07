using NEFBDAACommons.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NEFBDAACommons.Database.Services.CrudService
{
    public interface IReportService<T> : ICrudService<T> where T : ReportViewModel
    {
       
    }
    public class AbstractReportCrudService
    {
    }
}
