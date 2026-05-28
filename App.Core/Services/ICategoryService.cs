using System.Collections.Generic;
using App.Core.Enums;
using App.Core.Models;

namespace App.Core.Services
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        Category GetById(string id);
        Category Add(Category category);
        bool Update(Category category);
        bool Delete(string id);
        List<Category> Search(string text, CategoryTypeEnum? type, CategoryStatusEnum? status);
    }
}