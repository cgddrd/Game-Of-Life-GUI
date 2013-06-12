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
using System.Windows.Threading;

namespace GOFGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //Constant size for grid (40 x 40).
        const int MaxGridSize = 40;

        //Time span for 0 days, 0 hours, 0 mins, 0 secs and 300 millisecs.
        readonly TimeSpan TimerInterval = new TimeSpan(0, 0, 0, 0, 300);

        //The collection of cells that make up the grid.
        CellCollection _cells = new CellCollection(MaxGridSize);

        //This is the timer that should be used for WPF. Timer can be used for Windows Forms. 
        DispatcherTimer _timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            //Tells compiler to call 'Window_Loaded' method when form has loaded. (My own method).
            this.Loaded += new RoutedEventHandler(Window_Loaded);

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            //Sets up the display grid.
            InitializeGrid();

            //Determines what method should be called on every timer tick.
            _timer.Tick += new EventHandler(_timer_Tick);

            //Sets the timer interval.
            _timer.Interval = TimerInterval;

        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            //Update the grid and cells.
            _cells.UpdateLife();
        }

        private void InitializeGrid()
        {
            //Generate the grid with 40 rows and columns.
            for (int i = 0; i < MaxGridSize; i++)
            {
                mainGrid.ColumnDefinitions.Add(new ColumnDefinition());
                mainGrid.RowDefinitions.Add(new RowDefinition());
            }

            //Loop through every cell in the grid (40 x 40)
            for (int row = 0; row < MaxGridSize; row++)
            {
                for (int column = 0; column < MaxGridSize; column++)
                {
                    //Create a new Ellipse shape.
                    Ellipse ellipse = new Ellipse();

                    //Set the value of the Row/Column (e.g. Cell) with the Ellipse UI element. (e.g. (0,0), (35,35), (40,40) 
                    Grid.SetColumn(ellipse, column);
                    Grid.SetRow(ellipse, row);

                    /*
                     * This creates a binding between the underlying data and the UI element, which allows the UI
                     * to be automatically updated when the data changes. This can work one-way, or two ways
                     * (e.g. the UI element can UPDATE the underlying data).
                     * 
                     * For this application, the ellipse "listens" for the public 'IsAlive' boolean, and 
                     * displays if it is true, and not if it is false.
                     * 
                     * http://msdn.microsoft.com/en-us/library/cc278072(v=vs.95).aspx
                     */
                    ellipse.DataContext = _cells[row, column];

                    //Add the ellipse to the grid cell.
                    mainGrid.Children.Add(ellipse);

                    //Set the style of the ellipse using the style information defined in the XAML file.
                    ellipse.Style = Resources["lifeStyle"] as Style;
                }
            }
        }

       private void startButton_Click(object sender, RoutedEventArgs e)
        {
            _timer.Start();
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
        }

    }
}
