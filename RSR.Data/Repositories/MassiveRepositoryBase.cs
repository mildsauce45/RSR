using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using Massive;
using RSR.Core.Extensions;
using RSR.Core.Support;
using RSR.Data.Attributes;
using RSR.Data.Repositories.Contracts;

namespace RSR.Data.Repositories
{
	public abstract class MassiveRespositoryBase<TModel, TKey> : IMassiveRepositoryBase<TModel, TKey>
	{
		private string connectionName;
		private IList<PropertyInfo> properties;
		private IList<PropertyInfo> ignoreInsert;
		private IList<PropertyInfo> ignoreUpdate;

		protected string SetName { get; private set; }
		protected string IdColumn { get; private set; }

		public MassiveRespositoryBase(string connectionName, string setName, string idColumn)
		{
			this.connectionName = connectionName;

			this.SetName = setName;
			this.IdColumn = idColumn;

			GetProperties();
		}

		public TModel Get(TKey key)
		{
			var tbl = GetDynamicSet();

			var result = tbl.Single(key);

			return this.ToConceptualModel(result);
		}

		public TKey Add(TModel newRecord)
		{
			var tbl = GetDynamicSet();

			var result = tbl.Insert(BuildInsertModel(newRecord));

			return (TKey)result;
		}

		public IEnumerable<TModel> Find(string clause, string columns = "*", params object[] args)
		{
			var tbl = GetDynamicSet();

			var results = tbl.Query(string.Format("select {0} from {1} where {2}", columns, SetName, clause), args);

			var models = new List<TModel>();

			if (results.Any())
				models.AddRange(results.Select(ToConceptualModel));

			return models;
		}

		public abstract TModel ToConceptualModel(dynamic row);

		protected DynamicModel GetDynamicSet()
		{
			return new DynamicModel(connectionName, SetName, IdColumn);
		}

		private void GetProperties()
		{
			var type = typeof(TModel);

			properties = new List<PropertyInfo>();
			ignoreInsert = new List<PropertyInfo>();
			ignoreUpdate = new List<PropertyInfo>();

			type.GetProperties().Where(pi => pi.Name != IdColumn).ForEach(pi => properties.Add(pi));

			properties.ForEach(pi =>
			{
				var ignoreAttribute = pi.GetCustomAttribute<IgnoreAttribute>(false);

				if (ignoreAttribute != null)
				{
					if ((ignoreAttribute.CrudType & CrudType.Create) == CrudType.Create)
						ignoreInsert.Add(pi);

					if ((ignoreAttribute.CrudType & CrudType.Update) == CrudType.Update)
						ignoreUpdate.Add(pi);
				}
			});
		}

		private dynamic BuildInsertModel(TModel newRecord)
		{
			var insertModel = new ExpandoObject() as IDictionary<string, object>;

			// Now filter out the key property and any property that is marked with the proper value of the ignore attribute
			foreach (var pi in properties.Where(p => !ignoreInsert.Any(propInfo => propInfo.Name == p.Name)))
				insertModel.Add(pi.Name, pi.GetValue(newRecord, null));

			return insertModel;
		}
	}
}
