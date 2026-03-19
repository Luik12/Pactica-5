using System.Text.Json;
using WebApplication2.Modelo;

namespace WebApplication2.Services
{
    public class LogService
    {
        private readonly string _filePath = "user_logs.json";

        public async Task SaveLogAsync(UserLog log)
        {
            List<UserLog> logs = new List<UserLog>();

            if (File.Exists(_filePath))
            {
                var json = await File.ReadAllTextAsync(_filePath);

                if (!string.IsNullOrWhiteSpace(json))
                {
                    logs = JsonSerializer.Deserialize<List<UserLog>>(json)
                           ?? new List<UserLog>();
                }
            }

            logs.Add(log);
            var newJson = JsonSerializer.Serialize(logs, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            await File.WriteAllTextAsync(_filePath, newJson);
        }

        public async Task<List<UserLog>> GetLogsAsync()
        {
            if (!File.Exists(_filePath))
                return new List<UserLog>();

            var json = await File.ReadAllTextAsync(_filePath);

            if (string.IsNullOrWhiteSpace(json))
                return new List<UserLog>();

            return JsonSerializer.Deserialize<List<UserLog>>(json)
                   ?? new List<UserLog>();
        }
    }
}