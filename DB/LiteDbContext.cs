using LiteDB;
using Microsoft.Extensions.Options;

namespace LiteDBApp.DB
{
	public interface ILiteDbContext
	{
		LiteDatabase Database { get; }
	}
	public class LiteDbOptions
	{
		public string DatabaseLocation { get; set; }
	}
	public class LiteDbContext : ILiteDbContext
	{
		public LiteDatabase Database { get; }

		public LiteDbContext(IOptions<LiteDbOptions> options)
		{
			Database = new LiteDatabase(options.Value.DatabaseLocation);
		}
	}
}
