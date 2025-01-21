using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Win32;
using System.IO.Ports;

namespace Brush_Teeth
{
    /// <summary>
    /// Storing a boolean value for whether the user has brushed their teeth, for every calendar day, stored into a dictionary.
    /// </summary>
    public partial class MainWindow : Window {
        private Dictionary<DateTime, byte> brushingData;
        private const string FilePath = "brushing_data.json";
        private const string PointsPath = "points.json";
        private int points = 0;
        private DateTime lastLoginDate = DateTime.MinValue;
        
        private int streak = 0;
        private string streakMessage = "";
        private string pointsMessage = "";

        private bool drags = false;
        private bool brushy = false;

        public MainWindow() 
        {
            InitializeComponent();
            brushingData = new Dictionary<DateTime, byte>();
            LoadBrushingData();
            LoadPointsData();
            InitializeCalendar();
            BrushCalendar.SelectedDatesChanged += CalendarChange;
        }

        private void InitializeCalendar()
        {
            BrushCalendar.SelectedDate = DateTime.Today;
            UpdateBrushingStatus();
            LoginVisibility();
        }

        private void CalendarChange(object sender, SelectionChangedEventArgs e) 
        {
            UpdateBrushingStatus();
            LoginVisibility();
            var selectedDate = BrushCalendar.SelectedDate;
            //Displaying the date to be sure that clicking the calendar has an effect.
            if (selectedDate.HasValue)
            {
                Today.Text = selectedDate.Value.ToString("MMMM dd, yyyy");
            }
            else
            {
                Today.Text = "Not a fine day";
            }
        }

        private void LoginVisibility()
        {
            var selectedDate = BrushCalendar.SelectedDate;
            if(selectedDate.HasValue && selectedDate.Value.Date == DateTime.Today)
            {
                LoginButton.Visibility = Visibility.Visible;
            }
            else
            {
                LoginButton.Visibility = Visibility.Collapsed;
            }
        }

        private void BrushVisibility()
        {
            var selectedDate = BrushCalendar.SelectedDate;
            if (selectedDate.HasValue && selectedDate.Value.Date == DateTime.Today)
            {
                Toothbrush.Visibility = Visibility.Visible;
            }
            else
            {
                Toothbrush.Visibility = Visibility.Collapsed;
            }
        }

        private void UpdateBrushingStatus()
        {
            var selectedDate = BrushCalendar.SelectedDate;
            if (selectedDate.HasValue)
            {
                if (brushingData.TryGetValue(selectedDate.Value, out byte brushed))
                {
                    Console.WriteLine($"brushed number is {brushingData[selectedDate.Value]}");
                    if((brushingData[selectedDate.Value] & 8) != 0)
                    {
                        BrushStatus.Text = "Morning Brushed, ";
                    }
                    else
                    {
                        BrushStatus.Text = "Morning Skipped, ";
                    }
                    if ((brushingData[selectedDate.Value] & 4) != 0)
                    {
                        BrushStatus.Text = BrushStatus.Text + "Swished Mouthwash, ";
                    }
                    else
                    {
                        BrushStatus.Text = BrushStatus.Text + "Skipped Mouthwash, ";
                    }
                    if ((brushingData[selectedDate.Value] & 2) != 0)
                    {
                        BrushStatus.Text = BrushStatus.Text + "Night Brushed, ";
                    }
                    else
                    {
                        BrushStatus.Text = BrushStatus.Text + "Night Skipped, ";
                    }
                    if ((brushingData[selectedDate.Value] & 1) != 0)
                    {
                        BrushStatus.Text = BrushStatus.Text + "Used Floss.";
                    }
                    else
                    {
                        BrushStatus.Text = BrushStatus.Text + "Skipped Floss.";
                    }
                }
                else
                {
                    BrushStatus.Text = "My Teeth!!";
                }

                UpdateStreak();
                UpdateCleanerTeeth();
            }
        }

        private void BrushMornButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedDate = BrushCalendar.SelectedDate;
            if (selectedDate.HasValue)
            {
                if (brushingData.TryGetValue(selectedDate.Value, out byte brushed))
                {
                    brushingData[selectedDate.Value] ^= 8;
                }
                else
                {
                    brushingData[selectedDate.Value] = 8; //there is no value, set Morning Brushed to true.
                }
                UpdateBrushingStatus();
                SaveBrushingData();
            }
        }
            private void WashButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedDate = BrushCalendar.SelectedDate;

