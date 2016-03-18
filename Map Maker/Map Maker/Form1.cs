using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Map_Maker
{
    public partial class Form1 : Form
    {
        //attributes to read and write to file, store user input, and calculate the length and width of the file to make
        StreamReader reader;
        StreamWriter writer;
        Random populate = new Random();
        List<string> change;
        FileInfo files;
        string[,] map;
        string fileName;
        int length;
        int width;
        int counter;
        int setlength;

        //attributes to prevent unnecessary saving and loading of a file
        bool canSave = true;
        bool canLoad = true;
        bool validText = true;

        public Form1()
        {
            InitializeComponent();
            File.Visible = false; //disbale visibility of create file button until user clicks new or random
            CreateFile.IsAccessible = false; //used to differentiate between user clicking on load or new
            Save.IsAccessible = true; //true by default
            Overwrite.Checked = true; //true by default
        }

        private void Load_Click(object sender, EventArgs e)
        {
            canLoad = true; //if load is clicked then set canLoad to true
            openFileDialog1.ShowDialog(); //opens file explorer to let user choose a file

            Output.Size = new System.Drawing.Size(521, 243);
            Output.Location = new System.Drawing.Point(12, 95);
            FileContents.Location = new System.Drawing.Point(11, 79);
            //former format (save for future use):
            //Output.Size = new System.Drawing.Size(244, 243);
            //Output.Location = new System.Drawing.Point(12, 95);
            //FileContents.Location = new System.Drawing.Point(14, 79);


            if (openFileDialog1.FileName.EndsWith(".txt")) //if file chosen is a text file
            {

                FileContents.Text = "File Contents";
                fileName = openFileDialog1.FileName; //store the name of the file clicked into a filename variable
                string files = Path.GetFileName(fileName); //IMPORTANT: gets only the filename in the directory, not the full file path
                fileName = files;
                Load.IsAccessible = true;  //used to identify that load was clicked and not new or random
                string line = ""; //create a string to store the lines of the file being read
                Output.Clear(); //clear the output textbox to display the files by itself
                length = 0; //reset the length to be incremented
                reader = new StreamReader(fileName); //open the file for reading
                while ((line = reader.ReadLine()) != null) //loop to read all lines of the file
                {
                    length++; //increase the length per line to get the file's dimensions
                    Output.Text += line + "\r\n"; //format the output of the file's contents
                }

                
                reader.Close(); //close the file when finished opening
                openFileDialog1.Reset(); //IMPORTANT: Used to close the file opened by the opendialog functions so that the file can be used by another process (re-opened or written in)
                openFileDialog1.Dispose();

                if (length != 0)
                {
                    int conversion;
                    change = new List<string>();
                    //foreach loop to convert every character to an int and add it to a list to write to the file; IMPORTANT: needed to get the set dimension for the file
                    foreach (char s in Output.Text)
                    {
                        conversion = Convert.ToInt32(s);
                        if (conversion >= 48 && conversion <= 57) //used to only save the characters that are numbers (0 = 48, 9 = 57)
                        {
                            change.Add(s.ToString());
                        }
                    }
                    setlength = change.Count;

                    FileContents.Text += "(FileName: " + fileName + " |  Size: " + length + " x " + setlength / length + ")";
                    //previous format (save for future use):
                    //if (fileName.Length <= 9) //if filename is small then format normally
                    //{
                    //    FileContents.Text += "(FileName: " + fileName + " |  Size: " + length + " x " + setlength / length + ")";
                    //}
                    //else //if filename is bigger than 9 then move the descriptions up 
                    //{
                    //  FileContents.Location = new System.Drawing.Point(14, 69);
                    //  FileContents.Text += "(FileName: " + fileName + "\r\n" + "                      Size: " + length + " x " + setlength / length + ")";
                    //}
                }
                else
                {
                    MessageBox.Show("ERROR: Cannot load a blank file.");
                    Reset.PerformClick();
                }
            }
            else if (openFileDialog1.FileName.Contains(".")) //if file isn't a text file then notify the user
            {
                Output.Text = "Cannot display non-text files.";
                FileContents.Text = "File Contents"; //reset display
                canLoad = false;
                Load.IsAccessible = false;
            }
            else //if cancel is chosen after load is chosen
            {
                Load.IsAccessible = false;
                canLoad = false;
            }
        }

        private void New_Click(object sender, EventArgs e)
        {
            //differentiates between new being clicked and load being clicked when saving a file
            New.IsAccessible = true;
            Random.IsAccessible = false;
            //show the option to create a file and reset everything
            File.Visible = true;
            dataGridView1.Visible = false;
            
            FileInput.Clear(); //clears previous filename for a new filename to be entered

            //sets dimensions of display
            Output.Size = new System.Drawing.Size(521, 105);
            Output.Location = new System.Drawing.Point(12, 240);
            FileContents.Location = new System.Drawing.Point(11, 225);
            File.Location = new System.Drawing.Point(150, 94);
            //old Format  (Save for future use):
            ///if (fileName != null && fileName.Length > 9) //if filename is bigger than 9 
            ///{
            //format the display
            ///Output.Size = new System.Drawing.Size(244, 83);
            ///Output.Location = new System.Drawing.Point(12, 255);
            ///FileContents.Location = new System.Drawing.Point(14, 225);

            ///}
            ///else
            ///{
            //format the display
            /// Output.Size = new System.Drawing.Size(244, 101);
            ////Output.Location = new System.Drawing.Point(12, 237);
            ///FileContents.Location = new System.Drawing.Point(14, 223);
            ///}



        }

        private void Random_Click(object sender, EventArgs e)
        {
            //show the option to create a file and reset everything
            File.Visible = true;
            dataGridView1.Visible = false;
            New.IsAccessible = false;
            Random.IsAccessible = true;
            FileInput.Clear();

            //changes display
            Output.Size = new System.Drawing.Size(521, 105);
            Output.Location = new System.Drawing.Point(12, 240);
            FileContents.Location = new System.Drawing.Point(11, 225);
            File.Location = new System.Drawing.Point(150, 94);
            //old Format  (Save for future use):
            ///if (fileName != null && fileName.Length > 9) //if filename is bigger than 9 
            ///{
            //format the display
            ///Output.Size = new System.Drawing.Size(244, 83);
            ///Output.Location = new System.Drawing.Point(12, 255);
            ///FileContents.Location = new System.Drawing.Point(14, 225);

            ///}
            ///else
            ///{
            //format the display
            /// Output.Size = new System.Drawing.Size(244, 101);
            ////Output.Location = new System.Drawing.Point(12, 237);
            ///FileContents.Location = new System.Drawing.Point(14, 223);
            ///}
        }

        private void Save_Click(object sender, EventArgs e)
        {
            canSave = true;
            if (fileName != null) //save only if a file name is entered; used to protect against random saving
            {
                //create an int and a list for converting the characters in the text box and storing them in a list to add to the text file
                int conversion;
                change = new List<string>();

                //foreach loop to convert every character to an int and add it to a list to write to the file
                foreach (char s in Output.Text)
                {
                    conversion = Convert.ToInt32(s);
                    if (conversion >= 48 && conversion <= 57) //used to only save the characters that are numbers (0 = 48, 9 = 57)
                    {
                        change.Add(s.ToString());
                    }
                    else if (conversion > 31 && conversion < 48 || conversion > 57)
                    {
                        canSave = false;
                    }
                }

                if (canSave == false) //if anything other than a number is trying to be saved
                {
                    MessageBox.Show("ERROR: Can only save numbers to a file (No Spaces Allowed).");
                }
                else if (change.Count != setlength)
                {
                    MessageBox.Show("ERROR: The dimensions of the file cannot be changed.");
                }
                else
                {
                    if (Load.IsAccessible == false) //if new or random was clicked to display the file
                    {
                        //create a new 2d array with the dimensions inputted upon creating file and add the text in the text box to it
                        map = new string[length, width];
                        counter = 0;
                        for (int i = 0; i < length; i++)
                        {
                            for (int j = 0; j < width; j++)
                            {
                                map[i, j] = change[counter]; //store the text in the text box into the 2d array
                                counter++; //used to get all of the characters in the list storing the text
                            }
                        }
                    }
                    else if (canLoad == true)//if load was clicked to display the file (Load.IsAccessible == true && length != 0)//click on load
                    {
                        width = change.Count / length; //get the width by dividing the number of characters by the length 
                        map = new string[length, width]; //create a new 2d array with the correct dimensions
                        counter = 0; //used to go through everything in the list
                        for (int i = 0; i < length; i++) //for loop that goes through the text and adds it to the 2d array
                        {
                            for (int j = 0; j < width; j++)
                            {
                                map[i, j] = change[counter];
                                counter++;
                            }
                        }
                    }


                    if (fileName.EndsWith(".txt")) //if a loaded text file is being saved
                    {
                        writer = new StreamWriter(fileName); //open the file for writing

                        //for loop to write the the text within the dimensions of the file
                        for (int i = 0; i < length; i++)
                        {
                            for (int j = 0; j < width; j++)
                            {
                                if (j == width - 1)
                                {
                                    writer.WriteLine(map[i, j]);
                                }
                                else
                                {
                                    writer.Write(map[i, j]);
                                }
                            }
                        }
                        writer.Close(); //close the file when done
                    }
                    else //if a new text file was created
                    {
                        writer = new StreamWriter(fileName + ".txt"); //open the file for writing

                        //for loop to save the file with the dimensions given
                        for (int i = 0; i < length; i++)
                        {
                            for (int j = 0; j < width; j++)
                            {
                                if (j == width - 1)
                                {
                                    writer.WriteLine(map[i, j]);
                                }
                                else
                                {
                                    writer.Write(map[i, j]);
                                }
                            }
                        }
                        writer.Close(); //close file when done
                    }
                }
            }
            else //if no file is available then notify the user
            {
                MessageBox.Show("ERROR: There is not a file to save.");
            }
        }

        private void File_Enter(object sender, EventArgs e)
        {
            

        }

        private void Width_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void FileContents_Click(object sender, EventArgs e)
        {
            


        }

        private void Length_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void CreateFile_Click(object sender, EventArgs e)
        {
            //stores the length/height and width of the text file made by the user
            length = (int)Length.Value;
            width = (int)Width.Value;

            
            Load.IsAccessible = false; //if create is clicked then set load to false
            Output.Clear(); //clears display to only show what was loaded
            FileContents.Text = "File Contents"; //used to reset the file content text

            if (New.IsAccessible == true)
            {
                //goes through each charactered entered as the file name and converts it into an int to filter the data 
                foreach (char c in FileInput.Text)
                {
                    int conversion;
                    conversion = Convert.ToInt32(c);

                    if (conversion < 32 || conversion < 48 && conversion > 32 || conversion >= 58 && conversion <= 64 || conversion >= 91 && conversion <= 94
                        || conversion == 96 || conversion >= 123 && conversion <= 127) //if any invalid variables are in the file name entered
                    {
                        validText = false; //set the access to false as a trigger
                    }
                }

                if (validText == false || FileInput.Text.Trim() == "") //if access is false or nothing is entered
                {
                    //tell the user that it's invalid and reset the display to its default 
                    MessageBox.Show("ERROR: You entered invalid text.");
                    Reset.PerformClick();
                }
                else if (FileInput.Text.Length > 15) //if text is too long then tell user and reset display
                {
                    MessageBox.Show("ERROR: You file name is too long (Max allowed = 15).");
                    Reset.PerformClick();
                }
                else if (Width.Value == 0 || Length.Value == 0) //prevents creation of a file with size 0x0 or anything with a zero in it
                {
                    MessageBox.Show("ERROR: You cannot create a file without a height or width.");
                    Reset.PerformClick();
                }
                else //if nothing is wrong with the file name then create it
                {
                    files = new FileInfo(FileInput.Text + ".txt"); //gets the file name
                    if (Overwrite.Checked == false && files.Exists == true) //if user doesnt want to overwrite existing file
                    {
                        while (files.Exists == true) //loop to create a copy of that file
                        {
                            int extension = populate.Next(1, 41);
                            FileInput.Text = FileInput.Text + "(" + extension.ToString() + ")";
                            files = new FileInfo(FileInput.Text + ".txt");
                        }
                    }
                    
                    //store the file name and open the file for writing
                    fileName = FileInput.Text.Trim();
                    writer = new StreamWriter(fileName + ".txt");

                    //creates a new 2d array specified by the user and fills it with all 0s for the default
                    map = new string[length, width];
                    for (int i = 0; i < length; i++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            map[i, j] = "0";
                        }
                    }

                    //writes the current value of the 2d array (0s by default) into the text file and is formatted by its dimensions
                    for (int i = 0; i < length; i++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            if (j == width - 1) //used to skip to the next line in the file when the width is reached
                            {
                                writer.WriteLine(map[i, j]);
                            }
                            else
                            {
                                writer.Write(map[i, j]); //used to continue writing on the same line until the width is reached
                            }
                        }
                    }
                    writer.Close(); //close the file when done

                    //format the display
                    //Output.Size = new System.Drawing.Size(244, 243);
                    Output.Size = new System.Drawing.Size(521, 243);
                    Output.Location = new System.Drawing.Point(12, 95);
                    FileContents.Location = new System.Drawing.Point(14, 79);

                    FileContents.Text += "(FileName: " + fileName + ".txt" + " |  Size: " + length + " x " + width + ")";

                    //Former format saved below (save for future use):
                    //if (fileName.Length <= 9) //if filename is small then format normally
                    //{
                    //    FileContents.Text += "(FileName: " + fileName + ".txt" + " |  Size: " + length + " x " + width + ")";
                    //}
                    //else //if filename is bigger than 9 then move the descriptions up 
                    //{
                    //   FileContents.Location = new System.Drawing.Point(14, 69);
                    //   FileContents.Text += "(FileName: " + fileName + ".txt" + "\r\n" + "                      Size: " + length + " x " + width + ")";
                    // }

                    //open the file for reading and create a string to store each line
                    reader = new StreamReader(fileName + ".txt");
                    string line = "";
                    while ((line = reader.ReadLine()) != null) //loop to read the file line by line and output accordingly
                    {
                        Output.Text += line + "\r\n"; // '\r\n' is used to maintain the dimensions of the text
                    }
                    reader.Close(); //close the file when done


                    int conversion;
                    change = new List<string>();
                    //foreach loop to convert every character to an int and add it to a list to write to the file; IMPORTANT: needed to get the set dimension for the file
                    foreach (char s in Output.Text)
                    {
                        conversion = Convert.ToInt32(s);
                        if (s >= 48 && s <= 57) //used to only save the characters that are numbers (0 = 48, 9 = 57)
                        {
                            change.Add(s.ToString());
                        }
                    }
                    setlength = change.Count;
                    CreateFile.IsAccessible = true; //turn the access to on
                }
            }
            else if (Random.IsAccessible == true)
            {
                //goes through each charactered entered as the file name and converts it into an int to filter the data 
                foreach (char c in FileInput.Text)
                {
                    int conversion;
                    conversion = Convert.ToInt32(c);

                    if (conversion < 32 || conversion < 48 && conversion > 32 || conversion >= 58 && conversion <= 64 || conversion >= 91 && conversion <= 94
                        || conversion == 96 || conversion >= 123 && conversion <= 127) //if any invalid variables are in the file name entered
                    {
                        validText = false; //set the access to false as a trigger
                    }
                }

                if (validText == false || FileInput.Text.Trim() == "") //if access is false or nothing is entered
                {
                    //tell the user that it's invalid and reset the display to its default 
                    MessageBox.Show("ERROR: You entered invalid text.");
                    Reset.PerformClick();
                }
                else if (FileInput.Text.Length > 15) //if text is too long then tell user and reset display
                {
                    MessageBox.Show("ERROR: You file name is too long (Max allowed = 15).");
                    Reset.PerformClick();
                }
                else if (Width.Value == 0 || Length.Value == 0) //prevents creation of a file with size 0x0 or anything with a zero in it
                {
                    MessageBox.Show("ERROR: You cannot create a file without a height or width.");
                    Reset.PerformClick();
                }
                else //if nothing is wrong with the file name then create it
                {
                    //code to make a copy of the filename entered if it already exists by adding a number to the file name
                    files = new FileInfo(FileInput.Text + ".txt");
                    if (Overwrite.Checked == false && files.Exists == true)
                    {
                        while (files.Exists == true)
                        {
                            int extension = populate.Next(1, 41);
                            FileInput.Text = FileInput.Text + "(" + extension.ToString() + ")";
                            files = new FileInfo(FileInput.Text + ".txt");
                        }
                    }
                    //store the file name and open the file for writing
                    fileName = FileInput.Text.Trim();
                    writer = new StreamWriter(fileName + ".txt");

                    //creates a new 2d array specified by the user and fills it with all 0s for the default
                    map = new string[length, width];
                    for (int i = 0; i < length; i++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            if (i == 0)
                            {
                                map[i, j] = "1"; //fills the top with 1s
                            }
                            else if (i == length - 1)
                            {
                                map[i, j] = "1"; //fills the bottom with 1s
                            }
                            else
                            {
                                map[i, j] = populate.Next(0, 4).ToString();
                            }
                        }
                    }

                    //writes the current value of the 2d array (0s by default) into the text file and is formatted by its dimensions
                    for (int i = 0; i < length; i++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            if (j == width - 1) //used to skip to the next line in the file when the width is reached
                            {
                                writer.WriteLine(map[i, j]);
                            }
                            else
                            {
                                writer.Write(map[i, j]); //used to continue writing on the same line until the width is reached
                            }
                        }
                    }
                    writer.Close(); //close the file when done

                    //format the display
                    //Former Display: Output.Size = new System.Drawing.Size(244, 243);
                    Output.Size = new System.Drawing.Size(521, 243);
                    Output.Location = new System.Drawing.Point(12, 95);
                    FileContents.Location = new System.Drawing.Point(11, 79);

                    FileContents.Text += "(FileName: " + fileName + ".txt" + " |  Size: " + length + " x " + width + ")";

                    //Former format down below (save for future use):
                    //if (fileName.Length <= 9) //if filename is small then format normally
                    // {
                    //   FileContents.Text += "(FileName: " + fileName + ".txt" + " |  Size: " + length + " x " + width + ")";
                    //}
                    //else //if filename is bigger than 9 then move the descriptions up 
                    //{
                    //    FileContents.Location = new System.Drawing.Point(14, 69);
                    //    FileContents.Text += "(FileName: " + fileName + ".txt" + "\r\n" + "                      Size: " + length + " x " + width + ")";
                    //}

                    //open the file for reading and create a string to store each line
                    reader = new StreamReader(fileName + ".txt");
                    string line = "";
                    while ((line = reader.ReadLine()) != null) //loop to read the file line by line and output accordingly
                    {
                        Output.Text += line + "\r\n"; // '\r\n' is used to maintain the dimensions of the text
                    }
                    reader.Close(); //close the file when done


                    int conversion;
                    change = new List<string>();
                    //foreach loop to convert every character to an int and add it to a list to write to the file; IMPORTANT: needed to get the set dimension for the file
                    foreach (char s in Output.Text)
                    {
                        conversion = Convert.ToInt32(s);
                        if (s >= 48 && s <= 57) //used to only save the characters that are numbers (0 = 48, 9 = 57)
                        {
                            change.Add(s.ToString());
                        }
                    }
                    setlength = change.Count;
                    CreateFile.IsAccessible = true; //turn the access to on
                }
            }
        }

        private void FileInput_TextChanged(object sender, EventArgs e)
        {

        }

        private void Output_TextChanged(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void Reset_Click(object sender, EventArgs e)
        {
            fileName = null; //resets file name to prevent overwriting a file even after u press reset
            //resets everything to its default values
            Output.Clear();
            FileInput.Clear();
            Output.Size = new System.Drawing.Size(521, 243);
            Output.Location = new System.Drawing.Point(12, 95);
            FileContents.Location = new System.Drawing.Point(11, 79);
            //previous format (save for future use):
            //Output.Size = new System.Drawing.Size(244, 243);
            //Output.Location = new System.Drawing.Point(12, 95);
            //FileContents.Location = new System.Drawing.Point(14, 79);
            FileContents.Text = "File Contents";
            Length.Value = 0;
            Width.Value = 0;
            setlength = 0;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //this is disabled
            System.Drawing.Graphics draw = this.CreateGraphics();
            System.Drawing.Rectangle square = new System.Drawing.Rectangle(10, 10, 10, 10);
            draw.DrawRectangle(System.Drawing.Pens.Red, square);
        }

        private void Overwrite_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}

