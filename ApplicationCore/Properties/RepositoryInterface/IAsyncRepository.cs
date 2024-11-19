using System.Linq.Expressions;

namespace ApplicationCore.RepositoryInterface
{
    // 泛型异步仓储接口，用于提供通用的数据访问操作
    //Async返回task
    public interface IAsyncRepository<T> where T : class
    {
        // 根据主键 ID 获取单个实体对象
        // 参数: id - 实体的主键 ID
        // 返回: 异步返回一个实体对象
        Task<T> GetByIdAsync(int id);

        // 获取所有实体的列表
        // 返回: 异步返回一个实体对象的集合
        Task<IEnumerable<T>> ListAllAsync();

        // 根据筛选条件获取实体列表
        // 参数: filter - Lambda 表达式，用于筛选实体的条件
        // 返回: 异步返回符合条件的实体集合
        Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> filter);

        // 添加一个新实体到数据库
        // 参数: entity - 需要添加的实体对象
        // 返回: 异步返回添加后的实体对象（包括生成的主键等信息）
        Task<T> AddAsync(T entity);

        // 更新现有实体的数据
        // 参数: entity - 需要更新的实体对象
        // 返回: 异步返回更新后的实体对象
        Task<T> UpdateAsync(T entity);

        // 删除一个实体
        // 参数: entity - 需要删除的实体对象
        // 返回: 异步操作，无返回值
        Task DeleteAsync(T entity);
    }
}
