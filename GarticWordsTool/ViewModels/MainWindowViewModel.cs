using GarticWordsTool.Net;
using GarticWordsTool.Utility;
using GarticWordsTool.Views;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GarticWordsTool.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Filed

        private Gartic? _user = null;

        private Uri defaultUserUri = new Uri("pack://application:,,,/GarticWordsTool;component/user.png");

        private string _titie;
        public string Titie
        {
            get
            {
                return _titie;
            }
            set
            {
                _titie = value;
                SetProperty(ref _titie, value);
            }
        }

        private string _userName;

        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        private string _buttonContent;

        public string ButtonContent
        {
            get => _buttonContent;
            set => SetProperty(ref _buttonContent, value);
        }

        private ImageSource _avatarImage;

        public ImageSource AvatarImage
        {
            get => _avatarImage;
            set => SetProperty(ref _avatarImage, value);
        }

        #endregion

        #region Command

        public DelegateCommand LoginCommand { get; private set; }

        #endregion

        #region Constructor

        public MainWindowViewModel()
        {
            InitFiled();
            InitCommand();
        }

        #endregion

        #region Function

        private void InitCommand()
        {
            LoginCommand = new DelegateCommand(OnLogin);
        }

        private void InitFiled()
        {
            Titie = "Gartic词库编辑工具";
            UserName = "未登录";
            ButtonContent = "Gartio登录";
            AvatarImage = new BitmapImage(defaultUserUri);
        }

        private async void OnLogin()
        {
            if(ButtonContent== "Gartio登录")
            {
                var win = new WinLogin() { Owner = System.Windows.Application.Current.MainWindow };
                var vm = new WinLoginViewModel(win);
                win.DataContext = vm;
                if (win.ShowDialog() == true)
                {
                    _user = vm.Result;
                    Models.GarticConfig config = await _user.GetLoginProfile();
                    AvatarImage = ImageUtility.BitmapToBitmapImage(await _user.GetAvatar(config.props.data.user.avatar));
                    UserName = config.props.data.user.nome;
                    ButtonContent = "退出登录";
                }
            }
            else
            {
                if (MessageBox.Show("是否要退出登录？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    AvatarImage = new BitmapImage(defaultUserUri);
                    UserName = "未登录";
                    ButtonContent = "Gartio登录";
                }
            }
        }

        #endregion
    }
}
