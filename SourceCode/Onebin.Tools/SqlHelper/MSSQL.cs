using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onebin.Tools
{
    public class MSSQL : ISqlStatement
    {

        public string BeforeGen()
        {
            return String.Empty;
        }

        public string AfterGen()
        {
            return String.Empty;
        }

        public string GenMenu()
        {
            return 
                "IF EXISTS (SELECT 1 FROM [Menu] WHERE [Id] = '{0}') \r\n" +
                "BEGIN \r\n" +
                "UPDATE [Menu] SET [ParentId] = '{1}', [Name]='{2}', [Url]='{3}', [Category]='{4}', [Level]='{5}' WHERE [Id] = '{0}' \r\n" +
                "END \r\n" +
                "ELSE BEGIN \r\n" +
                "INSERT [Menu] ([Id],[ParentId],[Name],[Url],[Category],[Level]) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}') \r\n" +
                "END \r\n" +
                "GO \r\n\r\n";
        }

        public string DeleteMenu()
        {
            return
                "DELETE FROM [Menu] WHERE [Id] NOT IN ({0}) \r\n" +
                "GO \r\n\r\n\r\n\r\n";
        }

        public string GenPermission()
        {
            return
                "IF EXISTS (SELECT 1 FROM [Permission] WHERE [Id] = '{0}') \r\n" +
                "BEGIN \r\n" +
                "UPDATE [Permission] SET [MenuId] = '{1}', [Name]='{2}', [Action]='{3}' WHERE [Id] = '{0}' \r\n" +
                "END \r\n" +
                "ELSE BEGIN \r\n" +
                "INSERT [Permission] ([Id],[MenuId],[Name],[Action]) VALUES ('{0}','{1}','{2}','{3}') \r\n" +
                "END \r\n" +
                "GO \r\n\r\n";
        }

        public string DeletePermission()
        {
            return
                "DELETE FROM [Permission] WHERE [Id] NOT IN ({0}) \r\n" +
                "GO \r\n\r\n\r\n\r\n";
        }
    }
}
