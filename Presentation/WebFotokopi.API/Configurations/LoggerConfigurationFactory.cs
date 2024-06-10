using Serilog;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;

namespace WebFotokopi.API.Configurations
{
    public static class LoggerConfigurationFactory
    {
        public static Logger CreateLogger(IConfiguration configuration)
        {
            var customColumnOptions = CreateCustomColumnOptions();

            Logger log = new LoggerConfiguration()
                .WriteTo.MSSqlServer(configuration.GetConnectionString("MsSql"),
                    "logs",
                    autoCreateSqlTable: true,
                    columnOptions: new()
                    {
                        Level = { ColumnName = "Level", DataType = SqlDbType.NVarChar, AllowNull = false, DataLength = 128 },
                        TimeStamp = { ColumnName = "TimeStamp", DataType = SqlDbType.DateTimeOffset, AllowNull = false },
                        LogEvent = { ColumnName = "LogEvent", DataType = SqlDbType.NVarChar, AllowNull = true, DataLength = 4000 },
                        Message = { ColumnName = "Message", DataType = SqlDbType.NVarChar, AllowNull = true, DataLength = 4000 },
                        Exception = { ColumnName = "Exception", DataType = SqlDbType.NVarChar, AllowNull = true, DataLength = 4000 },
                        MessageTemplate = { ColumnName = "MessageTemplate", DataType = SqlDbType.NVarChar, AllowNull = true, DataLength = 4000 },
                        AdditionalColumns = customColumnOptions.AdditionalColumns
                    }
                 )
                .Enrich.FromLogContext()
                .MinimumLevel.Information()
                .CreateLogger();
            return log;
        }
        static ColumnOptions CreateCustomColumnOptions()
        {
            var customColumnOptions = new ColumnOptions
            {
                AdditionalColumns = new Collection<SqlColumn>{
                    new SqlColumn{
                        ColumnName = "Username",
                        DataType = SqlDbType.NVarChar,
                        DataLength = 255,
                        AllowNull = true,
                    }
                }
            };

            return customColumnOptions;
        }
    }
}
