using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenEquipmentManager.UI.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        private Guid _id;
        private string _userName;
        private string _userType;


        public Guid UserId
        {
            get => _id;
            set => SetProperty(ref _id, value, nameof(UserId));
        }
        public string UserType
        {
            get => _userType;
            set => SetProperty(ref _userType, value, nameof(UserType));
        }
        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value, nameof(UserName));
        }

    }
}
