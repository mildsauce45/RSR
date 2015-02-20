using System.Collections.Generic;

namespace RSR.Data.Repositories.Contracts
{
	public interface IMassiveRepositoryBase<TModel, TKey>
	{
		TModel Get(TKey key);
		TKey Add(TModel newRecord);
		IEnumerable<TModel> Find(string clause, string columns = "*", params object[] args);

		TModel ToConceptualModel(dynamic entity);
	}
}
