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

        public override void Handle(string eventName, WebField field, EventContext eventContext)
        {
            switch (eventName)
            {
                case "dopost/new/":
                    PostHandle(field, eventContext);
                    break;
                default:
                    base.Handle(eventName, field, eventContext);
                    break;
            }
        }

        private void PostHandle(WebField field, EventContext eventContext)
        {
            eventContext.Output.Append("DbField:PostHandle<br/>");
        }
    }
}
