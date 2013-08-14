using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onebin.Tools
{
    public interface ISqlStatement
    {
        string BeforeGen();

        string AfterGen();

        string GenMenu();

        string DeleteMenu();

        string GenPermission();

        string DeletePermission();
    }
}
