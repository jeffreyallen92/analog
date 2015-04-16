﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LIFES.Authentication;
using System.Runtime.InteropServices;
using System.Collections;

namespace LIFES.UserInterfaces
{
    /*
     * Class Name: DeleteUserForm.cs
     * 
     * Author: Riley Smith
     * Date: 4/1/2015
     * Modified by: Riley Smith
     * 
     * Description: This is the driver class for the Delete User Window.
     * 
     *   Initially generated by Visual Studio GUI builder.
     */
    public partial class DeleteUserForm : Form
    {
        //Constants Used for transition animations
        const int AW_SLIDE = 0X40000;
        const int AW_CENTER = 0x00000010;
        const int AW_HOR_POSITIVE = 0X1;
        const int AW_HOR_NEGATIVE = 0X2;
        const int AW_VER_POSITIVE = 0x00000004;
        const int AW_BLEND = 0X80000;

        [DllImport("user32")]
        static extern bool AnimateWindow(IntPtr hwnd, int time, int flags);

        private UserList userList;
       
        public DeleteUserForm()
        {
            InitializeComponent();

            userList = new UserList();
         
            FillTable();
                
        }

        /*
         * Method: DeleteButton_Click
         * Parameters: N/A
         * Output: N/A
         * Created By: Riley Smith
         * Date: 4/13/2015
         * Modified By: Riley Smith
         * 
         * Description: Event handler for the Delete User Button. 
         */
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (usersGridView.CurrentCell != null)
            {
                string userToDelete = usersGridView.CurrentCell.Value.ToString();
                
                // Confirmation Dialog.
                DialogResult result = MessageBox.Show("Delete " + userToDelete 
                    + "?", "Are You Sure", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    userList.DelUser(userToDelete);
                    
                    int selectedIndex = usersGridView.CurrentCell.RowIndex;
                    if (selectedIndex > -1)
                    {
                        usersGridView.Rows.RemoveAt(selectedIndex);
                        usersGridView.Refresh();
                    }
                }
            }
        }

        /*
         * Method: FillTable
         * Parameters: N/A
         * Output: N/A
         * Created By: Riley Smith
         * Date: 4/13/2015
         * Modified By: Riley Smith
         * 
         * Description: Fills the Users Table with a list of users.
         */
        private void FillTable()
        {
            ArrayList users = userList.GetUserNames();

            int line = 0;
            foreach (string ele in users)
            {
                usersGridView.Rows.Add();
                usersGridView.Rows[line].Cells[0].Value = ele;
                line++;
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
         * Description: Override the function that loads the Form.
         *              Animates the window as it opens.
         */
        protected override void OnLoad(EventArgs e)
        {
            //this.Size = this.Owner.Size;

            //this.Location = this.Owner.Location;

            AnimateWindow(this.Handle, 200, AW_SLIDE | AW_VER_POSITIVE);
        }

       
    }
}
