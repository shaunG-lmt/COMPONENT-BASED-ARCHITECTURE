using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SVM.VirtualMachine;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

namespace SML_Extensions
{
    class DisplayImage : BaseInstructionWithOperand
    {
        #region Constants
        static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };
        #endregion

        #region Fields
        #endregion

        #region Constructors
        #endregion

        #region Properties
        #endregion

        #region Public methods
        #endregion

        #region Non-public methods
        #endregion

        #region System.Object overrides
        /// <summary>
        /// Determines whether the specified <see cref="System.Object">Object</see> is equal to the current <see cref="System.Object">Object</see>.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object">Object</see> to compare with the current <see cref="System.Object">Object</see>.</param>
        /// <returns><b>true</b> if the specified <see cref="System.Object">Object</see> is equal to the current <see cref="System.Object">Object</see>; otherwise, <b>false</b>.</returns>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// Serves as a hash function for this type.
        /// </summary>
        /// <returns>A hash code for the current <see cref="System.Object">Object</see>.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="System.String">String</see> that represents the current <see cref="System.Object">Object</see>.
        /// </summary>
        /// <returns>A <see cref="System.String">String</see> that represents the current <see cref="System.Object">Object</see>.</returns>
        public override string ToString()
        {
            return base.ToString();
        }
        #endregion

        #region IInstruction Members
        public override void Run()
        {
            var path = VirtualMachine.Stack.Peek();
            if (File.Exists(path.ToString()))
            {
                if(ImageExtensions.Contains(Path.GetExtension(path.ToString()).ToUpperInvariant()))
                {
                    var f = new Form();
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Image = Image.FromFile(path.ToString());
                    pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
                    f.Controls.Add(pictureBox);
                    //f.Show();
                    Thread thread = new Thread(() => Application.Run(f));

                    thread.Start();
                    
                }
                else
                {
                    throw new SvmRuntimeException("File path on top of stack is not valid image. File path: " + path);
                }
                
            }
            else
            {
                throw new SvmRuntimeException("Value on top of stack point to file does not exist. Value at top of stack currently: " + path);
            }

            
        }
        #endregion
    }
}
