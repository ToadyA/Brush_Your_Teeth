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

namespace Brush_Teeth
{
    /// <summary>
    /// Storing a boolean value for whether the user has brushed their teeth, for every calendar day, stored into a dictionary.
    /// </summary>
    public partial class MainWindow : Window {
        private Dictionary<DateTime, byte> brushingData;
        private int streak = 0;

        public MainWindow() 
        {
            InitializeComponent();
            brushingData = new Dictionary<DateTime, byte>();
            BrushCalendar.SelectedDatesChanged += CalendarChange;
        }

        private void CalendarChange(object sender, SelectionChangedEventArgs e) 
        {
            UpdateBrushingStatus();
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
            }
        }
            private void WashButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedDate = BrushCalendar.SelectedDate;
            bool undoIt = false;

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
            StreakStatus.Text = "Streak: " + streak + " days.";
        }

        private void UpdateCleanerTeeth()
        {
            double opacity = Math.Max(0, 60 - (2 * streak)) / 100.0;
            Yellowed.Opacity = opacity;
        }

    }
}
