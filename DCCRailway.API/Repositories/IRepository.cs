using DccRailway.API.Entities;

namespace DccRailway.API.Repositories;

public interface IRepository<TEntity, TID> where TEntity : IEntity<TID> where TID : IComparable {
    /// <summary>
    ///     Get an object of type T by its unique identifier type I
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    TEntity? GetById(TID id);

    /// <summary>
    ///     Get a list of all objects of type T from the repository
    /// </summary>
    /// <returns></returns>
    IEnumerable<TEntity> GetAll();

    /// <summary>
    ///     Add a new object of type T and return a reference to its new identifier type I
    /// </summary>
    /// <param name="obj">Copy of the object to be written to the repository</param>
    TID Add(TEntity obj);

    /// <summary>
    ///     Update object of type T
    /// </summary>
    /// <param name="obj">Copy of the object to be written to the repository</param>
    void Update(TEntity obj);

    /// <summary>
    ///     Delete the object provided by as a full type
    /// </summary>
    /// <param name="obj">Copy of the object to be written to the repository</param>
    void Delete(TEntity obj);

    /// <summary>
    ///     Delete an object by its unique identifier I
    /// </summary>
    /// <param name="id">Unique identifier of the object</param>
    void Delete(TID id);

    /// <summary>
    ///     Save all entries in this repository (ie: force write to the source files)
    /// </summary>
    void Save();
}