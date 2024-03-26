namespace Data.Interfaces
{
    public interface ICrudRep<T> where T:class
    {
        Task<T> GetByIdAsync(int Id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T customer);
        Task DeleteByIdAsync(int Id);
        void Update(T custom);
    }
}
