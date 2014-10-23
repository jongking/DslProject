using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL_lib.FieldModel
{
    public class DbField : BasePlug
    {
        private string _tableName = "";

        public DbField(string tableName)
        {
            _tableName = tableName;
        }

        public override void Handle(string eventName, WebField field, StringBuilder Out)
        {
            switch (eventName)
            {
                case "dopost/new/":
                    PostHandle(field, Out);
                    break;
                default:
                    base.Handle(eventName, field, Out);
                    break;
            }
        }

        private void PostHandle(WebField field, StringBuilder Out)
        {
            Out.Append("DbField:PostHandle<br/>");
        }
    }
}
