using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Core
{
    public delegate bool TryParse<T>(string value, out T result);
}
