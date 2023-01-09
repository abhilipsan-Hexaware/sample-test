using System.Collections.Generic;

namespace dotnetpipeline.Data.Interfaces
{
    public interface IGetAll<T> where T : class
    {
        IEnumerable<T> GetAll();
    }
}
