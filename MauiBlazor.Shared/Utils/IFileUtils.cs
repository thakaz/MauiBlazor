using MauiBlazor.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazor.Shared.Utils
{
    public interface IFileUtils
    {                
        public Task<string> SaveFileAsync(string filePath ,CancellationToken cancellationToken,string defaultFileName="test.txt");
    }
}
