using Prism.Mvvm;
using Prism.Navigation;

namespace SQLI.SRS2.Core.Mvvm
{
    public abstract class ViewModelBase : BindableBase, IDestructible
    {
        protected ViewModelBase()
        {

        }

        public virtual void Destroy()
        {

        }
    }
}
