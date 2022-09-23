using IDPService.Entities.Entities;
using System.Collections.Generic;

namespace IDPService.Data.Interfaces
{
    public interface ICategorySetRepository : IGetAll<CategorySet>, IDelete<string>
    {
        string SaveBuiltInCategorySet(CategorySet categorySet);

        bool UpdateComposedModelId(string categorySetId, string composedModelId);

        CategorySet GetBuiltInCategorySetByName(string name);

        CategorySet GetByName(string name);

    }
}