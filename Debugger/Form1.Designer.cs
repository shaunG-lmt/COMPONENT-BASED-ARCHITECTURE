namespace Debugger
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.continue_btn = new System.Windows.Forms.Button();
            this.instruction_listbox = new System.Windows.Forms.ListBox();
            this.stack_listbox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // continue_btn
            // 
            this.continue_btn.Location = new System.Drawing.Point(12, 403);
            this.continue_btn.Name = "continue_btn";
            this.continue_btn.Size = new System.Drawing.Size(544, 98);
            this.continue_btn.TabIndex = 0;
            this.continue_btn.Text = "Continue";
            this.continue_btn.UseVisualStyleBackColor = true;
            this.continue_btn.Click += new System.EventHandler(this.continue_btn_Click);
            // 
            // instruction_listbox
            // 
            this.instruction_listbox.FormattingEnabled = true;
            this.instruction_listbox.ItemHeight = 15;
            this.instruction_listbox.Location = new System.Drawing.Point(12, 12);
            this.instruction_listbox.Name = "instruction_listbox";
            this.instruction_listbox.Size = new System.Drawing.Size(250, 379);
            this.instruction_listbox.TabIndex = 1;
            // 
            // stack_listbox
            // 
            this.stack_listbox.FormattingEnabled = true;
            this.stack_listbox.ItemHeight = 15;
            this.stack_listbox.Location = new System.Drawing.Point(306, 12);
            this.stack_listbox.Name = "stack_listbox";
            this.stack_listbox.Size = new System.Drawing.Size(250, 379);
            this.stack_listbox.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 513);
            this.Controls.Add(this.stack_listbox);
            this.Controls.Add(this.instruction_listbox);
            this.Controls.Add(this.continue_btn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button continue_btn;
        private System.Windows.Forms.ListBox instruction_listbox;
        private System.Windows.Forms.ListBox stack_listbox;
    }
}

