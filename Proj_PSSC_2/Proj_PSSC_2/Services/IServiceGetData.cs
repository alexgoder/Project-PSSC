using System;
using Proj_PSSC_2.Models;

namespace Proj_PSSC_2.Services
{
	public interface IServiceReturnType { }

    public interface IServiceGetData : IServiceReturnType
    {
        public IServiceReturnType[] GetData();

        public void SetData(IServiceReturnType[] input);
    }
}