            if (selectedDate.HasValue)
            {
                if (brushingData.TryGetValue(selectedDate.Value, out byte brushed))
                {
                    brushingData[selectedDate.Value] ^= 4;
                }
                else
                {
                    brushingData[selectedDate.Value] = 4; //there is no value, set Mouthwashed to true.
                }
                UpdateBrushingStatus();
                SaveBrushingData();
            }
        }
        private void BrushEveButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedDate = BrushCalendar.SelectedDate;
            if (selectedDate.HasValue)
            {
                if (brushingData.TryGetValue(selectedDate.Value, out byte brushed))
                {
                    brushingData[selectedDate.Value] ^= 2;
                }
                else
                {
                    brushingData[selectedDate.Value] = 2; //there is no value, set Night Brushed to true.
                }
                UpdateBrushingStatus();
                SaveBrushingData();
            }
        }
        private void FlossButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedDate = BrushCalendar.SelectedDate;
            if (selectedDate.HasValue)
            {
                if (brushingData.TryGetValue(selectedDate.Value, out byte brushed))
                {
                    brushingData[selectedDate.Value] ^= 1;
                }
                else
                {
                    brushingData[selectedDate.Value] = 1; //there is no value, set Flossed to true.
                }
                UpdateBrushingStatus();
                SaveBrushingData();
            }
        }

        private int CalculateStreak()
        {
            int streak = 0;
            DateTime today = DateTime.Today;
            while(brushingData.ContainsKey(today) && ((brushingData[today] & 8) != 0 || (brushingData[today] & 2) != 0))
            {
                streak++;
                today = today.AddDays(-1);
            }
            return streak;
        }

        private void UpdateStreak()
        {
            streak = CalculateStreak();
            streakMessage = "Streak: " + streak + " days.";
            UpdateStreakStatus();
        }

        private void UpdateCleanerTeeth()
        {
            double opacity = Math.Max(0, 60 - (2 * streak)) / 100.0;
            Yellowed.Opacity = opacity;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if(lastLoginDate.Date != DateTime.Today)//clicking it again won't do anything
            {
                points += 50;
                lastLoginDate = DateTime.Today;
                SavePointsData();
                pointsMessage = "Points: " + points;
            }
        }

        private void UpdateStreakStatus()
        {
            StreakStatus.Text = streakMessage + " " + pointsMessage;
            //a whole method just to concatenate two messages, huh?
        }

        private void SavePointsData()
        {
            var data = new { Points = points, LastLoginDate = lastLoginDate, Yellowness = Yellowed.Opacity, Brushy = brushy };
            string json = JsonConvert.SerializeObject(data);
            File.WriteAllText(PointsPath, json);
        }

        private void LoadPointsData()
        {
            if (File.Exists(PointsPath))
            {
                string json = File.ReadAllText(PointsPath);
                var data = JsonConvert.DeserializeObject<dynamic>(json);
                points = data.Points;
                lastLoginDate = data.LastLoginDate;

                if(lastLoginDate != DateTime.Today)
                {
                    Yellowed.Opacity = 0.6;
                    brushy = false;
                }
                else
                {
                    Yellowed.Opacity = data.Yellowness;
                    if(data.Brushy != null)
                    {
                        brushy = data.Brushy;
                    }
                }
            }
        }

        private void SaveBrushingData()
        {
            string json = JsonConvert.SerializeObject(brushingData);
            File.WriteAllText(FilePath, json);
        }

        private void LoadBrushingData()
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                brushingData = JsonConvert.DeserializeObject<Dictionary<DateTime, byte>>(json) ?? new Dictionary<DateTime, byte>();
            }
        }

        private void ImageButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png"
            };
            if(ofd.ShowDialog() == true)
            {
                BitmapImage bitmap = new BitmapImage(new Uri(ofd.FileName));
                ImageDisplay.Source = bitmap;
            }
        }

        private void Toothbrush_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                var position = e.GetPosition(BrushCanvas);
                Canvas.SetLeft(Toothbrush, position.X - (Toothbrush.Width / 2)); //grab the toothbrush, it snaps to the middle.
                Canvas.SetTop(Toothbrush, position.Y - (Toothbrush.Height / 2));

                if(position.X >= 23 && position.X <= 223 && position.Y >= 38 && position.Y <= 238)
                {
                    Yellowed.Opacity -= 0.01;
                    if(Yellowed.Opacity <= 0.1 && brushy == false)
                    {
                        points += 50;
                        MessageBox.Show("Look at that shine!");
                        SavePointsData();
                        brushy = true;
                    }
                }
            }
        }


    }
}
