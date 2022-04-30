using GarticWordsTool.Net;
using GarticWordsTool.Utility;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GarticWordsTool.ViewModels
{
    public class WinLoginViewModel : BindableBase
    {
        #region Filed
        public Gartic Result { get; set; }

        private Window _window;

        private string _cookieStr;

        public string CookieStr
        {
            get => _cookieStr;
            set => SetProperty(ref _cookieStr, value);
        }

        private string _title;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        #endregion


        #region Command
        public DelegateCommand SubmitCommand { get; set; }
        #endregion

        #region Constructor
        public WinLoginViewModel(Window window)
        {
            _window = window;
            SubmitCommand = new DelegateCommand(OnSubmit);
            Title = "Gartio账户登录";
        }
        #endregion

        #region Function
        private async void OnSubmit()
        {
            string finalCookie = CookieUtility.ConvertCookie(CookieStr);
            if (finalCookie == "")
            {
                MessageBox.Show("输入的Cookie无法转换为可使用的形式，请检查Cookie字符串格式后再试！");
                return;
            }

            Result = new Gartic(finalCookie);
            Models.GarticConfig config = await Result.GetLoginProfile();
            if (await Result.GetAvatar(config.props.data.user.avatar) != default(Bitmap))
            {
                MessageBox.Show("登录成功");
                _window.DialogResult = true;
            }
            else
            {
                MessageBox.Show("无法获取到头像，请检查Cookie是否过期并重试！");
                return;
            }
        }
        #endregion
    }
}
