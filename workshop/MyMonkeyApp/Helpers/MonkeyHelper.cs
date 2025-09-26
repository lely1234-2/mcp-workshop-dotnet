using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using MyMonkeyApp.Models;

namespace MyMonkeyApp.Helpers
{
    public static class MonkeyHelper
    {
        private static List<Monkey> _monkeys = new List<Monkey>();
        private static int _randomPickCount = 0;
        private static readonly HttpClient _httpClient = new HttpClient();

        // MCP 서버에서 원숭이 데이터 가져오기
        public static async Task LoadMonkeysFromServerAsync(string apiUrl)
        {
            var response = await _httpClient.GetStringAsync(apiUrl);
            var monkeys = JsonSerializer.Deserialize<List<Monkey>>(response);
            if (monkeys != null)
                _monkeys = monkeys;
        }

        public static List<Monkey> GetAllMonkeys()
        {
            return _monkeys;
        }

        public static Monkey? FindMonkeyByName(string name)
        {
            return _monkeys.FirstOrDefault(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public static Monkey? GetRandomMonkey()
        {
            if (_monkeys.Count == 0) return null;
            _randomPickCount++;
            var rand = new Random();
            int idx = rand.Next(_monkeys.Count);
            return _monkeys[idx];
        }

        public static int GetRandomPickCount()
        {
            return _randomPickCount;
        }
    }
}
