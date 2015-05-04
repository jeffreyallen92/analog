﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using LIFES.FileIO;
using System.Diagnostics;

namespace LIFES.UserInterfaces
{
    /*
     * Class Name: EnrollmentForm.cs
     * Author: Riley Smith
     * Date: 3/24/2015
     * Modified by: Scott Smoke
     * 
     * Description: This is the driver class for the Enrollment GUI Window.
     * 
     * Initially generated by Visual Studio GUI builder.
     */
    public partial class EnrollmentForm : Form
    {
        //Constants Used for transition animations
        const int AW_SLIDE = 0X40000;
        const int AW_CENTER = 0x00000010;
        const int AW_HOR_POSITIVE = 0X1;
        const int AW_HOR_NEGATIVE = 0X2;
        const int AW_BLEND = 0X80000;

        [DllImport("user32")]
        static extern bool AnimateWindow(IntPtr hwnd, int time, int flags);

        private string year;
        public EnrollmentForm()
        {
            InitializeComponent();
        }
        /*
         * Method: GetYear
         * Parameters: object sender, EventArgs e
         * Output: year
         * Created By: Scott Smoke
         * Date: 4/9/2015
         * Modified By: Scott Smoke
         * 
         * Description: This returns the selected year.
         */ 
        public string GetYear()
        {
            return year;
        }

        /*
         * Method: ChooseFileButton_Click
         * Pasrameters: object sender, EventArgs e
         * Ouytput: N/A
         * Created By: Scott Smoke
         * Date: 3/26/2015
         * Modified By: Scott Smoke
         * 
         * Description: This will pop up an open file dialog when clicked.
         *  This will allow the user to select a total enrollments file.
         */

        private void ChooseFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Comma Seperated Values |*.csv";
            openFile.Title = "Open Total Enrollments File";
            openFile.ShowDialog();
            Globals.totalEnrollemntsFileName = openFile.FileName;

            RunCompression();
        }

        /* 
         * Method: FallButtonCheckedChanged
         * Parameters: object sender, EventArgs e
         * Output: N/A
         * Created By: Scott Smoke
         * Date: 3/26/2015
         * Modified By: Scott Smoke
         * 
         * Description: When this radio button is clicked it will set the global
         *  semester to fall.
         * 
         */
        private void FallButtonCheckedChanged(object sender, EventArgs e)
        {
            if (fallButton.Checked)
            {
                Globals.semester = "fall";
            }
        }

        /* 
         * Method: SpringButtonCheckedChanged
         * Parameters: object sender, EventArgs e
         * Output: N/A
         * Created By: Scott Smoke
         * Date: 3/26/2015
         * Modified By: Scott Smoke
         * 
         * Description: When this radio button is clicked it will set the global
         *  semester to spring.
         * 
         */
        private void SpringButtonCheckedChanged(object sender, EventArgs e)
        {
            if (springButton.Checked)
            {
                Globals.semester = "spring";

            }
        }

        /*
         * Method: OnLoad
         * Parameters: EventArgs e
         * Output: N/A
         * Created By: Riley Smith
         * Date: 3/30/2015
         * Modified By: Riley Smith
         * 
         * Override the function that loads the Form.
         * Animates the window as it opens.
         */
        protected override void OnLoad(EventArgs e)
        {
            AnimateWindow(this.Handle, 200, AW_CENTER | AW_HOR_POSITIVE);
        }

        /*
         * Method: ComboBox1SelectedIndexChanged
         * Parameters: EventArgs e
         * Output: N/A
         * Created By: Scott Smoke
         * Date: 3/30/2015
         * Modified By: Scott Smoke
         * 
         *Description: This will get the selected year from
         *the drop down menu.
         */
        private void ComboBox1SelectedIndexChanged(object sender, EventArgs e)
        {
            year = comboBox1.SelectedItem.ToString();
            
        }
        /*
        * Method: RunCompression()
        * Parameters: N/A
        * Output: N/A
        * Created By: Riley Smith
        * Date: 3/30/2015
        * Modified By: Riley Smith
        * 
        *Description: This will run the compression.
        */
        public void RunCompression()
        {
            if (Globals.totalEnrollemntsFileName != "")
            {
                CompressedClassTimes compressedClassTimes = 
                    new CompressedClassTimes(Globals.totalEnrollemntsFileName);

                if (compressedClassTimes.getErrorList().Count == 0)
                {
                    Globals.compressedTimes = compressedClassTimes.getCompressedClassTimes();
                    MessageBox.Show("Enrollment File Accepted");
                }
                if (compressedClassTimes.getWarningList().Count !=0)
                {
                    string errorMsg = "";
                    foreach (string ele in compressedClassTimes.getWarningList())
                    {
                        errorMsg = errorMsg + ele + "\n";
                        //debuging info
                       // Debug.WriteLine(errorMsg);
                    }

                    MessageBox.Show(errorMsg, "Warning");
                }

                else
                {
                    string errorMsg = "";
                    foreach (string ele in compressedClassTimes.getErrorList())
                    {
                        errorMsg = errorMsg + ele + "\n";
                    }

                    MessageBox.Show(errorMsg, "ERROR");
                    Globals.compressedTimes = null;
                }
            }

            else
            {
                Globals.compressedTimes = null;
            }
        }
        
        private void Button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a year");
            }
            else
            {
                Close();
            }
          
        }

    }
}
