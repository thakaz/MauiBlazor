using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazor.Shared.Services;

public interface IFormFactor
{
    public string GetFormFactor();
    public string GetPlatform();
}

