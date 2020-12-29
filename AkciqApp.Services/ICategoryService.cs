namespace AkciqApp.Services
{
    using System.Collections.Generic;
    using System.Text;

    public interface ICategoryService
    {
        IEnumerable<T> GetAllPerants<T>();

        IEnumerable<T> GetAll<T>(int? count = null);

        T GetByName<T>(string name);
    }
}
