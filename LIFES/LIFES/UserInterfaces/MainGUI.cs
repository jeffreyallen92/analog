﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LIFES.FileIO;
namespace LIFES.UserInterfaces
{
    /*
    * Class Name: MainGUI.cs
    * Author: Riley Smith
    * Date: 3/24/2015
    * Modified by: Riley Smith
    * This is the driver class for the MainGUI Window
    * 
    * Initially generated by Visual Studio GUI builder.
    */
    public partial class MainGUI : Form
    {
        public MainGUI()
        {
            InitializeComponent();

            scheduleButton.Click += Schedule_Click;
            rescheduleButton.Click += Reschedule_Click;
            timeConstraintsButton.Click += TimeConstraintsButton_Click;
            enrollmentButton.Click += EnrollmentButton_Click;
        }

        /*
         * Method: EnrollmentButton_Click
         * Parameters: object sender, EventArgs e
         * Output: N/A
         * Created By: Riley Smith
         * Date: 3/24/2015
         * Modified By: Riley Smith
         * 
         * Event handler for the button Total Enrollment 
         */
        void EnrollmentButton_Click(object sender, EventArgs e)
        {
            EnrollmentForm enrollmentGUI = new EnrollmentForm();
            enrollmentGUI.ShowDialog();

            textTest.Text = "Clicked Total Enrollment Button";
        }

        /*
         * Method: TimeConstraintsButton_Click
         * Parameters: object sender, EventArgs e
         * Output: N/A
         * Created By: Riley Smith
         * Date: 3/24/2015
         * Modified By: Riley Smith
         * 
         * Event handler for the button Time Contraints
         */
        void TimeConstraintsButton_Click(object sender, EventArgs e)
        {

            TimeConstraintsForm timeConstraintsGUI = new TimeConstraintsForm();
            timeConstraintsGUI.ShowDialog();
            TimeConstraints tc = timeConstraintsGUI.GetTimeConstraints();
            Globals.timeConstraints = tc;
            Globals.timeConstraintsFileName = timeConstraintsGUI.GetFileName();
            //Testing
            if (tc != null)
            {
                textTest.Text = tc.GetNumberOfDays().ToString();
            }
            else
            {
                textTest.Text = "Error getting data";
            }
          
        }

        /*
         * Method: Reschedule_Click
         * Parameters: object sender, EventArgs e
         * Output: N/A
         * Created By: Riley Smith
         * Date: 3/24/2015
         * Modified By: Riley Smith
         * 
         * Event handler for the button Reschedule
         */
        void Reschedule_Click(object sender, EventArgs e)
        {
            textTest.Text = "Clicked Reschedule Button";
        }

        /*
         * Method: Schedule_Click
         * Parameters: object sender, EventArgs e
         * Output: N/A
         * Created By: Riley Smith
         * Date: 3/24/2015
         * Modified By: Riley Smith
         * 
         * Event handler for the button Schedule
         */
        void Schedule_Click(object sender, EventArgs e)
        {
            examTable.Rows[0].Cells[0].Value = "First Class Time";
            examTable.Rows[0].Cells[1].Value = "First Exam Time";
 
            textTest.Text = "Clicked Schedule Button";
        }


        // All this was generated by visual
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textTest_TextChanged(object sender, EventArgs e)
        {

        }
        /*
         * Method: SaveAsToolStripMenuItem_Click
         * Parameters: object sender, EventArgs e
         * Output: A filed saved in the requested format.
         * Created By: Scott Smoke
         * Date: 3/26/2015
         * Modified By: Scott Smoke
         * This will allow the user to enter a file name and a save file type.
         * Source: msdn.microsoft.com
         *         http://stackoverflow.com/questions/11964955/how-to-check-what-filter-is-applied
         */
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "txt files (*.txt)| *.txt|" +
            "Comma Sperateve Values (*.csv) |*.csv| pdf (*.pdf) |*.pdf";
            saveFile.ShowDialog();
            FileOut outFile = new FileOut(saveFile.FileName);
            //getting the filter the user selected from the menu
            switch(saveFile.FilterIndex)
            {
                case 1:
                    outFile.WriteToText();
                    break;
                case 2:
                    outFile.WriteToCSV();
                    break;
                case 3:
                    outFile.WriteToPDF();
                    break;
                default:
                    //error
                    break;
            }

        }
        /*
         * Method: PrintToolStripMenuItemClick 
         * Parameters: object sender, EventArgs e
         * Output: N/A
         * Created By: Scott Smoke
         * Date: 3/26/2015
         * Modified By: Scott Smoke
         * This will display a print dialog and allow the user to
         * select a printer.
         * Sources: msdn.miscrosoft.com
         *          http://stackoverflow.com/questions/15985909/show-print-dialog-before-printing
         */
        private void PrintToolStripMenuItemClick(object sender, EventArgs e)
        {
            System.Drawing.Printing.PrintDocument docToPrint =
                new System.Drawing.Printing.PrintDocument();

            //event handler for the object
            docToPrint.PrintPage += 
                new System.Drawing.Printing.
                    PrintPageEventHandler(DocumentPrintPage);

            PrintDialog print = new PrintDialog();
            print.AllowSomePages = false;
            print.ShowHelp = true;
            print.Document = docToPrint;
            DialogResult result = print.ShowDialog();
            if (result == DialogResult.OK)
            {
                docToPrint.Print();
            }
        }
        /*
         * Method:  DocumentPrintPage
         * Parameters: object sender, 
         *             System.Drawing.Printing.PrintPageEventArgs e
         * Output: A printed document
         * Created By: Scott Smoke
         * Date: 3/26/2015
         * Modified By: Scott Smoke
         * This will print a document.
         * Sources: msdn.microsoft.com
         */
        private void DocumentPrintPage(object sender, 
            System.Drawing.Printing.PrintPageEventArgs e)
        {
            //print schedule
            // Insert code to render the page here. 
            // This code will be called when the control is drawn. 

            // The following code will render a simple 
            // message on the printed document. 
            //testing
            string text = "In DocumentPrintPage method.";
            System.Drawing.Font printFont = new System.Drawing.Font
                ("Arial", 35, System.Drawing.FontStyle.Regular);

            // Draw the content.
            e.Graphics.DrawString(text, printFont,
                System.Drawing.Brushes.Black, 10, 10);
        }
    }
}

