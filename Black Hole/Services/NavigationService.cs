using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Black_Hole.Services
{
    public class NavigationService
    {
        private static NavigationService _instance;
        private Frame _frame;

        private NavigationService() { }

        public static NavigationService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NavigationService();
                }
                return _instance;
            }
        }

        public void Initialize(Frame frame)
        {
            _frame = frame;
        }

        public void NavigateTo(Type pageType, object parameter, NavigationTransitionInfo transitionInfo = null)
        {
            if (transitionInfo == null)
            {
                _frame.Navigate(pageType, parameter);
            }
            else
            {
                _frame.Navigate(pageType, parameter, transitionInfo);
            }
        }
    }
}
