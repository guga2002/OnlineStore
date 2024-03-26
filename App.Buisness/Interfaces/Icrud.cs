namespace Business.Interfaces
{
    public interface Icrud<T> where T:class
    {
      Task<IEnumerable<T>>  GetAllAsync();
      Task AddAsync(T item);
      Task  DeleteAsync(int item);
      Task UpdateAsync(T item);

    }
}
