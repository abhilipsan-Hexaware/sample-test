using System.Collections.Generic;

namespace dotnetmongo.Data.Interfaces
{
    public interface IGetAll<T> where T : class
    {
        IEnumerable<T> GetAll();
    }
}
