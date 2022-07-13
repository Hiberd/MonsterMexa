using MonsterMexa.Domain;

namespace MonsterMexa.BusinessLogic
{
    public static class CategoriesService
    {
        private static List<Category> _categories = new List<Category>();

        public static int CreateCategory(Category category)
        {
            int id = 1;
            if (_categories.Count > 0)
            {
                id = _categories.Max(p => p.Id) + 1;
            }

            _categories.Add(category with { Id = id });
            return id;
        }

        public static int UpdateCategory(Category category)
        {
            _categories.RemoveAll(c => c.Id == category.Id);
            _categories.Add(category with { Id = category.Id});
            return category.Id;
        }

        public static List<Category> GetAllCategories()
        {
            return _categories;
        }
    }
}
