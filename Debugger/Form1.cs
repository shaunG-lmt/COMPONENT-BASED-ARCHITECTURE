using SVM.VirtualMachine.Debug;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Debugger
{
    public partial class Form1 : Form
    {
        public delegate void PopulateForm(IDebugFrame debugFrame, Stack stack);
        public PopulateForm myDelegate;
            
        public Form1(IDebugFrame debugFrame, Stack stack)
        {
            InitializeComponent();
            myDelegate = new PopulateForm(PopulateFormMethod);
            PopulateFormMethod(debugFrame, stack);
        }
        public void PopulateFormMethod(IDebugFrame debugFrame, Stack stack)
        {
            if (debugFrame != null)
            {
                // Clear list boxes if needed
                if (stack_listbox.Items.Count > 0)
                {
                    stack_listbox.Items.Clear();
                }
                if (instruction_listbox.Items.Count > 0)
                {
                    instruction_listbox.Items.Clear();
                }

                if (debugFrame.CodeFrame.Count != 0)
                {
                    if (debugFrame.CodeFrame.Count > 9 && debugFrame.currentInstructionIndex < debugFrame.CodeFrame.Count - 4)
                    {
                        for (int i = 4; i > 0; i--)
                        {
                            instruction_listbox.Items.Add(debugFrame.CodeFrame[debugFrame.currentInstructionIndex - i]);
                        }
                        instruction_listbox.Items.Add(debugFrame.CodeFrame[debugFrame.currentInstructionIndex]);
                        for (int i = 1; i < 5; i++)
                        {
                            instruction_listbox.Items.Add(debugFrame.CodeFrame[debugFrame.currentInstructionIndex + i]);
                        }
                        //instruction_listbox.SetSelected(5, true); THREADING ISSUE
                    }
                    else if (debugFrame.currentInstructionIndex > 4)
                    {
                        for (int i = 4; i > 0; i--)
                        {
                            instruction_listbox.Items.Add(debugFrame.CodeFrame[debugFrame.currentInstructionIndex - i]);
                        }
                        instruction_listbox.Items.Add(debugFrame.CodeFrame[debugFrame.currentInstructionIndex]);
                        if (debugFrame.currentInstructionIndex > debugFrame.CodeFrame.Count - 4)
                        {
                            for (int i = debugFrame.currentInstructionIndex +1; i < debugFrame.CodeFrame.Count; i++)
                            {
                                instruction_listbox.Items.Add(debugFrame.CodeFrame[i]);
                            }
                        }
                        
                    }
                    else
                    {
                        foreach(var instruction in debugFrame.CodeFrame)
                        {
                            instruction_listbox.Items.Add(instruction.ToString());
                        }
                    }
                }
                // Populate stack listbox
                foreach (var data in stack)
                {
                    stack_listbox.Items.Add(data.ToString());
                }
                this.continue_btn.Enabled = true;
            }
        }
        private void continue_btn_Click(object sender, EventArgs e)
        {
            this.continue_btn.Enabled = false;
            Debugger.IsContinuePressed = true;
            
            return;
        }
    }
}
