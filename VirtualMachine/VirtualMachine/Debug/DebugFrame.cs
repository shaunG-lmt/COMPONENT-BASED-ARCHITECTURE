using System;
using System.Collections.Generic;
using System.Text;

namespace SVM.VirtualMachine.Debug
{
    class DebugFrame : IDebugFrame
    {
        public IInstruction _currentInstruction = null;
        public List<IInstruction> _codeFrame = null;
        public int _currentInstructionIndex;
        public DebugFrame(IInstruction currentInstruction, List<IInstruction> codeFrame, int currentInstructionIndex)
        {
            _currentInstruction = currentInstruction;
            _codeFrame = codeFrame;
            _currentInstructionIndex = currentInstructionIndex;
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
        public int currentInstructionIndex
        {
            get => _currentInstructionIndex;
            set => _currentInstructionIndex = value;
        }
    }
}
