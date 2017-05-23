using System;
using System.IO;
using System.Windows.Forms;

namespace VoodooChaos
{
    public partial class Main_Form : Form
    {
        public Main_Form()
        {
            InitializeComponent();
        }
        OpenFileDialog filecia = new OpenFileDialog();
        OpenFileDialog filencch = new OpenFileDialog();
        OpenFileDialog fileexheader = new OpenFileDialog();
        OpenFileDialog fileicon = new OpenFileDialog();

        private void KeyPressed(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            if (!((c >= '0' && c <= '9') || (c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f') || (Char.IsControl(c))))
            {
                e.Handled = true;
            }
        }

        public void readcia()
        {
            //read the title version value from the cia file and write on to the current title vesion textbox
            BinaryReader br = new BinaryReader(File.OpenRead(filecia.FileName));
            string readcia = null;
            for (int i = 0x2F9C; i <= 0x2F9D; i++)
            {
                br.BaseStream.Position = i;
                readcia += br.ReadByte().ToString("X2");
                CIA_current_titleVers_TextBox.Text = readcia;
            }
            br.Close();
        }

        private void readncch()
        {
            //Read the unique id value from the ncch file and write on to the current unique id textbox
            BinaryReader br = new BinaryReader(File.OpenRead(filencch.FileName));
            string readncch = null;
            for (int i = 0x10B; i >= 0x109; i--)
            {
                br.BaseStream.Position = i;
                readncch += br.ReadByte().ToString("X2");
                NCCH_current_UID_TextBox.Text = readncch;
            }

            //Read the title id value from the ncch file and write on to the current title id textbox
            readncch = null;
            for (int i = 0x150; i <= 0x159; i++)
            {
                br.BaseStream.Position = i;
                readncch += br.ReadChar();
                NCCH_current_TID_TextBox.Text = readncch;
            }
            br.Close();
        }

        public void readexheader()
        {
            //read the unique id value from the ncch file and write on to the current unique id textbox
            BinaryReader br = new BinaryReader(File.OpenRead(fileexheader.FileName));
            string readexheader = null;
            for (int i = 0x1CB; i >= 0x1C9; i--)
            {
                br.BaseStream.Position = i;
                readexheader += br.ReadByte().ToString("X2");
                Exheader_current_UID_TextBox.Text = readexheader;
            }
            br.Close();
        }

        private void Open_cia_button_Click(object sender, EventArgs e)
        {
            try
            {
                //Open up windows file dialog when open cia file button is clicked and wait for users input of cia file. if successful then store the file
                filecia.Filter = " CIA Files (*.Cia)|*.Cia|All Files (*.*)|*.*";

                //Enable the save cia file button
                if (filecia.ShowDialog() == DialogResult.OK)
                {
                    Save_cia_button.Enabled = true;
                    readcia();
                }
            }
            //if something unexpected happens to the file then prompt the following message and disable the save cia file button
            catch
            {
                MessageBox.Show("Invalid .cia file", "Failed to open the file");
                Save_cia_button.Enabled = false;
            }
        }

        private void Save_cia_button_Click(object sender, EventArgs e)
        {
            try
            {
                //when user clicks on the save cia file button then read the text from the new title version box and write it onto the file
                if (CIA_new_titleVers_TextBox.Text.Length == 4)
                {
                    BinaryWriter bw = new BinaryWriter(File.OpenWrite(filecia.FileName));
                    bw.BaseStream.Position = 0x2F9C;
                    bw.Write(Convert.ToByte(CIA_new_titleVers_TextBox.Text.Substring(0, 2), 16));

                    bw.BaseStream.Position = 0x2F9D;
                    bw.Write(Convert.ToByte(CIA_new_titleVers_TextBox.Text.Substring(2, 2), 16));
                    bw.Close();

                    readcia();

                    //prompt the user that the file has been successfully saved
                    MessageBox.Show("CIA File Successfully Saved", "File saved successfully");
                }
                else if (CIA_new_titleVers_TextBox.Text.Length == 0)
                {
                    return;
                }
                else
                {
                    MessageBox.Show("Incorrect title version provided.\nMust be 4 chacracters long!", "Title version error");
                }
            }
            //if something unexpected happens to the file then prompt the following message
            catch
            {
                MessageBox.Show("Invalid CIA file", "Failed to save the file");
            }
        }

        private void Open_ncch_button_Click(object sender, EventArgs e)
        {
            try
            {
                //Open up windows file dialog when open ncch file button is clicked and wait for users input of ncch file. if successful then store the file
                filencch.Filter = " NCCH Files (*.header, *.bin)|*.header;*.bin|All Files (*.*)|*.*";
                //Enable the save ncch file button
                if (filencch.ShowDialog() == DialogResult.OK)
                {
                    Save_ncch_button.Enabled = true;
                    readncch();
                }
            }

            //if something unexpected happens to the file then prompt the following message and disable the save ncch file button
            catch
            {
                MessageBox.Show("Invalid NCCH file", "Failed to open the file");
                Save_ncch_button.Enabled = false;
            }
        }

        private void Save_ncch_button_Click(object sender, EventArgs e)
        {
            try
            {
                //when user clicks on the save ncch file button then read the text from the new unique id box and write it onto the file
                if (NCCH_new_UID_TextBox.Text.Length == 6)
                {
                    BinaryWriter bw = new BinaryWriter(File.OpenWrite(filencch.FileName));
                    bw.BaseStream.Position = 0x10B;
                    bw.Write(Convert.ToByte(NCCH_new_UID_TextBox.Text.Substring(0, 2), 16));

                    bw.BaseStream.Position = 0x10A;
                    bw.Write(Convert.ToByte(NCCH_new_UID_TextBox.Text.Substring(2, 2), 16));

                    bw.BaseStream.Position = 0x109;
                    bw.Write(Convert.ToByte(NCCH_new_UID_TextBox.Text.Substring(4, 2), 16));

                    bw.BaseStream.Position = 0x11B;
                    bw.Write(Convert.ToByte(NCCH_new_UID_TextBox.Text.Substring(0, 2), 16));

                    bw.BaseStream.Position = 0x11A;
                    bw.Write(Convert.ToByte(NCCH_new_UID_TextBox.Text.Substring(2, 2), 16));

                    bw.BaseStream.Position = 0x119;
                    bw.Write(Convert.ToByte(NCCH_new_UID_TextBox.Text.Substring(4, 2), 16));
                    bw.Close();
                }
                else if (NCCH_new_UID_TextBox.Text.Length == 0)
                {
                    Console.WriteLine("Unique ID Empty");
                }
                else
                {
                    MessageBox.Show("Incorrect unique ID provided.\nMust be 6 chacracters long!", "Unique ID error");
                    return;
                }

                if (NCCH_new_TID_TextBox.Text.Length == 10)
                {
                    BinaryWriter bw = new BinaryWriter(File.OpenWrite(filencch.FileName));
                    bw.BaseStream.Position = 0x150;
                    bw.Write(Convert.ToChar(NCCH_new_TID_TextBox.Text.Substring(0, 1)));

                    bw.BaseStream.Position = 0x151;
                    bw.Write(Convert.ToChar(NCCH_new_TID_TextBox.Text.Substring(1, 1)));

                    bw.BaseStream.Position = 0x152;
                    bw.Write(Convert.ToChar(NCCH_new_TID_TextBox.Text.Substring(2, 1)));

                    bw.BaseStream.Position = 0x153;
                    bw.Write(Convert.ToChar(NCCH_new_TID_TextBox.Text.Substring(3, 1)));

                    bw.BaseStream.Position = 0x154;
                    bw.Write(Convert.ToChar(NCCH_new_TID_TextBox.Text.Substring(4, 1)));

                    bw.BaseStream.Position = 0x155;
                    bw.Write(Convert.ToChar(NCCH_new_TID_TextBox.Text.Substring(5, 1)));

                    bw.BaseStream.Position = 0x156;
                    bw.Write(Convert.ToChar(NCCH_new_TID_TextBox.Text.Substring(6, 1)));

                    bw.BaseStream.Position = 0x157;
                    bw.Write(Convert.ToChar(NCCH_new_TID_TextBox.Text.Substring(7, 1)));

                    bw.BaseStream.Position = 0x158;
                    bw.Write(Convert.ToChar(NCCH_new_TID_TextBox.Text.Substring(8, 1)));

                    bw.BaseStream.Position = 0x159;
                    bw.Write(Convert.ToChar(NCCH_new_TID_TextBox.Text.Substring(9, 1)));
                    bw.Close();
                }
                else if (NCCH_new_TID_TextBox.Text.Length == 0)
                {
                    Console.WriteLine("Title ID Empty");
                }
                else
                {
                    MessageBox.Show("Incorrect title ID provided.\nMust be 6 chacracters long!", "Title ID error");
                    return;
                }
                readncch();
                //prompt the user that the file has been successfully saved
                MessageBox.Show("NCCH File Successfully Saved", "File saved successfully");
            }
            //if something unexpected happens to the file then prompt the following messag
            catch
            {
                MessageBox.Show("Invalid NCCH file", "Failed to save the file");
            }
        }

        private void Open_exheader_button_Click(object sender, EventArgs e)
        {
            try
            {
                //Open up windows file dialog when open exheader file button is clicked and wait for users input of exheader file. if successful then store the file
                fileexheader.Filter = " Exheader Files (*.bin)|*.bin|All Files (*.*)|*.*";

                //Enable the save exheader file button
                if (fileexheader.ShowDialog() == DialogResult.OK)
                {
                    Save_exheader_button.Enabled = true;
                    readexheader();
                }
            }

            //if something unexpected happens to the file then prompt the following message and disable the save exheader file button'
            catch
            {
                MessageBox.Show("Invalid Exheader file", "Failed to open the file");
                Save_exheader_button.Enabled = false;
            }
        }

        private void Save_exheader_button_Click(object sender, EventArgs e)
        {
            try
            {
                //when user clicks on the save exheader file button then read the text from the new unique id box and write it onto the file
                if (Exheader_new_UID_TextBox.Text.Length == 6)
                {
                    BinaryWriter bw = new BinaryWriter(File.OpenWrite(fileexheader.FileName));
                    bw.BaseStream.Position = 0x1CB;
                    bw.Write(Convert.ToByte(Exheader_new_UID_TextBox.Text.Substring(0, 2), 16));

                    bw.BaseStream.Position = 0x1CA;
                    bw.Write(Convert.ToByte(Exheader_new_UID_TextBox.Text.Substring(2, 2), 16));

                    bw.BaseStream.Position = 0x1C9;
                    bw.Write(Convert.ToByte(Exheader_new_UID_TextBox.Text.Substring(4, 2), 16));

                    bw.BaseStream.Position = 0x203;
                    bw.Write(Convert.ToByte(Exheader_new_UID_TextBox.Text.Substring(0, 2), 16));

                    bw.BaseStream.Position = 0x202;
                    bw.Write(Convert.ToByte(Exheader_new_UID_TextBox.Text.Substring(2, 2), 16));

                    bw.BaseStream.Position = 0x201;
                    bw.Write(Convert.ToByte(Exheader_new_UID_TextBox.Text.Substring(4, 2), 16));

                    bw.BaseStream.Position = 0x232;
                    bw.Write(Convert.ToByte(Exheader_new_UID_TextBox.Text.Substring(0, 2), 16));

                    bw.BaseStream.Position = 0x231;
                    bw.Write(Convert.ToByte(Exheader_new_UID_TextBox.Text.Substring(2, 2), 16));

                    bw.BaseStream.Position = 0x230;
                    bw.Write(Convert.ToByte(Exheader_new_UID_TextBox.Text.Substring(4, 2), 16));
                    bw.Close();

                    readexheader();

                    //prompt the user that the file has been successfully saved
                    MessageBox.Show("Exheader File Successfully Saved", "File saved successfully");
                }
                else if (Exheader_new_UID_TextBox.Text.Length == 0)
                {
                    return;
                }
                else
                {
                    MessageBox.Show("Incorrect unique ID provided.\nMust be 6 chacracters long!", "Unique ID error");
                    return;
                }
            }
            //if something unexpected happens to the file then prompt the following message
            catch
            {
                MessageBox.Show("Invalid Exheader file", "Failed to save the file");
            }
        }

        private void Open_icon_button_Click(object sender, EventArgs e)
        {
            try
            {
                //Open up windows file dialog when open icon file button is clicked and wait for users input of icon file. if successful then store the file
                fileicon.Filter = " Icon Files (*.icn, *.bin)|*.icn;*.bin|All Files (*.*)|*.*";

                //Enable the Remove Age Rating and Remove Region Lock buttons
                if (fileicon.ShowDialog() == DialogResult.OK)
                {
                    Remove_age_button.Enabled = true;
                    Remove_region_button.Enabled = true;
                }
            }

            //if something unexpected happens to the file then prompt the following message and disable the Remove Age Rating and Remove Region Lock buttons
            catch
            {
                MessageBox.Show("Invalid Icon file", "Failed to open the file");
                Remove_age_button.Enabled = false;
                Remove_region_button.Enabled = false;
            }
        }

        private void Remove_age_button_Click(object sender, EventArgs e)
        {
            try
            {
                BinaryWriter bw = new BinaryWriter(File.OpenWrite(fileicon.FileName));
                bw.BaseStream.Position = 0x2008;
                bw.Write(0x00000000);

                bw.BaseStream.Position = 0x200C;
                bw.Write(0x00000000);

                bw.BaseStream.Position = 0x2010;
                bw.Write(0x00000000);

                bw.BaseStream.Position = 0x2014;
                bw.Write(0x00000000);
                bw.Close();

                //prompt the user that the file has been successfully saved
                MessageBox.Show("Icon File Successfully Saved", "File saved successfully");
            }
            //if something unexpected happens to the file then prompt the following message and disable the save exheader file button
            catch
            {
                MessageBox.Show("Invalid Icon file", "Failed to save the file");
            }
        }

        private void Remove_region_button_Click(object sender, EventArgs e)
        {
            try
            {
                BinaryWriter bw = new BinaryWriter(File.OpenWrite(fileicon.FileName));
                bw.BaseStream.Position = 0x2018;
                bw.Write(0x7FFFFFFF);
                bw.Close();

                //prompt the user that the file has been successfully saved
                MessageBox.Show("Icon File Successfully Saved", "File saved successfully");
            }
            //if something unexpected happens to the file then prompt the following message and disable the save exheader file button
            catch
            {
                MessageBox.Show("Invalid Icon file", "Failed to save the file");
            }
        }
    }
}