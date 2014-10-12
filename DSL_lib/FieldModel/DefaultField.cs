namespace DSL_lib.FieldModel
{
    public class SpanField : Field
    {
        public SpanField()
        {
            SetAttr("tag", "span");
        }

        public SpanField(string label)
        {
            SetAttr("tag", "span")
                .SetAttr("innerhtml", label);
        }

        public override void InitPlug(Field field)
        {
            field.AddAttr("innerhtml", Html());
        }
    }

    public class InputTextField : Field
    {
        public InputTextField()
        {
            SelfInit();
        }

        public InputTextField(string id = "", string classname = "", string type = "", string other = "")
        {
            SelfInit().SetAttr("type", type).AddAttr("class", classname).SetAttr("id", id).SetAttr("other", other);
        }

        private InputTextField SelfInit()
        {
            SetAttr("tag", "input")
                .SetAttr("selfclose", "true");
            return this;
        }

        public override void InitPlug(Field field)
        {
            field.AddAttr("innerhtml", Html());
        }
    }

    public class LabelField : Field
    {
        public LabelField()
        {
            SelfInit();
        }

        public LabelField(string label, string other = "")
        {
            SelfInit().SetAttr("innerhtml", label).SetAttr("other", other);
        }

        private LabelField SelfInit()
        {
            SetAttr("tag", "label");
            return this;
        }

        public override void InitPlug(Field field)
        {
            field.AddAttr("innerhtml", Html());
        }
    }

    public class DivField : Field
    {
        public DivField()
        {
            SelfInit();
        }

        public DivField(string className = "")
        {
            SelfInit().AddAttr("class", className);
        }

        private DivField SelfInit()
        {
            SetAttr("tag", "div");
            return this;
        }

        public override void InitPlug(Field field)
        {
            field.AddAttr("innerhtml", Html());
        }
    }
}