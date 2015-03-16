using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameOfLife
{
    public partial class MainWindow : Window
    {
        Image[,] images;
        BitmapImage bitmapDead;
        BitmapImage bitmapAlive;
        GameHandler handler;
        Button nextButton;

        public MainWindow()
        {
            bitmapDead = new BitmapImage(new Uri("empty.PNG", UriKind.Relative));
            bitmapAlive = new BitmapImage(new Uri("full.PNG", UriKind.Relative));

            images = new Image[10, 10];
            InitializeComponent();
            DrawBoard();

            handler = new GameHandler(this);
            handler.BeginPlay();
        }

        private void DrawBoard()
        {
            Grid myGrid = new Grid();
            myGrid.Margin = new Thickness(5);
            CreateRowAndColumnDefinitions(myGrid);
            InitZeroFilledBoard(myGrid);
            CreateButtons(myGrid);
            Content = myGrid;
            Show();
        }

        private void CreateButtons(Grid myGrid)
        {
            nextButton = new Button();
            nextButton.Width = 85;
            nextButton.Height = 25;
            nextButton.Content = "Next";
            nextButton.Background = new SolidColorBrush(Color.FromRgb(33, 33, 33));
            nextButton.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            nextButton.Click += NextButton_Click;
            Grid.SetRow(nextButton, 1);
            Grid.SetColumn(nextButton, 11);
            myGrid.Children.Add(nextButton);

            Button resetButton = new Button();
            resetButton.Width = 85;
            resetButton.Height = 25;
            resetButton.Content = "Reset";
            resetButton.Background = new SolidColorBrush(Color.FromRgb(33, 33, 33));
            resetButton.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            resetButton.Click += ResetButton_Click;
            Grid.SetRow(resetButton, 2);
            Grid.SetColumn(resetButton, 11);
            myGrid.Children.Add(resetButton);
        }

        private void InitZeroFilledBoard(Grid myGrid)
        {
            for (int i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    images[i, j] = new Image();
                    images[i, j].Source = bitmapDead;
                    Grid.SetRow(images[i, j], i);
                    Grid.SetColumn(images[i, j], j);
                    myGrid.Children.Add(images[i, j]);
                }
            }
        }

        private void CreateRowAndColumnDefinitions(Grid myGrid)
        {
            for (int i = 0; i < 10; ++i)
            {
                var column = new ColumnDefinition();
                column.Width = new GridLength(40);
                myGrid.ColumnDefinitions.Add(column);

                var row = new RowDefinition();
                row.Height = new GridLength(40);
                myGrid.RowDefinitions.Add(row);
            }

            var buttonColumn = new ColumnDefinition();
            buttonColumn.Width = new GridLength(115);
            myGrid.ColumnDefinitions.Add(buttonColumn);
        }

        public void OnCellDie(int i, int j)
        {
            images[i - 1, j - 1].Source = bitmapDead;
        }

        public void OnCellRise(int i, int j)
        {
            images[i - 1, j - 1].Source = new BitmapImage(new Uri("full.PNG", UriKind.Relative));
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            handler.Next();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 10; ++i)
                for (int j = 0; j < 10; ++j)
                    images[i, j].Source = bitmapDead;
            handler = new GameHandler(this);
            handler.BeginPlay();
        }
    }
}
