using IDPService.Entities.Entities;
using System.Collections.Generic;

namespace IDPService.Data.Interfaces
{
    public interface ICategoryRepository
    {

        Category GetBuiltInCategoryByName(string name);
        string Save(Category category);


    }
}