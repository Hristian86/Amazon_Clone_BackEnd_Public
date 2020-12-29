namespace AkciqApp.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ISearchService
    {
        IEnumerable<T> FindSearchResults<T>(string searchword);
    }
}
