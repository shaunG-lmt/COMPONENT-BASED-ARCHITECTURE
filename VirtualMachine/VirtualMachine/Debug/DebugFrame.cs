using System;
using System.Collections.Generic;
using System.Text;

namespace SVM.VirtualMachine.Debug
{
    class DebugFrame : IDebugFrame
    {
        public IInstruction _currentInstruction = null;
        public List<IInstruction> _codeFrame = null;
        public DebugFrame(IInstruction currentInstruction, List<IInstruction> codeFrame)
        {
            _currentInstruction = currentInstruction;
            _codeFrame = codeFrame;
        }
        public IInstruction CurrentInstruction
        {
            get => _currentInstruction;
            set => _currentInstruction = value;
        }

        public List<IInstruction> CodeFrame
        {
            get => _codeFrame;
            set => _codeFrame = value;
        }
    }
}
