using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Debugger
{
    public partial class Form1 : Form
    {
        public Form1(string[] instructions, string[] stack, string currentInstruction)
        {
            InitializeComponent();
            // Populate stack listbox
            foreach(string data in stack)
            {
                stack_listbox.Items.Add(data);
            }
            // Populate instructions listbox
            foreach (string instruction in instructions)
            {
                instruction_listbox.Items.Add(instruction);
            }
            // Select current instruction in instruction text box
            //instruction_listbox.SetSelected()
        }

        private void continue_btn_Click(object sender, EventArgs e)
        {
            
        }
    }
}
