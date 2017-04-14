using House.Annotations;
using Xamarin.Forms;

namespace House.Helpers
{
    public static class Utility
    {
        public static T GetParentControl<T>([CanBeNull] this Element control) where T : class
        {
            // Parent is null return null
            if (control != null && control.ParentView == null)
                return null;

            // Parent is desired control
            // Than return parent
            if (control != null && control.ParentView is T)
                return control.ParentView as T;

            // search for control
            return GetParentControl<T>(control.ParentView);

        }
    }
}
