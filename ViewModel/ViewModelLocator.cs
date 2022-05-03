using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace DirectxWpf.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<SizeViewModel>();
        }

        public MainViewModel MainVM => ServiceLocator.Current.GetInstance<MainViewModel>();

        public SizeViewModel SizeVM => ServiceLocator.Current.GetInstance<SizeViewModel>();

        public static void Cleanup()
        {

        }
    }
}