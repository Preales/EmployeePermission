﻿namespace N5.Challenge.Domain.Repositories.Base
{
    public interface IRead<T, T2>
    {
        Task<T> GetByKeyAsync(T2 key);
        Task<IList<T>> GetAllAsync();
    }

    public interface ICreate<T>
    {
        Task PostAsync(T entity);
    }

    public interface IUpdate<T>
    {
        Task PutAsync(T entity);
    }

    public interface IDeleted<T>
    {
        Task DeleteAsync(T entity);
    }

}
