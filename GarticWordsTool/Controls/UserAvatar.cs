using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GarticWordsTool.Controls
{
    public class UserAvatar : Image
    {
        static UserAvatar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(UserAvatar), new FrameworkPropertyMetadata(typeof(UserAvatar)));
        }

        private GeometryGroup _avatarGeomerty;
        private double SideLength
        {
            get => Width;
        }

        public static readonly DependencyProperty LengthProperty = DependencyProperty.Register(nameof(Length), typeof(double), typeof(UserAvatar), new PropertyMetadata(lengthCallback));

        private static void lengthCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var avatar = d as UserAvatar;
            if(avatar != null)
            {
                double? newLength = e.NewValue as double?;
                if (newLength.HasValue)
                {
                    avatar.Width = newLength.Value;
                    avatar.Height = newLength.Value;
                }
            }
        }

        private double _length;

        public double Length
        {
            get => _length;
            set
            {
                if (value != _length)
                {
                    _length = value;
                    Width = _length;
                    Height= _length;
                }
            }
        }

        public UserAvatar()
        {
            Stretch = Stretch.Uniform;
            _avatarGeomerty = new GeometryGroup();
            _avatarGeomerty.FillRule = FillRule.Nonzero;
            _avatarGeomerty.Children.Add(new EllipseGeometry() { RadiusX = SideLength, RadiusY = SideLength, Center = new Point(SideLength, SideLength) });
            Clip = _avatarGeomerty;
            SizeChanged += UserAvatar_SizeChanged;
        }

        private void UserAvatar_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Height = e.NewSize.Width;
            EllipseGeometry? geometry = _avatarGeomerty.Children[0] as EllipseGeometry;
            if(geometry != null)
            {
                geometry.RadiusX = SideLength / 2;
                geometry.RadiusY = SideLength / 2;
                geometry.Center = new Point(SideLength / 2, SideLength / 2);
            }
        }
    }
}
