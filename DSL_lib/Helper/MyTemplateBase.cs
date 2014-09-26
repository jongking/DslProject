using RazorEngine.Templating;

namespace DSL_lib.Helper
{
    public class MyTemplateBase<T> : TemplateBase<T>
    {
        public string ToUpperCase(string name)
        {
            return name.ToUpper();
        }
    }
}