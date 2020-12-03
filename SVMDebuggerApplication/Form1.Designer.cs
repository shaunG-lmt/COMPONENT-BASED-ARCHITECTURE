namespace SVMDebuggerApplication
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.continue_btn = new System.Windows.Forms.Button();
            this.instruction_lb = new System.Windows.Forms.ListBox();
            this.stack_lb = new System.Windows.Forms.ListBox();
            this.code_lbl = new System.Windows.Forms.Label();
            this.stack_lbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // continue_btn
            // 
            this.continue_btn.Location = new System.Drawing.Point(12, 358);
            this.continue_btn.Name = "continue_btn";
            this.continue_btn.Size = new System.Drawing.Size(585, 78);
            this.continue_btn.TabIndex = 0;
            this.continue_btn.Text = "Continue";
            this.continue_btn.UseVisualStyleBackColor = true;
            this.continue_btn.Click += new System.EventHandler(this.continue_btn_Click);
            // 
            // instruction_lb
            // 
            this.instruction_lb.FormattingEnabled = true;
            this.instruction_lb.Location = new System.Drawing.Point(12, 49);
            this.instruction_lb.Name = "instruction_lb";
            this.instruction_lb.Size = new System.Drawing.Size(266, 290);
            this.instruction_lb.TabIndex = 1;
            // 
            // stack_lb
            // 
            this.stack_lb.FormattingEnabled = true;
            this.stack_lb.Location = new System.Drawing.Point(313, 49);
            this.stack_lb.Name = "stack_lb";
            this.stack_lb.Size = new System.Drawing.Size(284, 290);
            this.stack_lb.TabIndex = 2;
            this.stack_lb.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // code_lbl
            // 
            this.code_lbl.AutoSize = true;
            this.code_lbl.Location = new System.Drawing.Point(9, 33);
            this.code_lbl.Name = "code_lbl";
            this.code_lbl.Size = new System.Drawing.Size(32, 13);
            this.code_lbl.TabIndex = 3;
            this.code_lbl.Text = "Code";
            this.code_lbl.Click += new System.EventHandler(this.label1_Click);
            // 
            // stack_lbl
            // 
            this.stack_lbl.AutoSize = true;
            this.stack_lbl.Location = new System.Drawing.Point(310, 33);
            this.stack_lbl.Name = "stack_lbl";
            this.stack_lbl.Size = new System.Drawing.Size(35, 13);
            this.stack_lbl.TabIndex = 4;
            this.stack_lbl.Text = "Stack";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 445);
            this.Controls.Add(this.stack_lbl);
            this.Controls.Add(this.code_lbl);
            this.Controls.Add(this.stack_lb);
            this.Controls.Add(this.instruction_lb);
            this.Controls.Add(this.continue_btn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button continue_btn;
        private System.Windows.Forms.ListBox instruction_lb;
        private System.Windows.Forms.ListBox stack_lb;
        private System.Windows.Forms.Label code_lbl;
        private System.Windows.Forms.Label stack_lbl;
    }
}

