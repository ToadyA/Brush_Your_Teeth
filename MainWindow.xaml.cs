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
        private Dictionary<DateTime, bool> brushingData;

        public MainWindow() 
        {
            InitializeComponent();
            brushingData = new Dictionary<DateTime, bool>();
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
                if (brushingData.TryGetValue(selectedDate.Value, out bool brushed))
                {
                    BrushStatus.Text = brushed ? "Brushed" : "Not Brushed";
                }
                else
                {
                    BrushStatus.Text = "My Teeth!!";
                }
            }
        }

        private void BrushButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedDate = BrushCalendar.SelectedDate;
            if (selectedDate.HasValue)
            {
                if (brushingData.TryGetValue(selectedDate.Value, out bool brushed))
                {
                    brushingData[selectedDate.Value] = !brushingData[selectedDate.Value];
                }
                else
                {
                    brushingData[selectedDate.Value] = true;
                }
                UpdateBrushingStatus();
            }
        }
    }
}
