using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInv
{
    public interface IAIState
    {
        public void Start(AI ai);
        public void Process(AI ai);
        public void FixedProcess(AI ai);
        public void Stop(AI ai);
    }
}
