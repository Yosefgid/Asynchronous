using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asynchronous
{
    internal class AsyncFileManager
    {
        public async Task<string> ReadFile(string filePath)
        {
            var content = await File.ReadAllTextAsync(filePath);
            return content;
        }
        public async void WriteFile(string filePath, string txt) 
        {
            await File.WriteAllTextAsync(filePath, txt);
          

        }
    }
}
