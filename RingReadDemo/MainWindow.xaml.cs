using System;
using System.Collections.Generic;
using System.IO;
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

namespace RingReadDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int[][] rings = { };
        private ListBox[] lbItems = { };
        private Label[] labelThings = { };
        private Label[] outputLabels = { };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            // expected format
            // row 1 col 1 = number of columns to be read also the number of rings
            // row 1 col 2 = number of characters in the rings
            // row 2 - just headers, skip this
            // column 1 - constant or input side of the rings

            string line = "";
            string[] content = { };
            int rowCount = 0;
            using (StreamReader sr = new StreamReader(txtbPath.Text))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    content = line.Split(',');
                    if(rowCount == 0)
                    {
                        rings = new int[int.Parse(content[0]) + 1][];
                        for(int x = 0; x < rings.Length; x++)
                            rings[x] = new int[int.Parse(content[1])];

                        lblRingCount.Content = "Ring Count : " + content[0];
                        lblCharCount.Content = "Char Count : " + content[1];
                    }
                    else if(rowCount > 1)
                    {
                        for (int x = 0; x < content.Length; x++)
                        {
                            rings[x][rowCount - 2] = int.Parse(content[x]);
                        }
                    }

                    rowCount++;
                }
            }

            MessageBox.Show("Done Reading...");
            string message = "";

            message += "Control ring : " + rings[0].Length + "\n";
            for(int x = 1; x < rings.Length; x++)
                message += "Ring " + x + " : " + rings[x].Length + "\n";
            MessageBox.Show(message);

            summonListBoxes();
        }

        private void summonListBoxes()
        {
            lbItems = new ListBox[rings.Count()];
            labelThings = new Label[rings.Count()];
            outputLabels = new Label[rings.Count()];

            int top = 151;
            int left = 10;

            for(int x = 0; x < lbItems.Length; x++)
            {
                ListBox lbItem = new ListBox();
                Label lblThing = new Label();
                Label lblOutput = new Label();

                if (x == 0)
                {
                    lblThing.Content = "Input";
                    lblOutput.Content = "Selected : 00";
                }
                else
                {
                    lblThing.Content = "Output Ring " + (x - 1);
                    lblOutput.Content = "Output : 00";
                }

                //lbItem.Items.Add(rings[x]);
                for (int y = 0; y < rings[x].Length; y++)
                    lbItem.Items.Add(rings[x][y]);

                lbItem.Height = 200;
                lbItem.Width = 100;

                lblThing.Height = 26;
                lblThing.Width = 100;

                lblOutput.Height = 26;
                lblOutput.Width = 100;

                lbItem.VerticalAlignment = VerticalAlignment.Top;
                lbItem.HorizontalAlignment = HorizontalAlignment.Left;

                lblThing.VerticalAlignment = VerticalAlignment.Top;
                lblThing.HorizontalAlignment = HorizontalAlignment.Left;

                lblOutput.VerticalAlignment = VerticalAlignment.Top;
                lblOutput.HorizontalAlignment = HorizontalAlignment.Left;

                lbItem.Margin = new Thickness(left, top, 0, 0);

                lblThing.Margin = new Thickness(left, 120,0,0);

                lblOutput.Margin = new Thickness(left, 356, 0, 0);

                if (x == 0)
                    lbItem.SelectionChanged += LbItem_SelectionChanged;

                myGrid.Children.Add(lbItem);
                myGrid.Children.Add(lblThing);
                myGrid.Children.Add(lblOutput);

                lbItems[x] = lbItem;
                labelThings[x] = lblThing;
                outputLabels[x] = lblOutput;

                left += 10 + (int)lbItem.Width;
                //top += 10;
            }
        }

        private void LbItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show(lbItems[0].SelectedIndex.ToString());
            for(int x = 0; x < outputLabels.Length; x++)
            {
            }
        }
    }
}
