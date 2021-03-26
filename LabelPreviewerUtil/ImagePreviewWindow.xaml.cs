using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LabelPreviewerUtil
{

    public partial class ImagePreviewWindow : Window
    {

        private string[] labelsPaths;
        private int selectedLabelIndex;

        public ImagePreviewWindow(string[] filesPaths)
        {
            labelsPaths = filesPaths;
            selectedLabelIndex = 0;

            InitializeComponent();

            SetStateOfNavigationButtons();
            BitmapImage bitmapImage = new BitmapImage(new Uri(labelsPaths[selectedLabelIndex]));


            ImageBox.Source = bitmapImage;
        }

        private async void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            selectedLabelIndex--;
            SetStateOfNavigationButtons();
            await FadeOutImageAsync();
            var image = new BitmapImage(new Uri(labelsPaths[selectedLabelIndex]));
            ImageBox.Source = image;
            await FadeInImageAsync();
        }

        private async void NextButton_Click(object sender, RoutedEventArgs e)
        {
            selectedLabelIndex++;
            SetStateOfNavigationButtons();
            await FadeOutImageAsync();
            var image = new BitmapImage(new Uri(labelsPaths[selectedLabelIndex]));
            ImageBox.Source = image;
            await FadeInImageAsync();
        }


        private void SetStateOfNavigationButtons()
        {
            LabelNumber.Content = $"{selectedLabelIndex + 1}/{labelsPaths.Length}";
            PreviousButton.IsEnabled = !(selectedLabelIndex == 0);
            NextButton.IsEnabled = !(selectedLabelIndex == labelsPaths.Length - 1);
        }

        private async Task FadeOutImageAsync()
        {
            DoubleAnimationUsingKeyFrames animation = new DoubleAnimationUsingKeyFrames();
            animation.Duration = TimeSpan.FromMilliseconds(300);
            animation.KeyFrames.Add(new SplineDoubleKeyFrame(0.1, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(300)),
                                                             new KeySpline(0.4, 0.2, 0.6, 0.4)));
            animation.FillBehavior = FillBehavior.HoldEnd;
            ImageBox.BeginAnimation(OpacityProperty, animation);
            await Task.Delay(300);
        }

        private async Task FadeInImageAsync()
        {
            DoubleAnimationUsingKeyFrames animation = new DoubleAnimationUsingKeyFrames();
            animation.Duration = TimeSpan.FromMilliseconds(300);
            animation.KeyFrames.Add(new SplineDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(300)),
                                                             new KeySpline(0.4, 0.2, 0.6, 0.4)));
            animation.FillBehavior = FillBehavior.HoldEnd;
            ImageBox.BeginAnimation(OpacityProperty, animation);
            await Task.Delay(300);
        }

    }
}
