using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.UnitTesting.Moq.Automapper
{
    public class MockMapper : Mock<IMapper>
    {
        public MockMapper Map<T1,T2>(T1 input, T2 output)
        {
            Setup(mapper => mapper.Map<T2>(input)).Returns(output);
            return this;
        }
    }
}
